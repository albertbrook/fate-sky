using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FateSky
{
    public partial class EditGrade : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<TextBox> textBoxes = new List<TextBox> { txtOne, txtTwo, txtThree, txtFour, txtFive, txtSix };
                List<AnimeGrade> grades = new AnimeService().GetGrades();
                foreach (AnimeGrade animeGrade in grades)
                    textBoxes[animeGrade.Level - 1].Text = animeGrade.Depict;
            }
            ltlMsg.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            List<AnimeGrade> grades = new List<AnimeGrade>();
            List<TextBox> textBoxes = new List<TextBox> { txtOne, txtTwo, txtThree, txtFour, txtFive, txtSix };
            foreach (TextBox textBox in textBoxes)
                grades.Add(new AnimeGrade()
                {
                    Level = textBoxes.IndexOf(textBox) + 1,
                    Depict = textBox.Text
                });
            bool result = new AnimeService().SetGrades(grades);
            if (result)
                ltlMsg.Text = "<script>alert('保存成功')</script>";
            else
                ltlMsg.Text = "<script>alert('保存失败')</script>";
        }
    }
}
