using System;
using System.Globalization;
using System.Web.UI;

namespace GuitarCentre
{
    public partial class HomePage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var he = new CultureInfo("he-IL");
                NowDate.Text = DateTime.Now.ToString("dd/MM/yyyy", he);
                NowTime.Text = DateTime.Now.ToString("HH:mm", he);
            }
        }
    }
}
