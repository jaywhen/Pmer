using System;
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
        string sqlcmd;
        string dbPath = System.Environment.CurrentDirectory + "\\Db";
        string dbName = "\\test.db";
        string userTableName = "MasterPassword";
        // string pwTableName = "Passwords";

        public bool Init()
        {
            // 检查是否有.db && db是否有表 && 表中是否有数据
            if (!Directory.Exists(dbPath))
            {
                createDbFile();
                createTables();
                return false;
            }
            else if (!checkIfTableExist(userTableName))
            {
                createTables();
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

        public void createTables()
        {
            if (!checkIfTableExist(userTableName))
            {
                sqlcmd = "CREATE TABLE " + userTableName + "(id INTEGER PRIMARY KEY AUTOINCREMENT,master_pw varchar);";
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
        public void insertMasterPw(string mpw)
        {
            sqlcmd = "insert into " + userTableName + "(id ,master_pw) values (0, " + mpw + ")";
            executeQuery(sqlcmd);
        }

    }
}
