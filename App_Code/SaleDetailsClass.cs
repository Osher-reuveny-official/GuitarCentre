using System;
using System.Data.SqlClient;

namespace GuitarCentre.App_Code
{
    public class SaleDetailsClass
    {
        public int ProductID { get; set; }
        public int SaleID { get; set; }
        public int Quantity { get; set; }
        public int ItemsCode { get; set; }

        public SaleDetailsClass(int productID, int saleID, int quantity, int itemsCode)
        {
            ProductID = productID;
            SaleID = saleID;
            Quantity = quantity;
            ItemsCode = itemsCode;
        }

        public void AddSaleDetails()
        {
            try
            {
                using (Connection conn = new Connection())
                {
                    conn.OpenConnection();
                    string query = @"
                        INSERT INTO tblSaleDetails (productID, saleID, quantity, itemsCode)
                        VALUES (@productID, @saleID, @quantity, @itemsCode);
                    ";

                    using (SqlCommand cmd = conn.Command(query))
                    {
                        cmd.Parameters.AddWithValue("@productID", ProductID);
                        cmd.Parameters.AddWithValue("@saleID", SaleID);
                        cmd.Parameters.AddWithValue("@quantity", Quantity);
                        cmd.Parameters.AddWithValue("@itemsCode", ItemsCode);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding sale details: " + ex.Message);
            }
        }
    }
}
