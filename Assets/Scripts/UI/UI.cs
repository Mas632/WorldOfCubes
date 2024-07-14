using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("Ёлементы интерфейса, куда будет выводитьс€ информаци€")]
    [Tooltip("Ёлемент Text дл€ вывода количества оставшихс€ кубов")]
    [SerializeField] private Text _leftCubesCountText;
    [Tooltip("Ёлемент Text дл€ вывода количества прошедшего времени")]
    [SerializeField] private Text _timerText;

    [Header("ќбъекты, сообщающие об изменении своего состо€ни€")]
    [Tooltip("ќбъект, который умеет и любит считать кубики")]
    [SerializeField] private CubesCounter _cubesCounter;
    [Tooltip("ќбъект, который умеет и любит считать врем€")]
    [SerializeField] private Timer _timer;

    private void OnEnable()
    {
        _cubesCounter.ValueChanged += DisplayLeftCubesCount;
        _timer.ValueChanged += DisplayTime;
    }

    private void OnDisable()
    {
        _cubesCounter.ValueChanged -= DisplayLeftCubesCount;
        _timer.ValueChanged -= DisplayTime;
    }

    public void DisplayLeftCubesCount(int count)
    {
        _leftCubesCountText.text = $"ќсталось кубов: {count} ";
    }

    public void DisplayTime(float time)
    {
        _timerText.text = " ѕрошло времени: " + Assistant.TimerValueToString(time);
    }
}
