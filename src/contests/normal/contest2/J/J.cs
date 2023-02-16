public partial class Program
{
    // Проверка входной даты на корректность.
    private static bool ValidateData(int day, int month, int year)
    {
        bool leapYear = year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
        int[] daysInMonth = {-1, 31, leapYear ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

        if (month > 12 || month < 1 || year < 1701 || year > 1800)
        {
            return false;
        }
        
        if (day > daysInMonth[month] || day < 1)
        {
            return false;
        }

        return true;
    }

    // Получение дня недели по дате.
    private static int GetDayOfWeek(int day, int month, int year)
    {
        if (month == 1 || month == 2)
        {
            year -= 1;
            month += 10;
        }
        else
        {
            month -= 2;
        }

        return (day + (31 * month) / 12 + year + year / 4 - year / 100 + year / 400) % 7;
    }

    // Получение даты пятницы.
    private static string GetDateOfFriday(int dateOfWeek, int day, int month, int year)
    {
        int fridayState = 5;
        int tempDay = day;
        int tempMonth = month;
        int tempYear = year;
        bool leapYear = year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
        int[] daysInMonth = {-1, 31, leapYear ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

        while (dateOfWeek != fridayState)
        {
            if (tempDay >= daysInMonth[tempMonth])
            {
                tempDay = 1;
                if (tempMonth == 12)
                {
                    tempMonth = 1;
                    tempYear += 1;
                }
                else
                {
                    tempMonth += 1;
                }
            }
            else
            {
                tempDay += 1;
            }

            if (dateOfWeek == 6)
            {
                dateOfWeek = 0;
            }
            else
            {
                dateOfWeek += 1;
            }
        }

        string formatDate = "";
        formatDate += (tempDay < 10 ? "0" + tempDay : tempDay) + ".";
        formatDate += (tempMonth < 10 ? "0" + tempMonth : tempMonth) + ".";
        formatDate += tempYear;

        return formatDate;
    }
}