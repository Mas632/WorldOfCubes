using System.Collections.Generic;
using UnityEngine;

public class CubesCreator : MonoBehaviour
{
    [Header("Настройки создания кубов")]
    [Tooltip("Прототип, используемый для создания кубов")]
    [SerializeField] private Cube _cubePrototype;
    [Tooltip("Область пространста, в которой появляются создаваемые кубы")]
    [SerializeField] private Transform _spawnArea;
    [Tooltip("Минимальное количество кубов, создаваемых за один раз")]
    [SerializeField, Range(1, 100)] private int _minCubesCount;
    [Tooltip("Максимальное количество кубов, создаваемых за один раз")]
    [SerializeField, Range(1, 100)] private int _maxCubesCount;
    [Tooltip("Делать новые кубики разноцветными?")]
    [SerializeField] private bool _areNewCubesColored;

    [Space(25)]
    [Tooltip("Объект, который умеет и любит считать кубики")]
    [SerializeField] private CubesCounter _cubesCounter;
    [Tooltip("Объект, который умеет и любит делить кубики на дочерние кубики")]
    [SerializeField] private CubeSplitter _cubeSplitter;

    private void OnValidate()
    {
        if (_maxCubesCount < _minCubesCount)
        {
            _maxCubesCount = _minCubesCount;
        }
    }

    private void Start()
    {
        CreateOriginalCubes();
    }

    private void Update()
    {
        KeyCode keyForCreatingCubes = KeyCode.Space;

        if (Input.GetKey(keyForCreatingCubes))
        {
            CreateOriginalCubes();
        }
    }

    public Cube CreateCube(Cube prototype, Vector3 spawnPoint)
    {
        int three = 3;
        float half = 0.5f;
        float halfInThirdDegree = Mathf.Pow(half, three);

        Cube newCube = Instantiate(prototype, spawnPoint, Quaternion.identity);

        newCube.transform.localScale = prototype.transform.localScale * half;

        newCube.SetChanceToSplitByHalf(prototype.ChanceToSplit * half);
        SetRandomColorToCube(newCube);

        if (newCube.gameObject.TryGetComponent(out Rigidbody childRigidBody) && prototype.gameObject.TryGetComponent(out Rigidbody parentRigidBody))
        {
            childRigidBody.mass = parentRigidBody.mass * halfInThirdDegree;
        }

        CustomizeCube(newCube);

        return newCube;
    }

    public List<Cube> CreateOriginalCubes()
    {
        int cubesCount = Random.Range(_minCubesCount, _maxCubesCount + 1);
        List<Cube> _cubes = new List<Cube>();

        for (int i = 0; i < cubesCount; i++)
        {
            _cubes.Add(CreateOriginalCube(Randomizer.GetPoint(_spawnArea)));
        }

        return _cubes;
    }

    private void SetRandomColorToCube(Cube cube)
    {
        if (cube.gameObject.TryGetComponent(out Renderer renderer))
        {
            renderer.material.color = Randomizer.GetColor();
        }
    }

    private void CustomizeCube(Cube cube)
    {
        cube.Created += _cubesCounter.IncrementCubesCount;
        cube.Destroyed += _cubesCounter.DecrementCubesCount;
        cube.SuccessedCliked += _cubeSplitter.OnCubeClicked;
    }

    private Cube CreateOriginalCube(Vector3 spawnPoint)
    {
        Cube newOriginalCube = Instantiate(_cubePrototype, spawnPoint, Random.rotation);

        if (_areNewCubesColored)
        {
            SetRandomColorToCube(newOriginalCube);
        }

        CustomizeCube(newOriginalCube);

        return newOriginalCube;
    }
}
