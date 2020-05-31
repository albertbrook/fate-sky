using DAL;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FateSky
{
    public partial class Code : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string language = Request.QueryString["language"];
                if (language == null)
                    gvwCode.DataSource = new CodeService().GetAllCodes();
                else
                    gvwCode.DataSource = new CodeService().GetCodesByLanguage(language);
                gvwCode.DataBind();
            }
        }

        protected void GvwCode_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover",
                    "a = this.getElementsByTagName('a')[0];" +
                    "tmpA = a.style.textShadow;" +
                    "a.style.textShadow = '0 0 5px red';" +
                    "tmpColor = this.style.backgroundColor;" +
                    "this.style.backgroundColor = 'rgba(0, 255, 0, 0.3)'");
                e.Row.Attributes.Add("onmouseout",
                    "this.getElementsByTagName('a')[0].style.textShadow = tmpA;" +
                    "this.style.backgroundColor = tmpColor;");
            }
        }

        protected void BtnAddRepo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Code/AddRepository.aspx");
        }
    }
}
