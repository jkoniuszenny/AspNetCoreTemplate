using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class MethodExtensions
    {
        /// <summary>
        /// Zwraca ostatni dzień miesiąca z podanej daty
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        /// <summary>
        /// Zwraca pierwszy dzień miesiąca wskazanej daty.
        /// </summary>
        /// <param name="dateTime">Data.</param>
        public static DateTime GetFirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        /// <summary>
        /// Testuje czy wartość mieści się we wskazanym zakresie z uwzględnieniem lub wykluczeniem wartości granicznych.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="includeBorders"></param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T item, T start, T end, bool includeBorders = true)
        {
            if (includeBorders)
            {
                return Comparer<T>.Default.Compare(item, start) >= 0 ///Czy element większy lub równy od startu
                && Comparer<T>.Default.Compare(item, end) <= 0; /// Czy element mniejszy lub równy od końca
            }
            else
            {
                return Comparer<T>.Default.Compare(item, start) > 0 ///Czy element większy  od startu
                && Comparer<T>.Default.Compare(item, end) < 0; /// Czy element mniejszy  od końca
            }

        }

        /// <summary>
        /// Testuje czy data mieści się we wskazanym zakresie dat.
        /// </summary>
        /// <param name="date">Testowana data.</param>
        /// <param name="begin">Data początku zakresu. (date >= begin)</param>
        /// <param name="end">Data końca zakresu. (date <= end)</param>
        /// <returns>true, jeśli data mieści się we wskazanym zakresie dat, w przeciwnym razie false.</returns>
        public static bool Between(this DateTime date, DateTime begin, DateTime end)
        {
            if (begin > end)
            {
                Swap(ref begin, ref end);
            }
            return (date >= begin && date <= end);
        }

        /// <summary>
        /// Zamienia miejscami wartości między dwoma zmiennymi.
        /// </summary>
        /// <param name="value1">Wartość 1.</param>
        /// <param name="value2">Wartość 2.</param>
        public static void Swap<T>(ref T value1, ref T value2)
        {
            T tmp = value1;
            value1 = value2;
            value2 = tmp;
        }


        /// <summary>
        /// Zaokrąglanie czasu do najbliższej pół godziny
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime RoundTime(this DateTime time)
        {
            DateTime Result;
            int hour = time.Hour;
            int min = time.Minute;
            Result = time.Date;

            int sek;

            if (min < 15)
            {
                min = 0;
                sek = 0;
            }
            else
            {
                if (min >= 15 && min < 45)
                {
                    min = 30;
                    sek = 0;
                }
                else
                {
                    if (hour < 23)
                    {
                        hour = hour + 1;
                    }
                    else
                    {
                        hour = 0;
                        Result = Result.AddDays(1);
                    }
                    min = 0;
                    sek = 0;
                }
            }

            Result = new DateTime(Result.Year, Result.Month, Result.Day, hour, min, sek);

            return Result;
        }

        public static decimal DatesInDates(DateTime fromA, DateTime toA, DateTime fromB, DateTime toB)
        {
            try
            {
                //  fA-fB-tA-tB  ->  fB-tA
                if (fromA < fromB && toA.IsBetween(fromB, toB))
                {
                    TimeSpan duration = toA - fromB;
                    return Convert.ToDecimal(duration.TotalHours);
                }

                //  fA-fB-tB-tA  -> fB-tB
                if (fromB.IsBetween(fromA, toA) && toB.IsBetween(fromA, toA))
                {
                    TimeSpan duration = toB - fromB;
                    return Convert.ToDecimal(duration.TotalHours);
                }

                //  fB-fA-tA-tB  -> fA-tA
                if (fromA.IsBetween(fromB, toB) && toA.IsBetween(fromB, toB))
                {
                    TimeSpan duration = toA - fromA;
                    return Convert.ToDecimal(duration.TotalHours);
                }

                // fB-fA-tB-tA  -> fA-tB
                if (fromA.IsBetween(fromB, toB) && toA > toB)
                {
                    TimeSpan duration = toB - fromA;
                    return Convert.ToDecimal(duration.TotalHours);
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Wspólna część dwóch zakresów dat", ex);
            }

        }

        public static string GetDescriptions<T>(this T source)
        {
            var attributes = typeof(T).GetMembers()
                .Where(w => w.Name == source.ToString())
                .SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>())
                .FirstOrDefault();

            return attributes.Description;
        }

        public static decimal RoundDownToPointFive(decimal ret)
        {
            if (ret - Math.Truncate(ret) > 0.5m)
            {
                return Math.Truncate(ret) + 0.5m;
            }
            else if (ret - Math.Truncate(ret) < 0.5m && ret - Math.Truncate(ret) != 0)
            {
                return Math.Truncate(ret);
            }
            else
            {
                return ret;
            }
        }

    }
}
