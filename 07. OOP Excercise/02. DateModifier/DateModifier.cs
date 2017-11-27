using System;

public class DateModifier
{
    private DateTime[] dates;

    public DateModifier()
    {
        this.dates = new DateTime[2];
    }

    public void AddDate(DateTime date, int position)
    {
        dates[position] = date;
    }

    public int GetDiff()
    {
        return Math.Abs(dates[0].Subtract(dates[1]).Days);
    }
}