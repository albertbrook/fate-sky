# FateSky
> 数据库信息

# 表

## Users
> 用户

| Field     | Type         | Null | Key | Default | Extra          |
| -         | -            | -    | -   | -       | -              |
| UserId    | int          | NO   | PRI | NULL    | auto_increment |
| LoginName | varchar(50)  | NO   |     | NULL    |                |
| LoginPwd  | varchar(200) | NO   |     | NULL    |                |
| Motto     | varchar(200) | YES  |     | NULL    |                |

```
DROP TABLE IF EXISTS Users;
CREATE TABLE Users (
	UserId INT PRIMARY KEY AUTO_INCREMENT,
	LoginName VARCHAR(50) NOT NULL,
	LoginPwd VARCHAR(200) NOT NULL,
	Motto VARCHAR(200)
);
```

## UserAvatars
> 用户头像

| Field  | Type       | Null | Key | Default | Extra |
| -      | -          | -    | -   | -       | -     |
| UserId | int        | NO   | MUL | NULL    |       |
| Image  | mediumblob | YES  |     | NULL    |       |

```
DROP TABLE IF EXISTS UserAvatars;
CREATE TABLE UserAvatars (
    UserId INT NOT NULL,
    Image MEDIUMBLOB,
    CONSTRAINT ua_ibfk_user_id FOREIGN KEY (UserId) REFERENCES Users (UserId)
);
```

## Codes
> 代码

| Field      | Type         | Null | Key | Default | Extra |
| -          | -            | -    | -   | -       | -     |
| Repository | varchar(50)  | NO   | PRI | NULL    |       |
| Url        | varchar(200) | YES  |     | NULL    |       |
| Language   | varchar(50)  | NO   |     | NULL    |       |

```
DROP TABLE IF EXISTS Codes;
CREATE TABLE Codes (
	Repository VARCHAR(50) PRIMARY KEY,
	Url VARCHAR(200),
	Language VARCHAR(50) NOT NULL
);
```

## CodeDemos
> 代码演示图

| Field      | Type        | Null | Key | Default | Extra |
| -          | -           | -    | -   | -       | -     |
| Repository | varchar(50) | NO   | MUL | NULL    |       |
| Image      | mediumblob  | YES  |     | NULL    |       |

```
DROP TABLE IF EXISTS CodeDemos;
CREATE TABLE CodeDemos (
    Repository VARCHAR(50) NOT NULL,
    Image MEDIUMBLOB,
    CONSTRAINT cd_ibfk_repository FOREIGN KEY (Repository) REFERENCES Codes (Repository)
);
```

## Animes
> 动漫

| Field  | Type         | Null | Key | Default | Extra |
| -      | -            | -    | -   | -       | -     |
| Title  | varchar(100) | NO   | PRI | NULL    |       |
| Origin | varchar(100) | YES  |     | NULL    |       |
| Year   | int          | NO   |     | NULL    |       |
| Month  | int          | NO   |     | NULL    |       |
| Level  | int          | YES  |     | NULL    |       |

```
DROP TABLE IF EXISTS Animes;
CREATE TABLE Animes (
	Title VARCHAR(100) PRIMARY KEY,
	Origin VARCHAR(100),
	Year INT NOT NULL,
	Month INT NOT NULL,
	Level INT,
	CONSTRAINT a_chk_month CHECK (Month IN (1,4,7,10)),
	CONSTRAINT a_chk_level CHECK (Level >= 1 AND Level <= 6)
);
```

## AnimePages
> 动漫封面

| Field | Type         | Null | Key | Default | Extra |
| -     | -            | -    | -   | -       | -     |
| Title | varchar(100) | NO   | MUL | NULL    |       |
| Image | mediumblob   | YES  |     | NULL    |       |

```
DROP TABLE IF EXISTS AnimePages;
CREATE TABLE AnimePages (
    Title VARCHAR(100) NOT NULL,
    Image MEDIUMBLOB,
    CONSTRAINT ap_ibfk_title FOREIGN KEY (Title) REFERENCES Animes (Title)
);
```

## AnimeGrade
> 动漫评级

| Field  | Type         | Null | Key | Default | Extra |
| -      | -            | -    | -   | -       | -     |
| Level  | int          | NO   |     | NULL    |       |
| Depict | varchar(200) | NO   |     | NULL    |       |

```
DROP TABLE IF EXISTS AnimeGrade;
CREATE TABLE AnimeGrade (
    Level INT NOT NULL,
    Depict VARCHAR(200) NOT NULL,
    CONSTRAINT ag_chk_level CHECK (Level >= 1 AND Level <= 6)
);
INSERT INTO AnimeGrade VALUES(1, "不行");
INSERT INTO AnimeGrade VALUES(2, "还行");
INSERT INTO AnimeGrade VALUES(3, "可以");
INSERT INTO AnimeGrade VALUES(4, "挺好");
INSERT INTO AnimeGrade VALUES(5, "不错");
INSERT INTO AnimeGrade VALUES(6, "很棒");
```

## Notes
> 记事本

| Field      | Type     | Null | Key | Default           | Extra             |
| -          | -        | -    | -   | -                 | -                 |
| Text       | text     | NO   |     | NULL              |                   |
| UpdateTime | datetime | NO   |     | CURRENT_TIMESTAMP | DEFAULT_GENERATED |

```
DROP TABLE IF EXISTS Notes;
CREATE TABLE Notes (
    Text TEXT NOT NULL,
    UpdateTime DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);
```

# 视图

## CodeCompose
> 代码和演示图

| Field      | Type         | Null | Key | Default | Extra |
| -          | -            | -    | -   | -       | -     |
| Repository | varchar(50)  | NO   |     | NULL    |       |
| Url        | varchar(200) | YES  |     | NULL    |       |
| Language   | varchar(50)  | NO   |     | NULL    |       |
| Image      | mediumblob   | YES  |     | NULL    |       |

```
CREATE OR REPLACE VIEW CodeCompose AS
SELECT Codes.*,Image FROM Codes
LEFT JOIN CodeDemos ON Codes.Repository = CodeDemos.Repository;
```

## AnimeCompose
> 动漫和封面

| Field  | Type         | Null | Key | Default | Extra |
| -      | -            | -    | -   | -       | -     |
| Title  | varchar(100) | NO   |     | NULL    |       |
| Origin | varchar(100) | YES  |     | NULL    |       |
| Year   | int          | NO   |     | NULL    |       |
| Month  | int          | NO   |     | NULL    |       |
| Level  | int          | YES  |     | NULL    |       |
| Depict | varchar(200) | YES  |     | NULL    |       |
| Image  | mediumblob   | YES  |     | NULL    |       |

```
CREATE OR REPLACE VIEW AnimeCompose AS
SELECT Animes.*,Depict,Image FROM Animes
LEFT JOIN AnimeGrade ON Animes.Level = AnimeGrade.Level
LEFT JOIN AnimePages ON Animes.Title = AnimePages.Title;
```
