using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private Manager _manager;
    [SerializeField] private Text _leftCubesCountText;
    [SerializeField] private Text _timerText;

    private string AddZeroInStringIfNeccessary(string str, int requiredStringLength = 2)
    {
        while (str.Length < requiredStringLength)
        {
            str = "0" + str;
        }

        return str;
    }

    private string TimerValueToString(float timerValue)
    {
        int secondsInHour = 3600;
        int secondsInMinute = 60;
        int hours;
        int minutes;
        int seconds;
        string result;

        hours = (int)(timerValue / secondsInHour);
        minutes = (int)((timerValue - hours * secondsInHour) / secondsInMinute);
        seconds = (int)(timerValue - hours * secondsInHour - minutes * secondsInMinute);

        result = $"{AddZeroInStringIfNeccessary(hours.ToString())}:{AddZeroInStringIfNeccessary(minutes.ToString())}:{AddZeroInStringIfNeccessary(seconds.ToString())}";

        return result;
    }

    private void OnEnable()
    {
        _manager.CubesCountChanged += DisplayLeftCubesCount;
        _manager.TimeChanged += DisplayTime;
    }

    private void OnDisable()
    {
        _manager.CubesCountChanged -= DisplayLeftCubesCount;
        _manager.TimeChanged += DisplayTime;
    }

    public void DisplayLeftCubesCount(int count)
    {
        _leftCubesCountText.text = $"Осталось кубов: {count} ";
    }

    public void DisplayTime(float time)
    {
        _timerText.text = " Прошло времени:" + TimerValueToString(time);
    }
}
