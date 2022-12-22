using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

internal class Program
{
    static void Main(string[] args)
    {
        Stopwatch watch = Stopwatch.StartNew();
        string interval = Console.ReadLine().Trim();
        string[] dates = Console.ReadLine().Split(' ');
        //string interval = "WEEK".Trim();
        //string[] dates = "2016-09-20 2500-11-30".Split(' ');

        DateTime date1 = DateTime.Parse(dates[0]);
        DateTime date2 = DateTime.Parse(dates[1]);

        int counter = 0;
        string result = "";
        DateTime endDate1 = date1;
        TimeSpan tickBegin = new TimeSpan(date1.Ticks);
        TimeSpan tickEnd = new TimeSpan(date2.Ticks);
        TimeSpan tickDay = new TimeSpan(new DateTime(1,1,2,0,0,0).Ticks);

        if (interval == "WEEK")
        {
            while (tickBegin <= tickEnd)
            {
                result += new DateTime(tickBegin.Ticks).ToString("yyyy-MM-dd ");
                if (counter == 0)
                    tickBegin = new TimeSpan(GetLastDayOfWeek(date1).Ticks);
                else
                    tickBegin = tickBegin + (tickDay * 6);

                result += new DateTime(tickBegin.Ticks).ToString("yyyy-MM-dd\n");

                tickBegin = tickBegin + tickDay;
                counter++;
            }
        }
        else
            while (date1 <= date2)
            {
                switch (interval)
                {
                    case "MONTH":
                        result += new DateTime(date1.Ticks).ToString("yyyy-MM-dd ");
                        endDate1 = new DateTime(date1.Year, date1.Month, 1).AddMonths(1).AddDays(-1);
                        date1 = new DateTime(date1.Year, date1.Month, 1).AddMonths(1);
                        if (endDate1 > date2)
                            endDate1 = date2;
                        result += new DateTime(endDate1.Ticks).ToString("yyyy-MM-dd\n");
                        break;
                    case "REVIEW":
                        result += new DateTime(date1.Ticks).ToString("yyyy-MM-dd ");
                        endDate1 = GetLastDayReview(date1);
                        date1 = GetFirstDayReview(endDate1.AddDays(1));
                        if (endDate1 > date2)
                            endDate1 = date2;
                        result += new DateTime(endDate1.Ticks).ToString("yyyy-MM-dd\n");
                        break;
                    case "QUARTER":
                        result += new DateTime(date1.Ticks).ToString("yyyy-MM-dd ");
                        endDate1 = GetLastDayQuarter(date1);
                        date1 = endDate1.AddDays(1);
                        if (endDate1 > date2)
                            endDate1 = date2;
                        result += new DateTime(endDate1.Ticks).ToString("yyyy-MM-dd\n");
                        break;
                    case "YEAR":
                        result += new DateTime(date1.Ticks).ToString("yyyy-MM-dd ");
                        endDate1 = new DateTime(date1.Year + 1, 1, 1).AddDays(-1);
                        date1 = endDate1.AddDays(1);
                        if (endDate1 > date2)
                            endDate1 = date2;
                        result += new DateTime(endDate1.Ticks).ToString("yyyy-MM-dd\n");
                        break;
                    default:
                        date1 = date2.AddDays(1);
                        break;

                }
                counter++;
            }
        watch.Stop();
        Console.WriteLine(counter + "\n" + result);
    }

    public static DateTime GetLastDayQuarter(DateTime date)
    {
        DateTime buffer = date;
        return buffer.Date
    .AddDays(1 - buffer.Day)
    .AddMonths(3 - (buffer.Month - 1) % 3)
    .AddDays(-1);
    }
    public static DateTime GetLastDayReview(DateTime date)
    {
        if ((4 <= date.Month) && (date.Month <= 9))
        {
            return new DateTime(date.Year, 10, 1).AddDays(-1);
        }
        else
        {
            if (date.Month <= 3)
            {
                return new DateTime(date.Year, 4, 1).AddDays(-1);
            }
            else
            {
                return new DateTime(date.Year + 1, 4, 1).AddDays(-1);
            }
        }
    }
    public static DateTime GetFirstDayReview(DateTime date)
    {
        if ((4 <= date.Month) && (date.Month <= 9))
        {
            return new DateTime(date.Year, 4, 1);
        }
        else
        {
            if (date.Month <= 3)
            {
                return new DateTime(date.Year - 1, 10, 1);
            }
            else
            {
                return new DateTime(date.Year, 10, 1);
            }
        }
    }
    public static DateTime GetFirstDayOfWeek(DateTime date)
    {
        DateTime firstDayInWeek = date.Date;
        while (firstDayInWeek.DayOfWeek != DayOfWeek.Monday)
            firstDayInWeek = firstDayInWeek.AddDays(-1);

        return firstDayInWeek;
    }
    public static DateTime GetLastDayOfWeek(DateTime date)
    {
        DateTime lastDayInWeek = date.Date;
        while (lastDayInWeek.DayOfWeek != DayOfWeek.Sunday)
            lastDayInWeek = lastDayInWeek.AddDays(1);

        return lastDayInWeek;
    }
}

