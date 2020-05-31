using DAL;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FateSky
{
    public partial class Anime : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<DateTime> dates = new AnimeService().GetSeason();
                for (int i = 0; i < dates.Count; i++)
                    lstDate.Items.Add(dates[i].ToString("yyyy年MM月"));
                if (lstDate.Items.Count > 0)
                {
                    lstDate.Items[0].Selected = true;
                    LstDate_SelectedIndexChanged(null, null);
                }
            }
            ltlMsg.Text = "";
        }

        protected void LstDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(lstDate.SelectedValue);
            rptAnime.DataSource = new AnimeService().GetAnimesByDate(date);
            rptAnime.DataBind();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string name = txtSearch.Text.Trim();
            rptAnime.DataSource = new AnimeService().SearchAnimes(name);
            rptAnime.DataBind();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            string title = ((Button)sender).CommandArgument;
            Response.Redirect("~/Anime/SetAnime.aspx?title=" + HttpUtility.UrlEncode(title));
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            string title = ((Button)sender).CommandArgument;
            bool result = new AnimeService().DelAnime(title);
            if (result)
                ltlMsg.Text = "<script>alert('删除成功')</script>";
            else
                ltlMsg.Text = "<script>alert('删除异常')</script>";
            List<DateTime> dates = new AnimeService().GetSeason();
            int index = lstDate.SelectedIndex;
            lstDate.Items.Clear();
            for (int i = 0; i < dates.Count; i++)
                lstDate.Items.Add(dates[i].ToString("yyyy年MM月"));
            if (dates.Count == 0)
            {
                rptAnime.DataSource = null;
                rptAnime.DataBind();
                return;
            }
            if (index > dates.Count - 1)
                index--;
            lstDate.Items[index].Selected = true;
            LstDate_SelectedIndexChanged(null, null);
        }
    }
}
