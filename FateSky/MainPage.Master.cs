using DAL;
using Models;
using System;
using System.Web;
using System.Web.UI;

namespace FateSky
{
    public partial class Site1 : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User user = (User)Session["User"];
                if (user == null)
                {
                    HttpCookie cookie = Request.Cookies.Get("User");
                    if (cookie == null)
                        Response.Redirect("~/User/Login.aspx");
                    user = new User()
                    {
                        LoginName = cookie["LoginName"],
                        LoginPwd = cookie["LoginPwd"]
                    };
                    user = new UserService().UserLogin(user);
                    if (user == null)
                        Response.Redirect("~/User/Login.aspx");
                    Session["User"] = user;
                }
                ltlUser.Text = user.LoginName;
                ibtnAvatar.ImageUrl = new UserService().LoadAvatar(user.UserId);
                ltlUid.Text = user.UserId.ToString();
                ltlName.Text = user.LoginName;
                ltlMotto.Text = user.Motto.ToString();
                btnEdit.Text = "修改信息";
                btnExit.Text = "退出";
            }
        }

        protected void IbtnAvatar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/UpAvatar.aspx");
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/EditUser.aspx");
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Cookies["User"].Expires = DateTime.Now.AddDays(-1);
            Session.Abandon();
            Response.Redirect("~/User/Login.aspx");
        }
    }
}
