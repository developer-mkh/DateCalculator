using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
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
        /// DataTableを使った休日データの取得
        /// </summary>
        /// <returns>登録されているすべての休日データ</returns>
        public DataTable GetHolidays()
        {
            DataTable dt = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectString))
            {
                connection.Open();

                using (SQLiteCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT holidays.id, holidays.name, holidays.date, holidays.category FROM holidays";

                    // DataAdapterの生成
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);

                    // データベースからデータを取得
                    da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// DataTableを使ったカテゴリーデータの取得
        /// </summary>
        /// <returns>登録されているすべてのカテゴリーデータ</returns>
        public DataTable GetCategories()
        {
            DataTable dt = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectString))
            {
                connection.Open();

                using (SQLiteCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT categories.id, categories.name, categories.value FROM categories";

                    // DataAdapterの生成
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);

                    // データベースからデータを取得
                    da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// カテゴリーを指定した休日一覧の取得
        /// </summary>
        /// <param name="category">カテゴリー</param>
        /// <returns>登録されており、指定したカテゴリーに一致するすべての休日データ</returns>
        public List<Holiday> GetHolidays(int category)
        {
            var sql = string.Format("SELECT date, category FROM holidays WHERE category = {0}", category);
            List<Object[]> holidays = ExecuteReader(sql);
            List<Holiday> ret = new List<Holiday>();
            CultureInfo provider = CultureInfo.InvariantCulture;

            foreach (var item in holidays)
            {
                Holiday dto = new Holiday();
                try
                {
                    dto.date = DateTime.ParseExact(item[0].ToString(), "yyyyMMdd", provider);
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
        /// 休日の追加
        /// </summary>
        /// <param name="targets">追加する休日情報</param>
        public void AddHolidays(List<Holiday> targets)
        {
            List<String> sqls = new List<String>();
            foreach (var item in targets)
            {
                sqls.Add(string.Format("INSERT INTO holidays (name, date, category) VALUES(\'{0}\', {1}, {2})", item.name, item.date.ToString("yyyyMMdd"), item.category));
            }
            ExecuteNoneQuery(sqls.ToArray());
        }

        /// <summary>
        /// 休日の削除
        /// </summary>
        /// <param name="targets">削除する休日情報（idのみ設定されていれば良い）</param>
        public void DeleteHolidays(List<Holiday> targets)
        {
            List<String> sqls = new List<String>();
            foreach (var item in targets)
            {
                sqls.Add(string.Format("DELETE FROM holidays WHERE id = {0}", item.id));
            }
            ExecuteNoneQuery(sqls.ToArray());
        }


        /// <summary>
        /// DataTableの内容をデータベースに保存
        /// </summary>
        /// <param name="dt">データベースに保存したいDataTable</param>
        /// <returns>DataTable</returns>
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
        /// SQLの実行（任意のSQL）
        /// </summary>
        /// <param name="sqls">実行したいSQL</param>
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
        /// <param name="sql">実行したいSQL</param>
        /// <returns>SQLの実行結果（単一オブジェクト）</returns>
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
        /// <param name="sql">実行したいSQL</param>
        /// <returns>SQLの実行結果（複数オブジェクト）</returns>
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
