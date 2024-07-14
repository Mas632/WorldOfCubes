using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Tooltip("Задержка таймера")]
    [SerializeField] private float _delay = 0.1f;

    private Coroutine _coroutine;

    public event UnityAction<float> ValueChanged;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(CountTime());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator CountTime()
    {
        var _wait = new WaitForSecondsRealtime(_delay);

        while (true)
        {
            ValueChanged?.Invoke(Time.time);
            yield return _wait;
        }
    }
}
