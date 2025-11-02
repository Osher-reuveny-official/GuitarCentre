using System;
using System.Data.SqlClient;

namespace GuitarCentre.App_Code
{
    public class GuitarsClass
    {
        private int guitarID;
        private string brand;
        private string model;
        private decimal price;

        public int GuitarID { get => guitarID; set => guitarID = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Model { get => model; set => model = value; }
        public decimal Price { get => price; set => price = value; }

        public GuitarsClass(int guitarID, string brand, string model, decimal price)
        {
            this.guitarID = guitarID;
            this.brand = brand;
            this.model = model;
            this.price = price;
        }

        public void AddGuitar()
        {
            using (Connection conn = new Connection())
            {
                conn.OpenConnection();
                SqlCommand cmd = conn.Command("INSERT INTO tblGuitars (guitarID, brand, model, price) VALUES (@guitarID, @brand, @model, @price)");

                cmd.Parameters.AddWithValue("@guitarID", guitarID);
                cmd.Parameters.AddWithValue("@brand", brand);
                cmd.Parameters.AddWithValue("@model", model);
                cmd.Parameters.AddWithValue("@price", price);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
