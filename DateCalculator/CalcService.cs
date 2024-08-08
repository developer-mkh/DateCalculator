using DateCalculator.Db;
using DateCalculator.Entity;
using System;
using System.Collections.Generic;

namespace DateCalculator.Service
{
    /// <summary>
    /// 日付計算のサービスクラス
    /// </summary>
    public class CalcService
    {
        /// <summary>
        /// 休日カテゴリ：祝祭日
        /// </summary>
        private const int HOLIDAY = 1;
        /// <summary>
        /// 休日カテゴリ：その他休日
        /// </summary>
        private const int PAID_HOLIDAY = 2;

        // DBアクセスクラス
        private DBAccess dBAccess;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dBAccess">DBアクセスクラスのインスタンス</param>
        public CalcService(DBAccess dBAccess)
        {
            this.dBAccess = dBAccess;
        }

        /// <summary>
        /// 与えられた2つの日付間の日数を求める。
        /// 開始日と終了日の大小関係は考慮せず、終了日-開始日を返す。
        /// </summary>
        /// <param name="startDate">開始日</param>
        /// <param name="endDate">終了日</param>
        /// <returns>終了日-開始日</returns>
        public int CalcDays(DateTime startDate, DateTime endDate)
        {
            return endDate.Subtract(startDate).Days; ;
        }

        /// <summary>
        /// 与えられた2つの日付間の日数（ただし土日を除く）を求める。
        /// 開始日と終了日の大小関係は考慮せず、終了日-開始日-土日の日数を返す。
        /// </summary>
        /// <param name="startDate">開始日</param>
        /// <param name="endDate">終了日</param>
        /// <param name="days">土日を含む与えられた2つの日付間の日数</param>
        /// <returns>終了日-開始日-土日</returns>
        public int CalcBusinessDays(DateTime startDate, DateTime endDate, int days)
        {
            int businessDays = (days * 5 - (startDate.DayOfWeek - endDate.DayOfWeek) * 2) / 7;
            if (endDate.DayOfWeek == DayOfWeek.Saturday)
            {
                businessDays--;
            }
            if (startDate.DayOfWeek == DayOfWeek.Saturday)
            {
                businessDays++;
            }

            return businessDays;
        }

        /// <summary>
        /// 与えられた2つの日付間の日数（ただし土日を除く）を求める。
        /// 開始日と終了日の大小関係は考慮せず、終了日-開始日-土日の日数を返す。
        /// </summary>
        /// <param name="startDate">開始日</param>
        /// <param name="endDate">終了日</param>
        /// <returns>終了日-開始日-土日</returns>
        public int CalcBusinessDays(DateTime startDate, DateTime endDate)
        {
            return CalcBusinessDays(startDate, endDate, CalcDays(startDate, endDate));
        }

        /// <summary>
        /// 与えられた2つの日付間の日数（ただし土日祝を除く）を求める。
        /// 開始日と終了日の大小関係は考慮せず、終了日-開始日-土日祝の日数を返す。
        /// 祝日の定義はDBより取得する。
        /// </summary>
        /// <param name="startDate">開始日</param>
        /// <param name="endDate">終了日</param>
        /// <param name="businessDays">土日を除く与えられた2つの日付間の日数</param>
        /// <returns>終了日-開始日-土日祝</returns>
        public int CalcBusinessDaysWithHolidays(DateTime startDate, DateTime endDate, int businessDays)
        {
            List<Holiday> holidays = dBAccess.GetHolidays(HOLIDAY);

            for (int i = holidays.Count - 1; i >= 0; i--)
            {
                if (!IsWorkDay(holidays[i].date, startDate, endDate))
                {
                    holidays.RemoveAt(i);
                }
            }

            return businessDays - holidays.Count;
        }

        /// <summary>
        /// 与えられた2つの日付間の日数（ただし土日祝を除く）を求める。
        /// 開始日と終了日の大小関係は考慮せず、終了日-開始日-土日祝の日数を返す。
        /// 祝日の定義はDBより取得する。
        /// </summary>
        /// <param name="startDate">開始日</param>
        /// <param name="endDate">終了日</param>
        /// <returns>終了日-開始日-土日祝</returns>
        public int CalcBusinessDaysWithHolidays(DateTime startDate, DateTime endDate)
        {
            return CalcBusinessDaysWithHolidays(startDate, endDate, CalcBusinessDays(startDate, endDate));
        }

        /// <summary>
        /// 与えられた2つの日付間の日数（ただし土日祝その他休みを除く）を求める。
        /// 開始日と終了日の大小関係は考慮せず、終了日-開始日-土日祝その他休みの日数を返す。
        /// その他休みの定義はDBより取得する。
        /// </summary>
        /// <param name="startDate">開始日</param>
        /// <param name="endDate">終了日</param>
        /// <param name="businessDaysWithHolidays">土日祝を除く与えられた2つの日付間の日数</param>
        /// <returns>終了日-開始日-土日祝その他休み</returns>
        public int CalcBusinessDaysWithPaidHolidays(DateTime startDate, DateTime endDate, int businessDaysWithHolidays)
        {
            List<Holiday> paidHolidays = dBAccess.GetHolidays(PAID_HOLIDAY);

            for (int i = paidHolidays.Count - 1; i >= 0; i--)
            {
                if (!IsWorkDay(paidHolidays[i].date, startDate, endDate))
                {
                    paidHolidays.RemoveAt(i);
                }
            }

            return businessDaysWithHolidays - paidHolidays.Count;
        }
        /// <summary>
        /// 与えられた2つの日付間の日数（ただし土日祝その他休みを除く）を求める。
        /// 開始日と終了日の大小関係は考慮せず、終了日-開始日-土日祝その他休みの日数を返す。
        /// 祝日、その他休みの定義はDBより取得する。
        /// </summary>
        /// <param name="startDate">開始日</param>
        /// <param name="endDate">終了日</param>
        /// <returns>終了日-開始日-土日祝その他休み</returns>
        public int CalcBusinessDaysWithPaidHolidays(DateTime startDate, DateTime endDate)
        {
            int businessDaysWithHolidays = CalcBusinessDaysWithHolidays(startDate, endDate);
            return CalcBusinessDaysWithPaidHolidays(startDate, endDate, CalcBusinessDaysWithHolidays(startDate, endDate));
        }

        /// <summary>
        /// 与えられた日が、与えられた期間内の平日かどうかを判定する。
        /// 期間の両端は期間内に含む。
        /// </summary>
        /// <param name="target">判定したい日</param>
        /// <param name="startDate">期間の開始日</param>
        /// <param name="endDate">期間の終了日</param>
        /// <returns>判定したい日が期間内の平日であればTrue</returns>
        private bool IsWorkDay(DateTime target, DateTime startDate, DateTime endDate)
        {
            return (startDate <= target && target <= endDate && target.DayOfWeek != DayOfWeek.Saturday && target.DayOfWeek != DayOfWeek.Sunday);
        }
    }
}
