using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace FateSky
{
    public partial class SetAnime : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 2000; i <= DateTime.Now.Year + 2; i++)
                    dropYear.Items.Add(i.ToString());
                for (int i = 1; i < 12; i += 3)
                    dropMonth.Items.Add(i.ToString());
                List<AnimeGrade> animeExtends = new AnimeService().GetGrades();
                foreach (AnimeGrade animeGrade in animeExtends)
                    dropDepict.Items.Add(animeGrade.Depict);
                string title = HttpUtility.UrlDecode(Request.QueryString["title"]);
                if (title == null)
                {
                    btnSave.Text = "添加动漫";
                    imgPage.Visible = false;
                }
                else
                {
                    btnSave.Text = "保存修改";
                    AnimeExtend anime = new AnimeService().GetAnimeByTitle(title);
                    imgPage.ImageUrl = anime.Image;
                    txtTitle.Text = anime.Title;
                    txtOrigin.Text = anime.Origin;
                    dropYear.SelectedValue = anime.Year.ToString();
                    dropMonth.SelectedValue = anime.Month.ToString();
                    dropDepict.SelectedValue = anime.Depict;
                }
                if (Request.QueryString["add"] == "1")
                {
                    ltlMsg.Text = "<script>alert('添加成功')</script>";
                    return;
                }
                if (Request.QueryString["modify"] == "1")
                {
                    ltlMsg.Text = "<script>alert('保存成功')</script>";
                    return;
                }
            }
            ltlMsg.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["title"] == null)
                Save();
            else
                Edit();
        }

        private void Save()
        {
            Models.Anime anime = new Models.Anime()
            {
                Title = txtTitle.Text.Trim(),
                Origin = txtOrigin.Text.Trim(),
                Year = Convert.ToInt32(dropYear.SelectedValue),
                Month = Convert.ToInt32(dropMonth.SelectedValue),
                Level = dropDepict.SelectedIndex + 1
            };
            bool success;
            if (fupPage.FileName == "")
                success = new AnimeService().AddAnime(anime, null);
            else
            {
                string[] name = fupPage.FileName.Split('.');
                if (name[name.Length - 1].ToLower() != "jpg")
                {
                    ltlMsg.Text = "<script>alert('图片必须jpg')</script>";
                    return;
                }
                if (fupPage.FileContent.Length > 1024 * 1024)
                {
                    ltlMsg.Text = "<script>alert('不能超过1M')</script>";
                    return;
                }
                success = new AnimeService().AddAnime(anime, fupPage.PostedFile.InputStream);
            }
            if (success)
                Response.Redirect("~/Anime/SetAnime.aspx?add=1");
            else
                ltlMsg.Text = "<script>alert('添加异常')</script>";
        }

        private void Edit()
        {
            Models.Anime anime = new Models.Anime()
            {
                Title = txtTitle.Text.Trim(),
                Origin = txtOrigin.Text.Trim(),
                Year = Convert.ToInt32(dropYear.SelectedValue),
                Month = Convert.ToInt32(dropMonth.SelectedValue),
                Level = dropDepict.SelectedIndex + 1
            };
            string oldTitle = HttpUtility.UrlDecode(Request.QueryString["title"]);
            bool success;
            if (fupPage.FileName == "")
                success = new AnimeService().UpdateAnime(anime, oldTitle);
            else
            {
                string[] name = fupPage.FileName.Split('.');
                if (name[name.Length - 1].ToLower() != "jpg")
                {
                    ltlMsg.Text = "<script>alert('图片必须jpg')</script>";
                    return;
                }
                if (fupPage.FileContent.Length > 1024 * 1024)
                {
                    ltlMsg.Text = "<script>alert('不能超过1M')</script>";
                    return;
                }
                success = new AnimeService().UpdateAnime(anime, oldTitle, fupPage.PostedFile.InputStream);
            }
            if (success)
                Response.Redirect(string.Format("~/Anime/SetAnime.aspx?title={0}&modify={1}", HttpUtility.UrlEncode(anime.Title), 1));
            else
                ltlMsg.Text = "<script>alert('保存异常')</script>";
        }
    }
}
