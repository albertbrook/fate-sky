using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class NoteService
    {
        /// <summary>
        /// 获取所有笔记
        /// </summary>
        /// <returns>笔记类集合</returns>
        public List<Note> GetAllNotes()
        {
            string sql = "select * from Notes order by UpdateTime desc";
            MySqlDataReader reader = SqlHelper.ExecuteReader(sql);
            List<Note> notes = new List<Note>();
            while (reader.Read())
            {
                notes.Add(new Note()
                {
                    Text = reader["Text"].ToString(),
                    UpdateTime = Convert.ToDateTime(reader["UpdateTime"])
                });
            }
            reader.Close();
            return notes;
        }

        /// <summary>
        /// 插入笔记
        /// </summary>
        /// <param name="text">笔记内容</param>
        /// <returns>影响行数</returns>
        public int InsertNote(string text)
        {
            string sql = "insert into Notes(Text) values(@Text)";
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@Text", text)
            };
            return SqlHelper.ExecuteNonQuery(sql, param);
        }

        /// <summary>
        /// 根据插入时间删除笔记
        /// </summary>
        /// <param name="updateTime">插入时间</param>
        /// <returns>影响行数</returns>
        public int DeleteNote(DateTime updateTime)
        {
            string sql = "delete from Notes where UpdateTime=@UpdateTime";
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@UpdateTime", updateTime)
            };
            return SqlHelper.ExecuteNonQuery(sql, param);
        }
    }
}
