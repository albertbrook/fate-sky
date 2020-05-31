using Models;
using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace DAL
{
    public class UserService
    {
        /// <summary>
        /// 根据登入名和密码查询用户
        /// </summary>
        /// <param name="user">登入名和密码</param>
        /// <returns>查询成功返回赋值过的用户id和个性签名，否则null</returns>
        public User UserLogin(User user)
        {
            string sql = "select UserId,Motto from Users where LoginName=@LoginName and LoginPwd=@LoginPwd";
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@LoginName", user.LoginName),
                new MySqlParameter("@LoginPwd", user.LoginPwd)
            };
            MySqlDataReader reader = SqlHelper.ExecuteReader(sql, param);
            if (!reader.HasRows)
                return null;
            reader.Read();
            user.UserId = Convert.ToInt32(reader["UserId"]);
            user.Motto = reader["Motto"].ToString();
            reader.Close();
            return user;
        }

        /// <summary>
        /// 根据用户修改用户信息
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>影响行数</returns>
        public int EditUser(User user)
        {
            string sql = "update Users set LoginName=@LoginName,Motto=@Motto where UserId=@UserId";
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@LoginName", user.LoginName),
                new MySqlParameter("@Motto", user.Motto),
                new MySqlParameter("@UserId", user.UserId)
            };
            return SqlHelper.ExecuteNonQuery(sql, param);
        }

        /// <summary>
        /// 根据用户修改密码
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns>影响行数</returns>
        public int ModifyPwd(User user)
        {
            string sql = "update Users set LoginPwd=@LoginPwd where UserId=@UserId";
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@LoginPwd", user.LoginPwd),
                new MySqlParameter("@UserId", user.UserId)
            };
            return SqlHelper.ExecuteNonQuery(sql, param);
        }

        /// <summary>
        /// 根据用户id插入或修改头像
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="stream">图片输入流</param>
        /// <returns>影响行数</returns>
        public int SaveAvatar(int userId, Stream stream)
        {
            string sql = "select count(UserId) from UserAvatars where UserId=@UserId";
            stream = stream ?? new MemoryStream();
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@UserId", userId),
                new MySqlParameter("@Image", ImageHelper.ToByteStream(stream))
            };
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(sql, param)) == 0)
                sql = "insert into UserAvatars values(@UserId, @Image)";
            else
                sql = "update UserAvatars set Image=@Image where UserId=@UserId";
            return SqlHelper.ExecuteNonQuery(sql, param);
        }

        /// <summary>
        /// 根据用户id查询头像
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>图片文本</returns>
        public string LoadAvatar(int userId)
        {
            string sql = "select Image from UserAvatars where UserId=@UserId";
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@UserId", userId)
            };
            byte[] image = (byte[])SqlHelper.ExecuteScalar(sql, param);
            image = image ?? new byte[0];
            return ImageHelper.ToStringImage(image);
        }
    }
}
