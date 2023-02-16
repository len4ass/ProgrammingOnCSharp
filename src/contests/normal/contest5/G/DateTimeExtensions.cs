using System;
internal static class DateTimeExtensions
{
    public static bool IsWeekend(this DateTime date)
    {
        return date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }

    public static bool IsWorkingDay(this DateTime date)
    {
        return date.DayOfWeek is DayOfWeek.Monday or DayOfWeek.Tuesday or DayOfWeek.Wednesday or DayOfWeek.Thursday or DayOfWeek.Friday;
    }

    public static string NextDayOfWeek(this DateTime date)
    {
        var datenew = date.AddDays(1);
        return datenew.DayOfWeek.ToString();
    }

    public static string DayOfWeekSomeDaysFromCurrent(this DateTime date, int amount)
    {
        var datenew = date.AddDays(amount);
        return datenew.DayOfWeek.ToString();
    }

    public static string DaysBetween(this DateTime date, DateTime dateother)
    {
        if (date < dateother)
        {
            return $"{(dateother - date).Days} days in future";
        }
        
        return $"{(date - dateother).Days} days ago";
    }
}