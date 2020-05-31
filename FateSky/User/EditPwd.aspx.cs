using DAL;
using Models;
using System;
using System.Web;
using System.Web.UI;

namespace FateSky
{
    public partial class EditPwd : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltlMsg.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            User user = (User)Session["User"];
            string oldPwd = txtOldPwd.Text.Trim();
            if (oldPwd == "" || oldPwd != user.LoginPwd)
            {
                ltlMsg.Text = "<script>alert('密码错误')</script>";
                return;
            }
            string newPwd = txtNewPwd.Text.Trim();
            if (newPwd == "")
            {
                ltlMsg.Text = "<script>alert('密码不能为空')</script>";
                return;
            }
            string chkPwd = txtChkPwd.Text.Trim();
            if (newPwd != chkPwd)
            {
                ltlMsg.Text = "<script>alert('密码不一致')</script>";
                return;
            }
            user.LoginPwd = newPwd;
            int result = new UserService().ModifyPwd(user);
            if (result == 1)
            {
                HttpCookie cookie = Response.Cookies["User"];
                cookie["LoginName"] = user.LoginName;
                cookie["LoginPwd"] = user.LoginPwd;
                cookie.Expires = DateTime.Now.AddDays(1);
                ltlMsg.Text = "<script>alert('修改成功')</script>";
            }
            else
                ltlMsg.Text = "<script>alert('修改异常')</script>";
        }
    }
}
