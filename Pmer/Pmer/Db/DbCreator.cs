﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                createMasterUserTable();
                return false;

            }
            else if (!checkIfTableContainsData(userTableName))
            {
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
                sqlcmd = "CREATE TABLE " + userTableName + "(id INTEGER PRIMARY KEY AUTOINCREMENT, username varchar, master_pw varchar);";
                executeQuery(sqlcmd);
            }

        }

        public void createPassWordsTable()
        {
            // 创建密码表
            if (!checkIfTableExist(pwTableName))
            {
                sqlcmd = "CREATE TABLE " + pwTableName + "(id INTEGER PRIMARY KEY AUTOINCREMENT, username varchar, master_pw varchar);";
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
            
            sqlcmd = "insert into " + userTableName + "(id, username, master_pw) values (0, " + "'" +username+ "'" + ", "+ mpw + ")";
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


    }
}
