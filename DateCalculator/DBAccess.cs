using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using DateCalculator.dto;

namespace DateCalculator.db
{
    public class DBAccess
    {
        public string ConnectString { get; set; } = @"Data Source = holidays.db";

        public void CreateTable()
        {
            string[] sqls = new string[2];
            sqls[0] = "CREATE TABLE IF NOT EXISTS holidays (id INTEGER PRIMARY KEY, date TEXT, name TEXT, category INTEGER)";
            sqls[1] = "CREATE TABLE IF NOT EXISTS categories (id INTEGER PRIMARY KEY, name TEXT, value INTEGER)";
            ExecuteNoneQuery(sqls);
            initializeData(ConnectString);
        }

        private void initializeData(string connectionString)
        {
            if(int.Parse(ExecuteScalar("SELECT count(*) FROM categories").ToString()) == 0)
            {
                string[] sqls = new string[2];
                sqls[0] = "INSERT INTO categories (name, value) VALUES(\"祝祭日\", 1)";
                sqls[1] = "INSERT INTO categories (name, value) VALUES(\"休日\", 2)";
                ExecuteNoneQuery(sqls);
            }
        }

        /// <summary>
        /// DataTableを使ったデータの取得
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        public DataTable GetData()
        {
            DataTable dt = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectString))
            {
                connection.Open();

                using (SQLiteCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT holidays.id, holidays.name, holidays.date, holidays.category, categories.name FROM holidays INNER JOIN categories ON holidays.category =  categories.value";

                    // DataAdapterの生成
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);

                    // データベースからデータを取得
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public List<Holiday> getHolidaysCount(int category)
        {
            var sql = string.Format("SELECT date, category FROM holidays WHERE category = {0}", category);
            List<Object[]> holidays = ExecuteReader(sql);
            List<Holiday> ret = new List<Holiday>();
            foreach (var item in holidays)
            {
                Holiday dto = new Holiday();
                try
                {
                    dto.date = DateTime.Parse(item[0].ToString());
                }
                catch (Exception)
                {
                }
                dto.category = item[1].ToString();
                ret.Add(dto);
            }

            return ret;
        }


        /// <summary>
        /// DataTableの内容をデータベースに保存
        /// </summary>
        /// <param name="connectString"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable SetData(DataTable dt)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectString))
            {
                connection.Open();
                SQLiteTransaction trans = connection.BeginTransaction();

                try
                {
                    using (SQLiteCommand cmd = connection.CreateCommand())
                    {
                        //書き込み先テーブルの列名と型を取得するためのSQLをCommandに登録
                        cmd.CommandText = "SELECT * FROM holidays";

                        // DataAdapterの生成
                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);

                        //Insert、Delete、Update　コマンドの自動生成
                        SQLiteCommandBuilder bulider = new SQLiteCommandBuilder(da);

                        //DataTableの内容をデータベースに書き込む
                        da.Update(dt);

                        //コミット
                        trans.Commit();
                    }
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
            return dt;
        }

        /// <summary>
        /// SQLの実行
        /// </summary>
        /// <param name="connectString"></param>
        /// <param name="sqls"></param>
        private void ExecuteNoneQuery(string[] sqls)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectString))
            {
                connection.Open();
                SQLiteTransaction trans = connection.BeginTransaction();

                try
                {
                    foreach (var sql in sqls)
                    {
                        using (SQLiteCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }

            }
        }
        /// <summary>
        /// スカラーによる単一データの取得
        /// </summary>
        /// <param name="connectString"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private object ExecuteScalar(string sql)
        {
            object result = null;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectString))
            {
                connection.Open();

                using (SQLiteCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = sql;
                    result = cmd.ExecuteScalar();
                }
            }

            return result;
        }
        /// <summary>
        /// DataReaderを使ったデータの取得
        /// </summary>
        /// <param name="connectString"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<object[]> ExecuteReader(string sql)
        {
            List<object[]> result = new List<object[]>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectString))
            {
                connection.Open();

                using (SQLiteCommand cmd = connection.CreateCommand())
                {
                    //SQLの設定
                    cmd.CommandText = sql;

                    //検索
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object[] data = Enumerable.Range(0, reader.FieldCount).Select(i => reader[i]).ToArray();
                            result.Add(data);
                        }
                    }
                }
            }
            return result;
        }

    }
}
