using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    [SerializeField] private CubesCreator _cubesCreator;
    [SerializeField] private CubeSplitter _cubeSplitter;
    [SerializeField] private Timer _timer;

    private int _cubesCount;

    public event UnityAction<int> CubesCountChanged;
    public event UnityAction<float> TimeChanged;

    private void Start()
    {
        _cubesCount = 0;
        _cubesCreator.CreateOriginalCubes();
    }

    private void OnEnable()
    {
        _timer.ValueChanged += Tick;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _cubesCreator.CreateOriginalCubes();
        }
    }

    private void OnDisable()
    {
        _timer.ValueChanged -= Tick;
    }

    public void IncrementCubesCount()
    {
        _cubesCount++;
        CubesCountChanged?.Invoke(_cubesCount);
    }

    public void DecrementCubesCount()
    {
        _cubesCount--;
        CubesCountChanged?.Invoke(_cubesCount);
    }

    public void SplitCube(GameObject cubeToSplit)
    {
        _cubeSplitter.DoWork(cubeToSplit);
    }

    public void Tick(float tickerValue)
    {
        TimeChanged.Invoke(tickerValue);
    }
}
