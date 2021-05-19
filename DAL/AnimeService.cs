using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace DAL
{
    public class AnimeService
    {
        /// <summary>
        /// 分组查询获取有动漫的季节，根据年月升序排序
        /// </summary>
        /// <returns>日期类集合</returns>
        public List<DateTime> GetSeason()
        {
            string sql = "select Year,Month from Animes group by Year,Month order by Year,Month";
            MySqlDataReader reader = SqlHelper.ExecuteReader(sql);
            List<DateTime> dates = new List<DateTime>();
            while (reader.Read())
            {
                int year = Convert.ToInt32(reader["Year"]);
                int month = Convert.ToInt32(reader["Month"]);
                dates.Add(new DateTime(year, month, 1));
            }
            reader.Close();
            return dates;
        }

        /// <summary>
        /// 根据年月获取相应的动漫
        /// </summary>
        /// <param name="date">年月</param>
        /// <returns>带封面的动漫扩展类集合</returns>
        public List<AnimeExtend> GetAnimesByDate(DateTime date)
        {
            string sql = "select * from AnimeCompose where Year=@Year and Month=@Month order by Title";
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@Year", date.Year),
                new MySqlParameter("@Month", date.Month)
            };
            MySqlDataReader reader = SqlHelper.ExecuteReader(sql, param);
            List<AnimeExtend> animes = new List<AnimeExtend>();
            while (reader.Read())
            {
                animes.Add(new AnimeExtend()
                {
                    Title = reader["Title"].ToString(),
                    Origin = reader["Origin"].ToString(),
                    Year = Convert.ToInt32(reader["Year"]),
                    Month = Convert.ToInt32(reader["Month"]),
                    Depict = reader["Depict"].ToString(),
                    Image = ImageHelper.ToStringImage((byte[])reader["Image"])
                });
            }
            reader.Close();
            return animes;
        }

        /// <summary>
        /// 根据标题搜索动漫
        /// </summary>
        /// <param name="name">搜索的文本</param>
        /// <returns>带封面的动漫扩展类集合</returns>
        public List<AnimeExtend> SearchAnimes(string name)
        {
            string sql = "select * from AnimeCompose where Title like '%" + name + "%' order by Year,Month";
            MySqlDataReader reader = SqlHelper.ExecuteReader(sql);
            List<AnimeExtend> animes = new List<AnimeExtend>();
            while (reader.Read())
            {
                animes.Add(new AnimeExtend()
                {
                    Title = reader["Title"].ToString(),
                    Origin = reader["Origin"].ToString(),
                    Year = Convert.ToInt32(reader["Year"]),
                    Month = Convert.ToInt32(reader["Month"]),
                    Depict = reader["Depict"].ToString(),
                    Image = ImageHelper.ToStringImage((byte[])reader["Image"])
                });
            }
            reader.Close();
            return animes;
        }

        /// <summary>
        /// 根据标题获取相应的动漫信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>带封面的动漫扩展类</returns>
        public AnimeExtend GetAnimeByTitle(string title)
        {
            string sql = "select * from AnimeCompose where Title=@Title";
            MySqlParameter[] param = new MySqlParameter[] {
                new MySqlParameter("@Title", title)
            };
            MySqlDataReader reader = SqlHelper.ExecuteReader(sql, param);
            reader.Read();
            AnimeExtend animeExtend = new AnimeExtend()
            {
                Title = reader["Title"].ToString(),
                Origin = reader["Origin"].ToString(),
                Year = Convert.ToInt32(reader["Year"]),
                Month = Convert.ToInt32(reader["Month"]),
                Depict = reader["Depict"].ToString(),
                Image = ImageHelper.ToStringImage((byte[])reader["Image"])
            };
            reader.Close();
            return animeExtend;
        }

        /// <summary>
        /// 根据标题删除动漫和封面
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>是否成功</returns>
        public bool DelAnime(string title)
        {
            string[] sql = new string[]
            {
                "delete from AnimePages where Title=@Title",
                "delete from Animes where Title=@Title"
            };
            MySqlParameter[] param = new MySqlParameter[] {
                new MySqlParameter("@Title", title)
            };
            return SqlHelper.ExecuteTransaction(sql, param);
        }

        /// <summary>
        /// 插入动漫和封面
        /// </summary>
        /// <param name="anime">动漫类</param>
        /// <param name="stream">输入流</param>
        /// <returns>是否成功</returns>
        public bool AddAnime(Anime anime, Stream stream)
        {
            string[] sql = new string[]
            {
                "insert into Animes(Title,Origin,Year,Month,Level) values(@Title,@Origin,@Year,@Month,@Level)",
                "insert into AnimePages values(@Title, @Image)"
            };
            stream = stream ?? new MemoryStream();
            MySqlParameter[] param = new MySqlParameter[] {
                new MySqlParameter("@Title", anime.Title),
                new MySqlParameter("@Origin", anime.Origin),
                new MySqlParameter("@Year", anime.Year),
                new MySqlParameter("@Month", anime.Month),
                new MySqlParameter("@Level", anime.Level),
                new MySqlParameter("Image", ImageHelper.ToByteStream(stream))
            };
            return SqlHelper.ExecuteTransaction(sql, param);
        }

        /// <summary>
        /// 更新动漫信息
        /// </summary>
        /// <param name="anime">动漫类</param>
        /// <param name="oldTitle">修改前的标题</param>
        /// <returns>是否成功</returns>
        public bool UpdateAnime(Anime anime, string oldTitle)
        {
            string[] sql = new string[]
            {
                "update Animes set Title=@Title,Origin=@Origin,Year=@Year,Month=@Month,Level=@Level where Title=@OldTitle",
                "update AnimePages set Title=@Title where Title=@OldTitle"
            };
            MySqlParameter[] param = new MySqlParameter[] {
                new MySqlParameter("@Title", anime.Title),
                new MySqlParameter("@Origin", anime.Origin),
                new MySqlParameter("@Year", anime.Year),
                new MySqlParameter("@Month", anime.Month),
                new MySqlParameter("@Level", anime.Level),
                new MySqlParameter("@OldTitle", oldTitle)
            };
            return SqlHelper.ExecuteTransaction(sql, param);
        }

        /// <summary>
        /// 更新动漫信息和封面
        /// </summary>
        /// <param name="anime">动漫类</param>
        /// <param name="oldTitle">修改前的标题</param>
        /// <param name="stream">输入流</param>
        /// <returns>是否成功</returns>
        public bool UpdateAnime(Anime anime, string oldTitle, Stream stream)
        {
            string[] sql = new string[]
            {
                "update Animes set Title=@Title,Origin=@Origin,Year=@Year,Month=@Month,Level=@Level where Title=@OldTitle",
                "update AnimePages set Title=@Title,Image=@Image where Title=@OldTitle"
            };
            stream = stream ?? new MemoryStream();
            MySqlParameter[] param = new MySqlParameter[] {
                new MySqlParameter("@Title", anime.Title),
                new MySqlParameter("@Origin", anime.Origin),
                new MySqlParameter("@Year", anime.Year),
                new MySqlParameter("@Month", anime.Month),
                new MySqlParameter("@Level", anime.Level),
                new MySqlParameter("@OldTitle", oldTitle),
                new MySqlParameter("Image", ImageHelper.ToByteStream(stream))
            };
            return SqlHelper.ExecuteTransaction(sql, param);
        }

        /// <summary>
        /// 获取所有动漫评级
        /// </summary>
        /// <returns>动漫评级类集合</returns>
        public List<AnimeGrade> GetGrades()
        {
            string sql = "select * from AnimeGrade";
            List<AnimeGrade> grades = new List<AnimeGrade>();
            MySqlDataReader reader = SqlHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                grades.Add(new AnimeGrade()
                {
                    Level = Convert.ToInt32(reader["Level"]),
                    Depict = reader["Depict"].ToString()
                });
            }
            return grades;
        }

        /// <summary>
        /// 修改所有动漫评级
        /// </summary>
        /// <param name="grades">动漫评级类集合</param>
        /// <returns>是否成功</returns>
        public bool SetGrades(List<AnimeGrade> grades)
        {
            string sql = "update AnimeGrade set Depict=@Depict where Level=@Level";
            List<MySqlParameter[]> list = new List<MySqlParameter[]>();
            foreach (AnimeGrade grade in grades)
                list.Add(new MySqlParameter[]
                {
                    new MySqlParameter("@Depict", grade.Depict),
                    new MySqlParameter("@Level", grade.Level)
                });
            MySqlParameter[][] param = list.ToArray();
            return SqlHelper.ExecuteTransaction(sql, param);
        }
    }
}
