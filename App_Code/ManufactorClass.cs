using System;
using System.Data.SqlClient;

namespace GuitarCentre.App_Code
{
    public class ManufactorClass
    {
        private int manufactorID;
        private string name;
        private string country;

        public int ManufactorID { get => manufactorID; set => manufactorID = value; }
        public string Name { get => name; set => name = value; }
        public string Country { get => country; set => country = value; }

        public ManufactorClass(int manufactorID, string name, string country)
        {
            this.manufactorID = manufactorID;
            this.name = name;
            this.country = country;
        }

        public void AddManufactor()
        {
            using (Connection conn = new Connection())
            {
                conn.OpenConnection();
                SqlCommand cmd = conn.Command("INSERT INTO tblManufactors (manufactorID, name, country) VALUES (@manufactorID, @name, @country)");

                cmd.Parameters.AddWithValue("@manufactorID", manufactorID);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@country", country);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
