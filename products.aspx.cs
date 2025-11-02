using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using GuitarCentre.App_Code; // Connection

namespace GuitarCentre
{
    public partial class products : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) GridView1.DataBind();
        }

        // חיפוש / ניקוי
        protected void Search_Refresh(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            GridView1.DataBind();
        }

        protected void Clear_Search(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GridView1.PageIndex = 0;
            GridView1.DataBind();
        }

        protected void tableInsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    Fail("Please fix validation errors.");
                    return;
                }

                // קלט
                string idStr = (productIDinput.Text ?? "").Trim();
                string manuf = (manufactorInput.Text ?? "").Trim();
                string name = (productnameInput.Text ?? "").Trim();
                string desc = (descriptionInput.Text ?? "").Trim();
                string priceStr = (priceInput.Text ?? "").Trim().Replace(',', '.');
                string stockStr = (instockDescription.Text ?? "").Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Fail("Product name is required.");
                    return;
                }
                if (!decimal.TryParse(priceStr, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal price))
                {
                    Fail("Invalid price.");
                    return;
                }
                int? productID = null;
                if (!string.IsNullOrEmpty(idStr))
                {
                    if (!int.TryParse(idStr, out int tmp))
                    {
                        Fail("Product ID must be numeric.");
                        return;
                    }
                    productID = tmp;
                }
                int? stockQty = null;
                if (!string.IsNullOrEmpty(stockStr))
                {
                    if (!int.TryParse(stockStr, out int sq))
                    {
                        Fail("Stock must be an integer.");
                        return;
                    }
                    stockQty = sq;
                }

                // התאמה אוטומטית לסכמה: נבדוק אילו עמודות קיימות ב-tblProducts
                var cols = GetExistingColumns("tblProducts");
                // עמודות אפשריות שנתמוך בהן
                var candidates = new (string col, object val, SqlDbType type)[]
                {
                    ("productID", productID, SqlDbType.Int),
                    ("manufactor", string.IsNullOrWhiteSpace(manuf)? null : (object)manuf, SqlDbType.NVarChar),
                    ("productName", name, SqlDbType.NVarChar),
                    ("description", string.IsNullOrWhiteSpace(desc)? null : (object)desc, SqlDbType.NVarChar),
                    ("price", price, SqlDbType.Decimal),
                    ("inStock", stockQty, SqlDbType.Int),
                    ("stockQuantity", stockQty, SqlDbType.Int) // חלק מהטבלאות קוראות לזה ככה
                };

                // נרכיב INSERT דינמי רק עם העמודות שבאמת קיימות
                var toInsert = candidates.Where(c => c.val != null && cols.Contains(c.col, StringComparer.OrdinalIgnoreCase)).ToList();

                // חייבים לפחות name+price
                if (!toInsert.Any(x => x.col.Equals("productName", StringComparison.OrdinalIgnoreCase)))
                    throw new Exception("Column 'productName' is missing in tblProducts.");
                if (!toInsert.Any(x => x.col.Equals("price", StringComparison.OrdinalIgnoreCase)))
                    throw new Exception("Column 'price' is missing in tblProducts.");

                // אם productID קיים אבל הוא Identity — אל תכלול; אחרת השאילתה תיכשל. זה על המשתמש לדעת את הסכמה שלו.
                // (אם השדה Identity, השמט אותו בטופס או אל תמלא אותו.)

                string colList = string.Join(", ", toInsert.Select(t => $"[{t.col}]"));
                string prmList = string.Join(", ", toInsert.Select(t => $"@{t.col}"));
                string sql = $"INSERT INTO tblProducts ({colList}) VALUES ({prmList});";

                using (var conn = new Connection())
                {
                    conn.OpenConnection();
                    using (var cmd = conn.Command(sql))
                    {
                        foreach (var t in toInsert)
                        {
                            var p = cmd.Parameters.Add($"@{t.col}", t.type);
                            if (t.type == SqlDbType.Decimal)
                            {
                                p.Precision = 18;
                                p.Scale = 2;
                            }
                            p.Value = t.val;
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                // ריענון
                GridView1.PageIndex = 0;
                GridView1.DataBind();
                ClearForm();
                Success("Product inserted successfully.");
            }
            catch (Exception ex)
            {
                Fail("Insert failed: " + ex.Message);
            }
        }

        private HashSet<string> GetExistingColumns(string tableName)
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var conn = new Connection())
            {
                conn.OpenConnection();
                string q = @"
                    SELECT COLUMN_NAME
                    FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_NAME = @t
                ";
                using (var cmd = conn.Command(q))
                {
                    cmd.Parameters.AddWithValue("@t", tableName);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            set.Add(rdr.GetString(0));
                        }
                    }
                }
            }
            return set;
        }

        private void ClearForm()
        {
            productIDinput.Text = "";
            manufactorInput.Text = "";
            productnameInput.Text = "";
            descriptionInput.Text = "";
            priceInput.Text = "";
            instockDescription.Text = "";
        }

        private void Fail(string msg)
        {
            Msg.Visible = true;
            Msg.ForeColor = System.Drawing.Color.Red;
            Msg.Text = msg;
        }

        private void Success(string msg)
        {
            Msg.Visible = true;
            Msg.ForeColor = System.Drawing.Color.Green;
            Msg.Text = msg;
        }
    }
}
