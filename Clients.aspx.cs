using System;
using System.Web.UI;
using GuitarCentre.App_Code; // כדי להשתמש ב-ClientsClass

namespace GuitarCentre
{
    public partial class Clients : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataBind(); // טעינה ראשונית
            }
        }

        // חיפוש — גם Enter בתיבת החיפוש וגם לחיצה על כפתור
        protected void searchKey_TextChanged(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            GridView1.DataBind();
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            GridView1.DataBind();
        }

        // רישום לקוח חדש
        protected void ClientRegisterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // ולידציה בסיסית בצד השרת (בנוסף ל-Validators)
                if (string.IsNullOrWhiteSpace(ClientRegisterFirstName.Text) ||
                    string.IsNullOrWhiteSpace(ClientRegisterLastName.Text) ||
                    string.IsNullOrWhiteSpace(ClientRegisterID.Text) ||
                    string.IsNullOrWhiteSpace(ClientRegisterPhoneNumber.Text) ||
                    string.IsNullOrWhiteSpace(ClientRegisterEmail.Text))
                {
                    // אפשר לשים Label להודעה; כרגע נזרוק חריג קצר
                    throw new Exception("נא למלא את כל השדות הנדרשים.");
                }

                // ממפים לשכבת המודל שלך
                var client = new ClientsClass(
                    clientID: int.Parse(ClientRegisterID.Text.Trim()),
                    firstName: ClientRegisterFirstName.Text.Trim(),
                    lastName: ClientRegisterLastName.Text.Trim(),
                    email: ClientRegisterEmail.Text.Trim(),
                    address: "", // אם תרצה להוסיף שדה כתובת בטופס
                    phone: ClientRegisterPhoneNumber.Text.Trim()
                );

                client.AddClient();

                // ריענון הגריד
                GridView1.PageIndex = 0;
                GridView1.DataBind();

                // איפוס טופס
                ClientRegisterFirstName.Text = "";
                ClientRegisterLastName.Text = "";
                ClientRegisterID.Text = "";
                ClientRegisterPhoneNumber.Text = "";
                ClientRegisterEmail.Text = "";
            }
            catch (Exception ex)
            {
                // כאן עדיף להציג Label על המסך; למען הדמו נשליך חריג
                throw new Exception("שגיאה ברישום לקוח: " + ex.Message);
            }
        }
    }
}
