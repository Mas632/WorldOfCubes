using UnityEngine;
using UnityEngine.UI;

public class Viewer : MonoBehaviour
{
    [Header("Ёлементы интерфейса, куда будет выводитьс€ информаци€")]
    [Tooltip("Ёлемент Text дл€ вывода количества оставшихс€ кубов")]
    [SerializeField] private Text _leftCubesCountText;
    [Tooltip("Ёлемент Text дл€ вывода количества прошедшего времени")]
    [SerializeField] private Text _timerText;

    [Header("ќбъекты, сообщающие об изменении своего состо€ни€")]
    [Tooltip("ќбъект, который умеет и любит считать врем€")]
    [SerializeField] private Timer _timer;
    [Tooltip(" ликер, который сообщает об изменении количества кубиков")]
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
        _leftCubesCountText.text = $"ќсталось кубов: {count} ";
    }

    public void DisplayTime(float time)
    {
        _timerText.text = " ѕрошло времени: " + Assistant.TimerValueToString(time);
    }
}
