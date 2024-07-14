using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("�������� ����������, ���� ����� ���������� ����������")]
    [Tooltip("������� Text ��� ������ ���������� ���������� �����")]
    [SerializeField] private Text _leftCubesCountText;
    [Tooltip("������� Text ��� ������ ���������� ���������� �������")]
    [SerializeField] private Text _timerText;

    [Header("�������, ���������� �� ��������� ������ ���������")]
    [Tooltip("������, ������� ����� � ����� ������� ������")]
    [SerializeField] private CubesCounter _cubesCounter;
    [Tooltip("������, ������� ����� � ����� ������� �����")]
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
        _leftCubesCountText.text = $"�������� �����: {count} ";
    }

    public void DisplayTime(float time)
    {
        _timerText.text = " ������ �������: " + Assistant.TimerValueToString(time);
    }
}
