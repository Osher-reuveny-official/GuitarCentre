using System;
using System.Data.SqlClient;

namespace GuitarCentre.App_Code
{
    public class ProductsClass
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public ProductsClass(int productID, string name, decimal price, int stockQuantity)
        {
            ProductID = productID;
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public void AddProduct()
        {
            try
            {
                using (Connection conn = new Connection())
                {
                    conn.OpenConnection();

                    // אם ProductID הוא Identity במסד – אל תכניס אותו ל-INSERT
                    string query = @"
                        INSERT INTO tblProducts (name, price, stockQuantity)
                        VALUES (@name, @price, @stockQuantity);
                    ";

                    using (SqlCommand cmd = conn.Command(query))
                    {
                        cmd.Parameters.AddWithValue("@name", Name);
                        cmd.Parameters.AddWithValue("@price", Price);
                        cmd.Parameters.AddWithValue("@stockQuantity", StockQuantity);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting product: " + ex.Message);
            }
        }
    }
}
