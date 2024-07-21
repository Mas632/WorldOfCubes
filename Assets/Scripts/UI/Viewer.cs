using UnityEngine;
using UnityEngine.UI;

public class Viewer : MonoBehaviour
{
    [Header("�������� ����������, ���� ����� ���������� ����������")]
    [Tooltip("������� Text ��� ������ ���������� ���������� �����")]
    [SerializeField] private Text _leftCubesCountText;
    [Tooltip("������� Text ��� ������ ���������� ���������� �������")]
    [SerializeField] private Text _timerText;

    [Header("�������, ���������� �� ��������� ������ ���������")]
    [Tooltip("������, ������� ����� � ����� ������� �����")]
    [SerializeField] private Timer _timer;
    [Tooltip("������, ������� �������� �� ��������� ���������� �������")]
    [SerializeField] private CubesClicker _cubesClicker;

    private void OnEnable()
    {
        _cubesClicker.CubesCountChanged += DisplayLeftCubesCount;
        _timer.ValueChanged += DisplayTime;
    }
    
    private void OnDisable()
    {
        _cubesClicker.CubesCountChanged -= DisplayLeftCubesCount;
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
