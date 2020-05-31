# DAL
> 数据访问层

# Helper

## SqlHelper

### 无参sql

```
public static int ExecuteNonQuery(string sql)
public static bool ExecuteTransaction(string[] sql)
public static object ExecuteScalar(string sql)
public static MySqlDataReader ExecuteReader(string sql)
```

### 带参sql

```
public static int ExecuteNonQuery(string sql, MySqlParameter[] param)
public static bool ExecuteTransaction(string sql, MySqlParameter[][] param)
public static bool ExecuteTransaction(string[] sql, MySqlParameter[] param)
public static object ExecuteScalar(string sql, MySqlParameter[] param)
public static MySqlDataReader ExecuteReader(string sql, MySqlParameter[] param)
```

### 带参存储过程

```
public static int ExecuteNonQuery(string sql, MySqlParameter[] param, bool IsProcedure)
public static bool ExecuteTransaction(string sql, MySqlParameter[][] param, bool IsProcedure)
public static bool ExecuteTransaction(string[] sql, MySqlParameter[] param, bool IsProcedure)
public static object ExecuteScalar(string sql, MySqlParameter[] param, bool IsProcedure)
public static MySqlDataReader ExecuteReader(string sql, MySqlParameter[] param, bool IsProcedure)
```

## ImageHelper

```
/// <summary>
/// 将流转换成图片字节流
/// </summary>
public static byte[] ToByteStream(Stream stream)
{
    byte[] image = new byte[stream.Length];
    stream.Read(image, 0, image.Length);
    return image;
}
```

```
/// <summary>
/// 将字节流转换成可解析的图片文字
/// </summary>
public static string ToStringImage(byte[] image)
{
    MemoryStream stream = new MemoryStream(image);
    string base64 = Convert.ToBase64String(stream.ToArray());
    return "data:image/jpg;base64," + base64;
}
```

## LogHelper

```
/// <summary>
/// 将信息写入日志
/// </summary>
public static void WriteLog(string msg)
{
    FileStream fs = new FileStream("/var/log/fatesky.log", FileMode.Append);
    StreamWriter sw = new StreamWriter(fs);
    sw.WriteLine(DateTime.Now.ToString() + " " + msg);
    sw.Close();
    fs.Close();
}
```

# Service

## UserService

---

```
/// <summary>
/// 根据登入名和密码查询用户
/// </summary>
/// <param name="user">登入名和密码</param>
/// <returns>查询成功返回赋值过的用户id和个性签名，否则null</returns>
public User UserLogin(User user)
```

* 根据登入名和密码和密码获取用户id和签名
* 获取失败返回null
* 获取成功将用户id和签名赋值后返回

---

```
/// <summary>
/// 根据用户修改用户信息
/// </summary>
/// <param name="user">用户</param>
/// <returns>影响行数</returns>
public int EditUser(User user)
```

* 根据用户id修改用户信息
* 返回影响行数

---

```
/// <summary>
/// 根据用户修改密码
/// </summary>
/// <param name="user">用户</param>
/// <returns>影响行数</returns>
public int ModifyPwd(User user)
```

* 根据用户id修改密码
* 返回影响行数

---

```
/// <summary>
/// 根据用户id插入或修改头像
/// </summary>
/// <param name="userId">用户id</param>
/// <param name="stream">图片输入流</param>
/// <returns>影响行数</returns>
public int SaveAvatar(int userId, Stream stream)
```

* 根据用户id查询是否存在头像
* 不存在插入头像
* 存在更新头像

---

```
/// <summary>
/// 根据用户id查询头像
/// </summary>
/// <param name="userId">用户id</param>
/// <returns>图片文本</returns>
public string LoadAvatar(int userId)
```

* 根据用户id查询图片
* 如果查询失败新建字节数组防止异常
* 返回可以解析的图片文本

## CodeService

---

```
/// <summary>
/// 获取所有项目
/// </summary>
/// <returns>带演示图的代码扩展类</returns>
public List<CodeExtend> GetAllCodes()
```

* 通过视图查询带演示图的所有项目
* 返回代码扩展类集合

---

```
/// <summary>
/// 根据语言获取项目
/// </summary>
/// <param name="language">语言</param>
/// <returns>带图片的代码扩展类</returns>
public List<CodeExtend> GetCodesByLanguage(string language)
```

* 通过视图查询带演示图的对应语言项目
* 返回代码扩展类集合

---

```
/// <summary>
/// 判断仓库是否已经存在
/// </summary>
/// <param name="repository">仓库名</param>
/// <returns>是否存在</returns>
public bool IsCodeExist(string repository)
```

* 根据仓库名查询数据库中的数量
* 如果为1返回true，0返回false，其它数据库异常

---

```
/// <summary>
/// 插入仓库和演示图
/// </summary>
/// <param name="code">仓库实体类</param>
/// <param name="stream">演示图输入流</param>
/// <returns>是否插入成功</returns>
public bool InsertCode(Code code, Stream stream)
```

* 使用事务插入仓库和演示图
* 如果演示图输入流为null则建立一个防止出错
* 返回事务是否提交成功

---

```
/// <summary>
/// 更新仓库信息
/// </summary>
/// <param name="code">仓库实体类</param>
/// <returns>影响行数</returns>
public int UpdateCode(Code code)
```

* 只更新仓库信息
* 返回影响行数

---

```
/// <summary>
/// 更新仓库信息和演示图
/// </summary>
/// <param name="code">仓库实体类</param>
/// <param name="stream">演示图输入流</param>
/// <returns>是否更新成功</returns>
public bool UpdateCode(Code code, Stream stream)
```

* 更新仓库信息和演示图
* 演示图不应该传入null，为了防止出错加一个判断
* 返回影响行数

## AnimeService

---

```
/// <summary>
/// 分组查询获取有动漫的季节，根据年月升序排序
/// </summary>
/// <returns>日期类集合</returns>
public List<DateTime> GetSeason()
```

* 分组查询有动漫的季节
* 根据年月升序排序
* 返回日期类集合

---

```
/// <summary>
/// 根据年月获取相应的动漫
/// </summary>
/// <param name="date">年月</param>
/// <returns>带封面的动漫扩展类集合</returns>
public List<AnimeExtend> GetAnimesByDate(DateTime date)
```

* 根据年月通过视图查询动漫和动漫封面
* 返回动漫扩展类集合

---

```
/// <summary>
/// 根据标题搜索动漫
/// </summary>
/// <param name="name">搜索的文本</param>
/// <returns>带封面的动漫扩展类集合</returns>
public List<AnimeExtend> SearchAnimes(string name)
```

* 根据标题通过视图搜索符合条件的动漫
* 返回动漫扩展类集合

---

```
/// <summary>
/// 根据标题获取相应的动漫信息
/// </summary>
/// <param name="title">标题</param>
/// <returns>带封面的动漫扩展类</returns>
public AnimeExtend GetAnimeByTitle(string title)
```

* 根据标题通过视图查询动漫和封面
* 返回动漫扩展类

---

```
/// <summary>
/// 根据标题删除动漫和封面
/// </summary>
/// <param name="title">标题</param>
/// <returns>是否成功</returns>
public bool DelAnime(string title)
```

* 根据标题删除动漫和封面
* 返回是否成功

---

```
/// <summary>
/// 插入动漫和封面
/// </summary>
/// <param name="anime">动漫类</param>
/// <param name="stream">输入流</param>
/// <returns>是否成功</returns>
public bool AddAnime(Anime anime, Stream stream)
```

* 插入动漫和封面
* 如果输入流为null则新建一个防止出错
* 返回是否成功

---

```
/// <summary>
/// 更新动漫信息
/// </summary>
/// <param name="anime">动漫类</param>
/// <param name="oldTitle">修改前的标题</param>
/// <returns>是否成功</returns>
public bool UpdateAnime(Anime anime, string oldTitle)
```

* 通过事务根据旧标题修改动漫信息
* 旧标题和当前标题可能不同，同时修改数据库中封面对应的标题
* 返回是否成功

---

```
/// <summary>
/// 更新动漫信息和封面
/// </summary>
/// <param name="anime">动漫类</param>
/// <param name="oldTitle">修改前的标题</param>
/// <param name="stream">输入流</param>
/// <returns>是否成功</returns>
public bool UpdateAnime(Anime anime, string oldTitle, Stream stream)
```

* 通过事务根据旧标题修改动漫信息和封面
* 如果输入流为null则新建一个防止出错
* 旧标题和当前标题可能不同，同时修改数据库中封面对应的标题
* 返回是否成功

---

```
/// <summary>
/// 获取所有动漫评级
/// </summary>
/// <returns>动漫评级类集合</returns>
public List<AnimeGrade> GetGrades()
```

* 获取所有动漫评级
* 返回动漫评级类集合

---

```
/// <summary>
/// 修改所有动漫评级
/// </summary>
/// <param name="grades">动漫评级类集合</param>
/// <returns>是否成功</returns>
public bool SetGrades(List<AnimeGrade> grades)
```

* 通过事务修改所有动漫评级
* 返回是否成功

## NoteService

---

```
/// <summary>
/// 获取所有笔记
/// </summary>
/// <returns>笔记类集合</returns>
public List<Note> GetAllNotes()
```

* 获取所有笔记
* 根据插入时间降序排序
* 返回笔记类集合

---

```
/// <summary>
/// 插入笔记
/// </summary>
/// <param name="text">笔记内容</param>
/// <returns>影响行数</returns>
public int InsertNote(string text)
```

* 插入笔记
* 返回影响行数

---

```
/// <summary>
/// 根据插入时间删除笔记
/// </summary>
/// <param name="updateTime">插入时间</param>
/// <returns>影响行数</returns>
public int DeleteNote(DateTime updateTime)
```

* 根据插入时间删除笔记
* 返回影响行数
