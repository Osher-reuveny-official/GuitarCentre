using System;
using System.Web.UI;

namespace GuitarCentre
{
    public partial class Manufactor : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // טעינה ראשונית — ה-SqlDataSource דואג לנתונים; רק ויזואל קטן אם צריך
            if (!IsPostBack)
            {
                GridView1.DataBind();
            }

            // חבר אירועים לכפתורים אם לא בוצע ב-ASPX
            btnSearch.Click += btnSearch_Click;
            btnClear.Click += btnClear_Click;
        }

        /// <summary>
        /// חיפוש לפי הטקסט בתיבה — פשוט עושה DataBind כדי שה-@q יתעדכן.
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            GridView1.DataBind();
        }

        /// <summary>
        /// ניקוי חיפוש והצגת כל היצרנים.
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GridView1.PageIndex = 0;
            GridView1.DataBind();
        }

        // שמרתי את האירועים שקיימים אצלך כדי שלא יישבר קומפילציה — אפשר להשאיר ריקים.
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e) { }

        // אם ב-ASPX שלך יש אירוע Selecting על SqlDataSource בשם אחר — עדכן את השם כאן או מחק את האירוע מה-ASPX.
        protected void SqlDataSource1_Selecting(object sender, EventArgs e) { }
    }
}
