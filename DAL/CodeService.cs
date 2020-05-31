using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace DAL
{
    public class CodeService
    {
        /// <summary>
        /// 获取所有项目
        /// </summary>
        /// <returns>带图片的代码扩展类</returns>
        public List<CodeExtend> GetAllCodes()
        {
            string sql = "select * from CodeCompose";
            MySqlDataReader reader = SqlHelper.ExecuteReader(sql);
            List<CodeExtend> codes = new List<CodeExtend>();
            while (reader.Read())
            {
                codes.Add(new CodeExtend()
                {
                    Repository = reader["Repository"].ToString(),
                    Url = reader["Url"].ToString(),
                    Image = ImageHelper.ToStringImage((byte[])reader["Image"])
                });
            }
            reader.Close();
            return codes;
        }

        /// <summary>
        /// 根据语言获取项目
        /// </summary>
        /// <param name="language">语言</param>
        /// <returns>带图片的代码扩展类</returns>
        public List<CodeExtend> GetCodesByLanguage(string language)
        {
            string sql = "select * from CodeCompose where find_in_set(@Language,Language)";
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@Language", language)
            };
            MySqlDataReader reader = SqlHelper.ExecuteReader(sql, param);
            List<CodeExtend> codes = new List<CodeExtend>();
            while (reader.Read())
            {
                codes.Add(new CodeExtend()
                {
                    Repository = reader["Repository"].ToString(),
                    Url = reader["Url"].ToString(),
                    Image = ImageHelper.ToStringImage((byte[])reader["Image"])
                });
            }
            reader.Close();
            return codes;
        }

        /// <summary>
        /// 判断仓库是否已经存在
        /// </summary>
        /// <param name="repository">仓库名</param>
        /// <returns>是否存在</returns>
        public bool IsCodeExist(string repository)
        {
            string sql = "select count(Repository) from Codes where Repository=@Repository";
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@Repository", repository)
            };
            object result = SqlHelper.ExecuteScalar(sql, param);
            return Convert.ToInt32(result) == 1;
        }

        /// <summary>
        /// 插入仓库和演示图
        /// </summary>
        /// <param name="code">仓库实体类</param>
        /// <param name="stream">演示图输入流</param>
        /// <returns>是否插入成功</returns>
        public bool InsertCode(Code code, Stream stream)
        {
            string[] sql = new string[]
            {
                "insert into Codes(Repository,Url,Language) values(@Repository,@Url,@Language)",
                "insert into CodeDemos values(@Repository,@Image)"
            };
            stream = stream ?? new MemoryStream();
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@Repository", code.Repository),
                new MySqlParameter("@Url", code.Url),
                new MySqlParameter("@Language", code.Language),
                new MySqlParameter("@Image", ImageHelper.ToByteStream(stream))
            };
            return SqlHelper.ExecuteTransaction(sql, param);
        }

        /// <summary>
        /// 更新仓库信息
        /// </summary>
        /// <param name="code">仓库实体类</param>
        /// <returns>影响行数</returns>
        public int UpdateCode(Code code)
        {
            string sql = "update Codes set Url=@Url,Language=@Language where Repository=@Repository";
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@Repository", code.Repository),
                new MySqlParameter("@Url", code.Url),
                new MySqlParameter("@Language", code.Language),
            };
            return SqlHelper.ExecuteNonQuery(sql, param);
        }

        /// <summary>
        /// 更新仓库信息和演示图
        /// </summary>
        /// <param name="code">仓库实体类</param>
        /// <param name="stream">演示图输入流</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateCode(Code code, Stream stream)
        {
            string[] sql = new string[]
            {
                "update Codes set Url=@Url,Language=@Language where Repository=@Repository",
                "update CodeDemos set Image=@Image where Repository=@Repository"
            };
            stream = stream ?? new MemoryStream();
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@Repository", code.Repository),
                new MySqlParameter("@Url", code.Url),
                new MySqlParameter("@Language", code.Language),
                new MySqlParameter("Image", ImageHelper.ToByteStream(stream))
            };
            return SqlHelper.ExecuteTransaction(sql, param);
        }
    }
}
