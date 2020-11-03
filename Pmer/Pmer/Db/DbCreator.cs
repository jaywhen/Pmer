using Pmer.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;


namespace Pmer.Db
{
    class DbCreator
    {
        SQLiteConnection dbConnection;
        SQLiteCommand cmd;
        private string sqlcmd;
        private string dbPath = System.Environment.CurrentDirectory + "\\Db";
        private string dbName = "\\Pmer.db";
        private string userTableName = "MasterPassword";
        private string pwTableName = "Passwords";

        public bool Init()
        {
            // 检查是否有.db && db是否有表 && 表中是否有数据
            if (!Directory.Exists(dbPath))
            {
                createDbFile();
                createMasterUserTable();
                return false;
            }
            else if (!checkIfTableExist(userTableName))
            {
                // 若不存在用户表，则创建用户表与密码表
                createMasterUserTable();
                CreatePassWordsTable();
                return false;

            }
            else if (!checkIfTableContainsData(userTableName))
            {
                // 存在用户表，但表中无数据
                return false;
            }
            else
            {
                return true;
            }
        }

        public void createDbFile()
        {
            if (!string.IsNullOrEmpty(dbPath) && !Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);
            }
            string dbFilePath = dbPath + dbName;
            if (!System.IO.File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }
        }

        public string createDbConnection()
        {
            string dbFilePath = dbPath + dbName;
            if (!Directory.Exists(dbPath))
            {
                createDbFile();
            }
            else if (!System.IO.File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }
            string strCon = string.Format("Data Source={0};", dbFilePath);
            dbConnection = new SQLiteConnection(strCon);
            dbConnection.Open();
            cmd = dbConnection.CreateCommand();
            return strCon;
        }

        public void createMasterUserTable()
        {
            if (!checkIfTableExist(userTableName))
            {
                sqlcmd = "CREATE TABLE " + userTableName + 
                    "(id INTEGER PRIMARY KEY AUTOINCREMENT, username varchar, master_pw varchar);";
                executeQuery(sqlcmd);
            }

        }

        public void CreatePassWordsTable()
        {
            // 创建密码表
            if (!checkIfTableExist(pwTableName))
            {
                sqlcmd = "CREATE TABLE " + pwTableName +
                    "(id INTEGER PRIMARY KEY AUTOINCREMENT, title varchar, account varchar, password varchar, website varchar, avatar varchar);";
                executeQuery(sqlcmd);
            }

        }


        public bool checkIfTableExist(string tableName)
        {
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
            var result = cmd.ExecuteScalar();
            return result != null && result.ToString() == tableName ? true : false;
        }

        public void executeQuery(string sqlCommand)
        {
            SQLiteCommand triggerCommand = dbConnection.CreateCommand();
            triggerCommand.CommandText = sqlCommand;
            triggerCommand.ExecuteNonQuery();
        }

        public bool checkIfTableContainsData(string tableName)
        {
            cmd.CommandText = "SELECT count(*) FROM " + tableName;
            var result = cmd.ExecuteScalar();

            return Convert.ToInt32(result) > 0 ? true : false;
        }

        public void fillTable()
        {
            if (!checkIfTableContainsData("MY_TABLE"))
            {
                sqlcmd = "insert into MY_TABLE (code_test_type) values (999)";
                executeQuery(sqlcmd);
            }
        }

        // insert
        public void insertMasterPw(string mpw, string username)
        {
            
            sqlcmd = 
                "insert into " + userTableName + "(username, master_pw) values ('" + username + "', '" + mpw + "')";
            executeQuery(sqlcmd);
        }

        public void InsertNewPw(string title, string account, string password, string website, string avatar)
        {

            sqlcmd =
                "insert into " + pwTableName + "(title, account, password, website, avatar) values ('" + title + "', '" + account + "', '" + password + "', '" + website + "', '"+ avatar + "')";
            executeQuery(sqlcmd);
        }


        // search
        public string getMasterPwFromMpTable()
        {
            cmd.CommandText = "SELECT master_pw FROM " + userTableName;
            var result = cmd.ExecuteScalar();
            return result.ToString();
        }

        public string getUserNameFromMpTable()
        {
            cmd.CommandText = "SELECT username FROM " + userTableName;
            var result = cmd.ExecuteScalar();
            return result.ToString();
        }

        public List<PasswordItem> GetPasswordItems()
        {
            List<PasswordItem> passwordItems = new List<PasswordItem>();
            cmd.CommandText = "SELECT * FROM " + pwTableName;
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PasswordItem passwordItem = new PasswordItem();
                passwordItem.Title = (string)reader["title"];
                passwordItem.Account = (string)reader["account"];
                passwordItem.Password = (string)reader["password"];
                passwordItem.Website = (string)reader["website"];
                passwordItem.Avatar = (string)reader["avatar"];
                passwordItems.Add(passwordItem);
            }
            return passwordItems;
        }
    }
}
