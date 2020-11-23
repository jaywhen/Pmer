using Pmer.Encryption;
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
                CreateDbFile();
                CreateMasterUserTable();
                return false;
            }
            else if (!CheckIfTableExist(userTableName))
            {
                // 若不存在用户表，则创建用户表与密码表
                CreateMasterUserTable();
                CreatePassWordsTable();
                return false;

            }
            else if (!CheckIfTableContainsData(userTableName))
            {
                // 存在用户表，但表中无数据
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CreateDbFile()
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

        public string CreateDbConnection()
        {
            string dbFilePath = dbPath + dbName;
            if (!Directory.Exists(dbPath))
            {
                CreateDbFile();
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

        public void CreateMasterUserTable()
        {
            if (!CheckIfTableExist(userTableName))
            {
                sqlcmd = "CREATE TABLE " + userTableName + 
                    "(id INTEGER PRIMARY KEY AUTOINCREMENT, username varchar, master_pw varchar, preSalt varchar, sufSalt varchar);";
                ExecuteQuery(sqlcmd);
            }

        }

        public void CreatePassWordsTable()
        {
            // 创建密码表
            if (!CheckIfTableExist(pwTableName))
            {
                sqlcmd = "CREATE TABLE " + pwTableName +
                    "(id INTEGER PRIMARY KEY AUTOINCREMENT, title varchar, account varchar, password varchar, website varchar, avatar varchar);";
                ExecuteQuery(sqlcmd);
            }

        }


        public bool CheckIfTableExist(string tableName)
        {
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
            var result = cmd.ExecuteScalar();
            return result != null && result.ToString() == tableName ? true : false;
        }

        public void ExecuteQuery(string sqlCommand)
        {
            SQLiteCommand triggerCommand = dbConnection.CreateCommand();
            triggerCommand.CommandText = sqlCommand;
            triggerCommand.ExecuteNonQuery();
        }

        public bool CheckIfTableContainsData(string tableName)
        {
            cmd.CommandText = "SELECT count(*) FROM " + tableName;
            var result = cmd.ExecuteScalar();

            return Convert.ToInt32(result) > 0 ? true : false;
        }

        // insert
        public void InsertMasterPw(string username, string password, string preSalt, string sufSalt)
        {
            // 待改进，此句太长

            sqlcmd =
                "insert into " + userTableName + "(username, master_pw, preSalt, sufSalt) values ('" + username + "', '" + password + "', '" + preSalt + "', '" + sufSalt + "')";
            ExecuteQuery(sqlcmd);
        }

        public void InsertNewPw(string title, string account, string password, string website, string avatar)
        {

            sqlcmd =
                "insert into " + pwTableName + "(title, account, password, website, avatar) values ('" + title + "', '" + account + "', '" + password + "', '" + website + "', '"+ avatar + "')";
            ExecuteQuery(sqlcmd);
        }


        // search
        public string GetMasterPwFromMpTable()
        {
            cmd.CommandText = "SELECT master_pw FROM " + userTableName;
            var result = cmd.ExecuteScalar();
            return result.ToString();
        }

        public string GetPreSaltFromMpTable()
        {
            cmd.CommandText = "SELECT preSalt FROM " + userTableName;
            var result = cmd.ExecuteScalar();
            return result.ToString();
        }

        public string GetSufSaltFromMpTable()
        {
            cmd.CommandText = "SELECT sufSalt FROM " + userTableName;
            var result = cmd.ExecuteScalar();
            return result.ToString();
        }

        public string GetUserNameFromMpTable()
        {
            cmd.CommandText = "SELECT username FROM " + userTableName;
            var result = cmd.ExecuteScalar();
            return result.ToString();
        }

        public List<PasswordItem> GetPasswordItems(byte[] keyPassword)
        {
            List<PasswordItem> passwordItems = new List<PasswordItem>();
            cmd.CommandText = "SELECT * FROM " + pwTableName;
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PasswordItem passwordItem = new PasswordItem();
                passwordItem.Id = (Int64)reader["id"];
                passwordItem.Title = (string)reader["title"];
                passwordItem.Account = (string)reader["account"];

                string cipherText = (string)reader["password"];
                passwordItem.Password = Encryptor.AESDecrypt(cipherText, keyPassword);

                passwordItem.Website = (string)reader["website"];
                passwordItem.Avatar = (string)reader["avatar"];
                passwordItems.Add(passwordItem);
            }
            return passwordItems;
        }

        //delete
        public void DeletePasswordItem(Int64 itemId)
        {

            cmd.CommandText = "delete from passwords where id = " + itemId.ToString();
            cmd.ExecuteNonQuery();
        }

        //update
        public void UpdatePasswordItem(string account ,string cipherText, string website, Int64 id)
        {
            cmd.CommandText = "UPDATE passwords set Account = '" + account + "', password = '" + cipherText + "', website = '" + website + "' where id = " + id.ToString();
            cmd.ExecuteNonQuery();
        }
    }
}
