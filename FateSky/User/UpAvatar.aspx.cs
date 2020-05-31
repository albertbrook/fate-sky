using DAL;
using Models;
using System;
using System.IO;
using System.Web.UI;

namespace FateSky
{
    public partial class UpAvatar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                int userId = ((User)Session["User"]).UserId;
                string image = new UserService().LoadAvatar(userId);
                imgAvatar.ImageUrl = image;
            }
            ltlMsg.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!fupAvatar.HasFile)
            {
                ltlMsg.Text = "<script>alert('请选择文件')</script>";
                return;
            }
            string[] name = fupAvatar.FileName.Split('.');
            if (name[name.Length - 1].ToLower() != "jpg")
            {
                ltlMsg.Text = "<script>alert('图片必须jpg')</script>";
                return;
            }
            if (fupAvatar.FileContent.Length > 1024 * 1024)
            {
                ltlMsg.Text = "<script>alert('不能超过1M')</script>";
                return;
            }
            try
            {
                int userId = ((User)Session["User"]).UserId;
                Stream stream = fupAvatar.PostedFile.InputStream;
                int result = new UserService().SaveAvatar(userId, stream);
                if (result == 1)
                    ltlMsg.Text = "<script>alert('保存成功')</script>";
                else
                    throw new Exception("数据异常");
            }
            catch (Exception ex)
            {
                ltlMsg.Text = "<script>alert('图片上传失败\\n" + ex.Message + "')</script>";
            }
        }
    }
}
