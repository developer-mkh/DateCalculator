using DateCalculator.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SQLite;

namespace DateCalculator.Service
{
    /// <summary>
    /// CalcServiceのテストクラス
    /// </summary>
    [TestClass]
    public class CalcServiceTest
    {
        // DBアクセスクラス
        internal static DBAccess dBAccess = new DBAccess();
        // DB接続文字列
        internal const string CONNECTION_STRING = @"Data Source = dateCalculator.db";

        [ClassInitialize]
        public static void ClassInit(TestContext ctx)
        {
            dBAccess.ConnectString = CONNECTION_STRING;
            dBAccess.CreateTable();
        }

        [TestInitialize]
        public void Init()
        {
            string[] sqls = new string[3];
            sqls[0] = "DELETE FROM holidays";
            sqls[1] = @"INSERT INTO holidays (name, date, category) VALUES('祝日', '20240813', 1)";
            sqls[2] = @"INSERT INTO holidays (name, date, category) VALUES('休日', '20240815', 2)";
            ExecuteNoneQuery(sqls);
        }

        /// <summary>
        /// CalcDaysのテスト
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="expected"></param>
        [TestMethod]
        [DataRow("2024/08/05", "2024/08/07", 2, DisplayName = "平日～平日")]
        [DataRow("2024/08/03", "2024/08/04", 1, DisplayName = "土～日")]
        [DataRow("2024/08/02", "2024/08/06", 4, DisplayName = "平日～土日～平日")]
        [DataRow("2024/08/01", "2024/08/03", 2, DisplayName = "平日～土")]
        [DataRow("2024/08/01", "2024/08/04", 3, DisplayName = "平日～日")]
        [DataRow("2024/08/03", "2024/08/06", 3, DisplayName = "土～平日")]
        [DataRow("2024/08/04", "2024/08/06", 2, DisplayName = "日～平日")]
        [DataRow("2024/08/05", "2024/08/05", 0, DisplayName = "平日同一日")]
        [DataRow("2024/08/12", "2024/08/14", 2, DisplayName = "祝日を挟む")]
        [DataRow("2024/08/14", "2024/08/16", 2, DisplayName = "休日を挟む")]
        public void TestCalcDays(string startDate, string endDate, int expected)
        {
            CalcService target = new CalcService(dBAccess);
            int actual = target.CalcDays(DateTime.Parse(startDate), DateTime.Parse(endDate));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// CalcBusinessDaysのテスト
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="expected"></param>
        [TestMethod]
        [DataRow("2024/08/05", "2024/08/07", 2, DisplayName = "平日～平日")]
        [DataRow("2024/08/03", "2024/08/04", 0, DisplayName = "土～日")]
        [DataRow("2024/08/02", "2024/08/06", 2, DisplayName = "平日～土日～平日")]
        [DataRow("2024/08/01", "2024/08/03", 1, DisplayName = "平日～土")]
        [DataRow("2024/08/01", "2024/08/04", 1, DisplayName = "平日～日")]
        [DataRow("2024/08/03", "2024/08/06", 2, DisplayName = "土～平日")]
        [DataRow("2024/08/04", "2024/08/06", 2, DisplayName = "日～平日")]
        [DataRow("2024/08/05", "2024/08/05", 0, DisplayName = "平日同一日")]
        [DataRow("2024/08/12", "2024/08/14", 2, DisplayName = "祝日を挟む")]
        [DataRow("2024/08/14", "2024/08/16", 2, DisplayName = "休日を挟む")]
        [DataRow("2024/08/05", "2024/08/14", 7, DisplayName = "平日～平日（1週間後）")]
        [DataRow("2024/08/03", "2024/08/11", 5, DisplayName = "土～日（1週間後）")]
        [DataRow("2024/08/02", "2024/08/13", 7, DisplayName = "平日～土日～平日（1週間後）")]
        [DataRow("2024/08/01", "2024/08/10", 6, DisplayName = "平日～土（1週間後）")]
        [DataRow("2024/08/01", "2024/08/11", 6, DisplayName = "平日～日（1週間後）")]
        [DataRow("2024/08/03", "2024/08/13", 7, DisplayName = "土～平日（1週間後）")]
        [DataRow("2024/08/04", "2024/08/13", 7, DisplayName = "日～平日（1週間後）")]
        [DataRow("2024/08/12", "2024/08/21", 7, DisplayName = "祝日を挟む（1週間後）")]
        [DataRow("2024/08/14", "2024/08/23", 7, DisplayName = "休日を挟む（1週間後）")]
        public void TestCalcBusinessDays(string startDate, string endDate, int expected)
        {
            CalcService target = new CalcService(dBAccess);
            int actual = target.CalcBusinessDays(DateTime.Parse(startDate), DateTime.Parse(endDate));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// CalcBusinessDaysWithHolidaysのテスト
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="expected"></param>
        [TestMethod]
        [DataRow("2024/08/05", "2024/08/07", 2, DisplayName = "平日～平日")]
        [DataRow("2024/08/03", "2024/08/04", 0, DisplayName = "土～日")]
        [DataRow("2024/08/02", "2024/08/06", 2, DisplayName = "平日～土日～平日")]
        [DataRow("2024/08/01", "2024/08/03", 1, DisplayName = "平日～土")]
        [DataRow("2024/08/01", "2024/08/04", 1, DisplayName = "平日～日")]
        [DataRow("2024/08/03", "2024/08/06", 2, DisplayName = "土～平日")]
        [DataRow("2024/08/04", "2024/08/06", 2, DisplayName = "日～平日")]
        [DataRow("2024/08/05", "2024/08/05", 0, DisplayName = "平日同一日")]
        [DataRow("2024/08/12", "2024/08/14", 1, DisplayName = "祝日を挟む")]
        [DataRow("2024/08/12", "2024/08/21", 6, DisplayName = "祝日、休日を挟む")]
        [DataRow("2024/08/14", "2024/08/16", 2, DisplayName = "休日を挟む")]
        [DataRow("2024/08/19", "2024/08/28", 7, DisplayName = "平日～平日（1週間後）")]
        [DataRow("2024/08/03", "2024/08/11", 5, DisplayName = "土～日（1週間後）")]
        [DataRow("2024/08/16", "2024/08/27", 7, DisplayName = "平日～土日～平日（1週間後）")]
        [DataRow("2024/08/01", "2024/08/10", 6, DisplayName = "平日～土（1週間後）")]
        [DataRow("2024/08/01", "2024/08/11", 6, DisplayName = "平日～日（1週間後）")]
        [DataRow("2024/08/17", "2024/08/27", 7, DisplayName = "土～平日（1週間後）")]
        [DataRow("2024/08/18", "2024/08/27", 7, DisplayName = "日～平日（1週間後）")]
        [DataRow("2024/08/14", "2024/08/23", 7, DisplayName = "休日を挟む（1週間後）")]
        public void TestCalcBusinessDaysWithHolidays(string startDate, string endDate, int expected)
        {
            CalcService target = new CalcService(dBAccess);
            int actual = target.CalcBusinessDaysWithHolidays(DateTime.Parse(startDate), DateTime.Parse(endDate));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// CalcBusinessDaysWithPaidHolidaysのテスト
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="expected"></param>
        [TestMethod]
        [DataRow("2024/08/05", "2024/08/07", 2, DisplayName = "平日～平日")]
        [DataRow("2024/08/03", "2024/08/04", 0, DisplayName = "土～日")]
        [DataRow("2024/08/02", "2024/08/06", 2, DisplayName = "平日～土日～平日")]
        [DataRow("2024/08/01", "2024/08/03", 1, DisplayName = "平日～土")]
        [DataRow("2024/08/01", "2024/08/04", 1, DisplayName = "平日～日")]
        [DataRow("2024/08/03", "2024/08/06", 2, DisplayName = "土～平日")]
        [DataRow("2024/08/04", "2024/08/06", 2, DisplayName = "日～平日")]
        [DataRow("2024/08/05", "2024/08/05", 0, DisplayName = "平日同一日")]
        [DataRow("2024/08/12", "2024/08/14", 1, DisplayName = "祝日を挟む")]
        [DataRow("2024/08/14", "2024/08/16", 1, DisplayName = "休日を挟む")]
        [DataRow("2024/08/12", "2024/08/21", 5, DisplayName = "祝日、休日を挟む")]
        [DataRow("2024/08/19", "2024/08/28", 7, DisplayName = "平日～平日（1週間後）")]
        [DataRow("2024/08/03", "2024/08/11", 5, DisplayName = "土～日（1週間後）")]
        [DataRow("2024/08/16", "2024/08/27", 7, DisplayName = "平日～土日～平日（1週間後）")]
        [DataRow("2024/08/01", "2024/08/10", 6, DisplayName = "平日～土（1週間後）")]
        [DataRow("2024/08/01", "2024/08/11", 6, DisplayName = "平日～日（1週間後）")]
        [DataRow("2024/08/17", "2024/08/27", 7, DisplayName = "土～平日（1週間後）")]
        [DataRow("2024/08/18", "2024/08/27", 7, DisplayName = "日～平日（1週間後）")]
        [DataRow("2024/08/14", "2024/08/23", 6, DisplayName = "休日を挟む（1週間後）")]
        public void TestCalcBusinessDaysWithPaidHolidays(string startDate, string endDate, int expected)
        {
            CalcService target = new CalcService(dBAccess);
            int actual = target.CalcBusinessDaysWithPaidHolidays(DateTime.Parse(startDate), DateTime.Parse(endDate));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// SQLの実行（任意のSQL）
        /// </summary>
        /// <param name="sqls">実行したいSQL</param>
        private void ExecuteNoneQuery(string[] sqls)
        {
            using (SQLiteConnection connection = new SQLiteConnection(CONNECTION_STRING))
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
    }
}
