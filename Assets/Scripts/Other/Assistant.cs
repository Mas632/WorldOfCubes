public static class Assistant
{
    public static string TimerValueToString(float timerValue)
    {
        int secondsInHour = 3600;
        int secondsInMinute = 60;
        int hours = (int)(timerValue / secondsInHour);
        int minutes = (int)((timerValue - hours * secondsInHour) / secondsInMinute);
        int seconds = (int)(timerValue - hours * secondsInHour - minutes * secondsInMinute); 

        return $"{AddZeroInStringIfNeccessary(hours.ToString())}:{AddZeroInStringIfNeccessary(minutes.ToString())}:{AddZeroInStringIfNeccessary(seconds.ToString())}";
    }

    private static string AddZeroInStringIfNeccessary(string str, int requiredStringLength = 2)
    {
        while (str.Length < requiredStringLength)
        {
            str = "0" + str;
        }

        return str;
    }
}
