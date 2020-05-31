using DAL;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FateSky
{
    public partial class Note : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptNote.DataSource = new NoteService().GetAllNotes();
                rptNote.DataBind();
                string message = null;
                if (Request.QueryString["insert"] == "1")
                    message = "添加成功";
                else if (Request.QueryString["insert"] != null)
                    message = "添加异常";
                else if (Request.QueryString["delete"] == "1")
                    message = "删除成功";
                else if (Request.QueryString["delete"] != null)
                    message = "删除异常";
                if (message != null)
                {
                    ltlMsg.Text = "<script>alert('" + message + "')</script>";
                    return;
                }
            }
            ltlMsg.Text = "";
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            string text = txtText.Text.Trim().Replace("\n", "").Replace("\r", "");
            int result = new NoteService().InsertNote(text);
            Response.Redirect("~/Note/Note.aspx?insert=" + result);
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            DateTime updateTime = Convert.ToDateTime(((Button)sender).CommandArgument);
            int result = new NoteService().DeleteNote(updateTime);
            Response.Redirect("~/Note/Note.aspx?delete=" + result);
        }
    }
}
