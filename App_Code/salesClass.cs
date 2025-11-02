using System;
using System.Data.SqlClient;

namespace GuitarCentre.App_Code
{
    public class SalesClass
    {
        public int SaleID { get; set; }              // Identity בטבלה - לא שולחים ב-INSERT
        public int SaleItemsCode { get; set; }       // אם זה קוד הזמנה/אסמכתא (אחרת אפשר לוותר)
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public decimal TotalPrice { get; set; }      // כסף = decimal
        public DateTime SaleDate { get; set; }

        public SalesClass(int saleItemsCode, int customerID, int employeeID, decimal totalPrice, DateTime saleDate)
        {
            SaleItemsCode = saleItemsCode;
            CustomerID = customerID;
            EmployeeID = employeeID;
            TotalPrice = totalPrice;
            SaleDate = saleDate;
        }

        /// <summary>
        /// מוסיף מכירה לטבלת tblSales ומחזיר את המזהה החדש (saleID).
        /// </summary>
        public int AddSale()
        {
            try
            {
                using (Connection conn = new Connection())
                {
                    conn.OpenConnection();

                    string sql = @"
                        INSERT INTO tblSales (saleItemsCode, customerID, employeeID, totalPrice, saleDate)
                        VALUES (@saleItemsCode, @customerID, @employeeID, @totalPrice, @saleDate);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);
                    ";

                    using (SqlCommand cmd = conn.Command(sql))
                    {
                        cmd.Parameters.AddWithValue("@saleItemsCode", SaleItemsCode);
                        cmd.Parameters.AddWithValue("@customerID", CustomerID);
                        cmd.Parameters.AddWithValue("@employeeID", EmployeeID);
                        cmd.Parameters.AddWithValue("@totalPrice", TotalPrice);
                        cmd.Parameters.AddWithValue("@saleDate", SaleDate);

                        // קבל את ה-ID החדש
                        var newIdObj = cmd.ExecuteScalar();
                        SaleID = (newIdObj != null && newIdObj != DBNull.Value) ? Convert.ToInt32(newIdObj) : 0;
                        return SaleID;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting sale: " + ex.Message);
            }
        }
    }
}
