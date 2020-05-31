using DAL;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FateSky
{
    public partial class AddRepository : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["add"] == "1")
                {
                    ltlMsg.Text = "<script>alert('添加成功')</script>";
                    return;
                }
                if (Request.QueryString["modify"] == "1")
                {
                    ltlMsg.Text = "<script>alert('修改成功')</script>";
                    return;
                }
            }

            ltlMsg.Text = "";
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            string language = "";
            CheckBox[] checkBoxes = new CheckBox[3] { chkJava, chkPython, chkCSharp };
            foreach (CheckBox checkBox in checkBoxes)
                if (checkBox.Checked == true)
                    language += checkBox.Text + ",";
            language = language.Substring(0, language.Length - 1);
            Models.Code code = new Models.Code()
            {
                Repository = txtRepository.Text,
                Url = txtUrl.Text,
                Language = language
            };
            CodeService codeService = new CodeService();
            bool exist = codeService.IsCodeExist(code.Repository);
            string message = exist ? "modify" : "add";
            bool success;
            if (fupDemo.FileName == "")
            {
                if (exist)
                    success = codeService.UpdateCode(code) == 1;
                else
                    success = codeService.InsertCode(code, null);
            }
            else
            {
                string[] name = fupDemo.FileName.Split('.');
                if (name[name.Length - 1].ToLower() != "jpg")
                {
                    ltlMsg.Text = "<script>alert('图片必须jpg')</script>";
                    return;
                }
                if (fupDemo.FileContent.Length > 1024 * 1024)
                {
                    ltlMsg.Text = "<script>alert('不能超过1M')</script>";
                    return;
                }
                if (exist)
                    success = codeService.UpdateCode(code, fupDemo.PostedFile.InputStream);
                else
                    success = codeService.InsertCode(code, fupDemo.PostedFile.InputStream);
            }
            if (success)
                Response.Redirect("~/Code/AddRepository.aspx?" + message + "=1");
            else
                ltlMsg.Text = "<script>alert('" + message + "异常')</script>";
        }
    }
}
