using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Tooltip("Задержка таймера")]
    [SerializeField] private float _delay = 0.1f;

    private float _value;
    private Coroutine _coroutine;

    public event UnityAction<float> ValueChanged;

    private IEnumerator CountTime()
    {
        var _wait = new WaitForSecondsRealtime(_delay);

        while (true)
        {
            _value = Time.time;
            ValueChanged?.Invoke(_value);
            yield return _wait;
        }
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(CountTime());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }
}
