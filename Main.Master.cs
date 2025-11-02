using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuitarCentre
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Logo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
}
