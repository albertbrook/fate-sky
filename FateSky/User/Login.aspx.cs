using DAL;
using Models;
using System;
using System.Web;
using System.Web.UI;

namespace FateSky
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                    Response.Redirect("~/Default.aspx");
                txtUser.Text = "用户名";
                txtPwd.Text = "密码";
                chkRem.Text = "自动登入";
                btnSignIn.Text = "登入";
                btnSignOut.Text = "注册";
            }
            ltlMsg.Text = "";
        }

        protected void BtnSignIn_Click(object sender, EventArgs e)
        {
            string loginName = txtUser.Text.Trim().ToLower();
            string loginPwd = txtPwd.Text.Trim();
            txtUser.Text = "用户名";
            txtPwd.Text = "密码";
            if (loginName == "用户名" || loginPwd == "密码")
            {
                ltlMsg.Text = "<script>alert('用户名和密码不能为空!')</script>";
                return;
            }
            User user = new User()
            {
                LoginName = loginName,
                LoginPwd = loginPwd
            };
            user = new UserService().UserLogin(user);
            if (user != null)
            {
                if (chkRem.Checked)
                {
                    HttpCookie cookie = new HttpCookie("User")
                    {
                        Expires = DateTime.Now.AddDays(1)
                    };
                    cookie.Values.Add("LoginName", user.LoginName);
                    cookie.Values.Add("LoginPwd", user.LoginPwd);
                    Response.Cookies.Add(cookie);
                }
                Session["User"] = user;
                Response.Redirect("~/Default.aspx");
            }
            ltlMsg.Text = "<script>alert('用户名或密码错误!')</script>";
        }
    }
}
