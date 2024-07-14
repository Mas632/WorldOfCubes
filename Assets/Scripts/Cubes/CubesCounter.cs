using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubesCounter : MonoBehaviour
{
    private int _cubesCount;

    public event UnityAction<int> ValueChanged;

    public void IncrementCubesCount()
    {
        _cubesCount++;
        ValueChanged?.Invoke(_cubesCount);
    }

    public void DecrementCubesCount()
    {
        _cubesCount--;
        ValueChanged?.Invoke(_cubesCount);
    }
}
