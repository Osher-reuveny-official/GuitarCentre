using System;
using System.Data.SqlClient;

namespace GuitarCentre.App_Code
{
    public class AmpClass
    {
        public int AmpID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Power { get; set; }
        public decimal Price { get; set; }

        public AmpClass(int ampID, string brand, string model, int power, decimal price)
        {
            AmpID = ampID;
            Brand = brand;
            Model = model;
            Power = power;
            Price = price;
        }

        public void AddAmp()
        {
            try
            {
                using (Connection conn = new Connection())
                {
                    conn.OpenConnection();

                    using (SqlCommand cmd = conn.Command(
                        "INSERT INTO tblAmps (ampID, brand, model, power, price) VALUES (@ampID, @brand, @model, @power, @price)"
                    ))
                    {
                        cmd.Parameters.AddWithValue("@ampID", AmpID);
                        cmd.Parameters.AddWithValue("@brand", Brand);
                        cmd.Parameters.AddWithValue("@model", Model);
                        cmd.Parameters.AddWithValue("@power", Power);
                        cmd.Parameters.AddWithValue("@price", Price);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add amp to database: " + ex.Message);
            }
        }
    }
}
