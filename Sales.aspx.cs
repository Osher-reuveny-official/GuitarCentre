using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using GuitarCentre.App_Code; // Connection

namespace GuitarCentre
{
    public partial class Sales : Page
    {
        // עמודות שאנחנו תומכים בהן אם קיימות בטבלת המכירות
        private static readonly string[] SalesCols = new[]
        {
            "saleID", "saleItemsCode", "customerID", "employeeID", "totalPrice", "saleDate"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        protected void Search_Refresh(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            BindGrid();
        }

        protected void Clear_Search(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GridView1.PageIndex = 0;
            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                string table = DetectSalesTable(); // נדרוש tblSales כדי לא להתבלבל עם Details
                if (table == null)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    Fail("Table 'tblSales' not found. Create it with columns: saleID, saleItemsCode, customerID, employeeID, totalPrice, saleDate.");
                    return;
                }

                var existing = GetExistingColumns(table);
                var pick = SalesCols.Where(c => existing.Contains(c, StringComparer.OrdinalIgnoreCase)).ToArray();
                if (pick.Length == 0)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    Fail($"Table '{table}' has none of the expected columns.");
                    return;
                }

                string colList = string.Join(", ", pick.Select(c => $"[{c}]"));
                string sql = $"SELECT {colList} FROM [{table}]";

                // חיפוש
                string q = (txtSearch.Text ?? "").Trim();
                bool hasFilter = false;
                if (!string.IsNullOrEmpty(q))
                {
                    // נסה לסנן על saleID / customerID / employeeID
                    sql += " WHERE (CAST([saleID] AS NVARCHAR(50)) LIKE @q OR CAST([customerID] AS NVARCHAR(50)) LIKE @q OR CAST([employeeID] AS NVARCHAR(50)) LIKE @q)";
                    hasFilter = true;
                }
                sql += " ORDER BY " + (existing.Contains("saleID", StringComparer.OrdinalIgnoreCase) ? "[saleID]" : pick[0]);

                using (var conn = new Connection())
                {
                    conn.OpenConnection();
                    using (var cmd = conn.Command(sql))
                    {
                        if (hasFilter) cmd.Parameters.AddWithValue("@q", "%" + q + "%");
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            da.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }

                Msg.Visible = false;
                Msg.Text = "";
            }
            catch (Exception ex)
            {
                Fail("Load failed: " + ex.Message);
            }
        }

        protected void btnInsertSale_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    Fail("Please fix validation errors.");
                    return;
                }

                string table = DetectSalesTable();
                if (table == null)
                {
                    Fail("Cannot insert: 'tblSales' table not found.");
                    return;
                }

                var existing = GetExistingColumns(table);

                // קלט
                string saleIdStr = (saleIDInput.Text ?? "").Trim();
                string itemsStr = (saleItemsCodeInput.Text ?? "").Trim();
                string custStr = (customerIDInput.Text ?? "").Trim();
                string empStr = (employeeIDInput.Text ?? "").Trim();
                string priceStr = (totalPriceInput.Text ?? "").Trim().Replace(',', '.');
                string dateStr = (saleDateInput.Text ?? "").Trim();

                int? saleID = null;
                if (!string.IsNullOrEmpty(saleIdStr))
                {
                    if (!int.TryParse(saleIdStr, out int sid)) { Fail("Sale ID must be numeric."); return; }
                    saleID = sid;
                }
                if (!int.TryParse(itemsStr, out int itemsCode)) { Fail("Items code must be numeric."); return; }
                if (!int.TryParse(custStr, out int customerID)) { Fail("Customer ID must be numeric."); return; }
                if (!int.TryParse(empStr, out int employeeID)) { Fail("Employee ID must be numeric."); return; }
                if (!decimal.TryParse(priceStr, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal totalPrice))
                { Fail("Invalid total price."); return; }
                if (!DateTime.TryParse(dateStr, out DateTime saleDate))
                { Fail("Invalid sale date."); return; }

                // נבנה INSERT רק לעמודות שקיימות בטבלה בפועל
                var insCols = SalesCols
                    .Where(c => existing.Contains(c, StringComparer.OrdinalIgnoreCase))
                    .ToList();

                // חובה הגיונית: לפחות price+date+customerID+employeeID+saleItemsCode
                string[] must = { "saleItemsCode", "customerID", "employeeID", "totalPrice", "saleDate" };
                foreach (var m in must)
                    if (!insCols.Contains(m, StringComparer.OrdinalIgnoreCase))
                        throw new Exception($"Column '{m}' missing in '{table}'.");

                // אם saleID Identity — פשוט אל תמלא
                if (saleID == null)
                    insCols.RemoveAll(c => c.Equals("saleID", StringComparison.OrdinalIgnoreCase));

                string colList = string.Join(", ", insCols.Select(c => $"[{c}]"));
                string prmList = string.Join(", ", insCols.Select(c => $"@{c}"));
                string sql = $"INSERT INTO [{table}] ({colList}) VALUES ({prmList});";

                using (var conn = new Connection())
                {
                    conn.OpenConnection();
                    using (var cmd = conn.Command(sql))
                    {
                        foreach (var c in insCols)
                        {
                            object val =
                                c.Equals("saleID", StringComparison.OrdinalIgnoreCase) ? (object)saleID :
                                c.Equals("saleItemsCode", StringComparison.OrdinalIgnoreCase) ? itemsCode :
                                c.Equals("customerID", StringComparison.OrdinalIgnoreCase) ? customerID :
                                c.Equals("employeeID", StringComparison.OrdinalIgnoreCase) ? employeeID :
                                c.Equals("totalPrice", StringComparison.OrdinalIgnoreCase) ? totalPrice :
                                c.Equals("saleDate", StringComparison.OrdinalIgnoreCase) ? (object)saleDate :
                                null;

                            var p = cmd.Parameters.AddWithValue("@" + c, val ?? DBNull.Value);
                            if (c.Equals("totalPrice", StringComparison.OrdinalIgnoreCase))
                            {
                                p.SqlDbType = SqlDbType.Decimal; p.Precision = 18; p.Scale = 2;
                            }
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                GridView1.PageIndex = 0;
                BindGrid();
                ClearForm();
                Success("Sale inserted successfully.");
            }
            catch (Exception ex)
            {
                Fail("Insert failed: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            saleIDInput.Text = "";
            saleItemsCodeInput.Text = "";
            customerIDInput.Text = "";
            employeeIDInput.Text = "";
            totalPriceInput.Text = "";
            saleDateInput.Text = "";
        }

        private string DetectSalesTable()
        {
            // מעדיף tblSales; אם אין — נחזיר null כדי שלא נטעה עם הטבלת פרטי מכירה
            using (var conn = new Connection())
            {
                conn.OpenConnection();
                const string q = @"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblSales'";
                using (var cmd = conn.Command(q))
                {
                    var o = cmd.ExecuteScalar();
                    return (o == null) ? null : "tblSales";
                }
            }
        }

        private static System.Collections.Generic.HashSet<string> GetExistingColumns(string table)
        {
            var set = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var conn = new Connection())
            {
                conn.OpenConnection();
                using (var cmd = conn.Command(@"
                    SELECT COLUMN_NAME
                    FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_NAME = @t"))
                {
                    cmd.Parameters.AddWithValue("@t", table);
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
