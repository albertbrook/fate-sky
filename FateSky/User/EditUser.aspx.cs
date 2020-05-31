using DAL;
using Models;
using System;
using System.Web;
using System.Web.UI;

namespace FateSky
{
    public partial class EditUser : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User user = (User)Session["User"];
                ibtnAvatar.ImageUrl = new UserService().LoadAvatar(user.UserId);
                ltlUid.Text = user.UserId.ToString();
                txtName.Text = user.LoginName;
                txtMotto.Text = user.Motto;
                if (Request.QueryString["modify"] == "1")
                {
                    ltlMsg.Text = "<script>alert('修改成功')</script>";
                    return;
                }
            }
            ltlMsg.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            User user = (User)Session["User"];
            string loginName = txtName.Text.Trim();
            string motto = txtMotto.Text.Trim();
            if (loginName == user.LoginName && motto == user.Motto)
            {
                ltlMsg.Text = "<script>alert('未修改')</script>";
                return;
            }
            user.LoginName = loginName;
            user.Motto = motto;
            int result = new UserService().EditUser(user);
            if (result == 1)
            {
                HttpCookie cookie = Response.Cookies["User"];
                cookie["LoginName"] = user.LoginName;
                cookie["LoginPwd"] = user.LoginPwd;
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Redirect("~/User/EditUser.aspx?modify=1");
            }
            else
                ltlMsg.Text = "<script>alert('修改异常')</script>";
        }

        protected void BtnPwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/EditPwd.aspx");
        }

        protected void IbtnAvatar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/User/UpAvatar.aspx");
        }
    }
}
