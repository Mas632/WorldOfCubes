using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubesCreator : MonoBehaviour
{
    [Header("Настройки создания оригинальных кубов")]
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

    [Header("Настройки создания дочерних кубов")]
    [Tooltip("Минимальное количество дочерних кубов")]
    [SerializeField, Range(1, 8)] private int _minChildCubesCount;
    [Tooltip("Максимальное количество дочерних кубов")]
    [SerializeField, Range(1, 8)] private int _maxChildCubesCount;

    public event UnityAction<int> NewCubesCreated;

    private void OnValidate()
    {
        if (_maxCubesCount < _minCubesCount)
        {
            _maxCubesCount = _minCubesCount;
        }

        if (_maxChildCubesCount < _minChildCubesCount)
        {
            _maxChildCubesCount = _minChildCubesCount;
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

    public List<Cube> CreateChildCubes(Cube parentCube)
    {
        int childCubesCount = Random.Range(_minChildCubesCount, _maxChildCubesCount + 1);
        int randomSpawnPointIndex;
        List<Vector3> spawnPoints = GenerateSpawnPoints(parentCube);
        List<Cube> childCubes = new();

        for (int i = 0; i < childCubesCount; i++)
        {
            if (spawnPoints.Count == 0)
            {
                break;
            }

            randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
            childCubes.Add(CreateCube(parentCube, spawnPoints[randomSpawnPointIndex]));
            spawnPoints.RemoveAt(randomSpawnPointIndex);
        }

        NewCubesCreated?.Invoke(childCubesCount);

        return childCubes;
    }

    private Cube CreateCube(Cube prototype, Vector3 spawnPoint)
    {
        int three = 3;
        float half = 0.5f;
        float halfInThirdDegree = Mathf.Pow(half, three);

        Cube newCube = Instantiate(prototype, spawnPoint, Quaternion.identity);

        newCube.transform.localScale = prototype.transform.localScale * half;
        newCube.SetChanceToSplit(prototype.ChanceToSplit * half);
        SetRandomColorToCube(newCube);

        if (newCube.gameObject.TryGetComponent(out Rigidbody childRigidBody) && prototype.gameObject.TryGetComponent(out Rigidbody parentRigidBody))
        {
            childRigidBody.mass = parentRigidBody.mass * halfInThirdDegree;
        }

        return newCube;
    }

    private void CreateOriginalCubes()
    {
        int cubesCount = Random.Range(_minCubesCount, _maxCubesCount + 1);

        for (int i = 0; i < cubesCount; i++)
        {
            CreateOriginalCube(Randomizer.GetPoint(_spawnArea));
        }

        NewCubesCreated?.Invoke(cubesCount);
    }

    private void CreateOriginalCube(Vector3 spawnPoint)
    {
        Cube newOriginalCube = Instantiate(_cubePrototype, spawnPoint, Random.rotation);

        if (_areNewCubesColored)
        {
            SetRandomColorToCube(newOriginalCube);
        }
    }

    private List<Vector3> GenerateSpawnPoints(Cube parentCube)
    {
        float half = 0.5f;
        float step = parentCube.gameObject.transform.localScale.x * half;
        float halfStep = step * half;
        Vector3 firstSpawnPoint = parentCube.gameObject.transform.position - Vector3.one * halfStep;
        Vector3 lastSpawnPoint = parentCube.gameObject.transform.position + Vector3.one * halfStep;
        List<Vector3> spawnPoints = new ();

        for (float x = firstSpawnPoint.x; x <= lastSpawnPoint.x; x += step)
        {
            for (float y = firstSpawnPoint.y; y <= lastSpawnPoint.y; y += step)
            {
                for (float z = firstSpawnPoint.z; z <= lastSpawnPoint.z; z += step)
                {
                    spawnPoints.Add(new Vector3(x, y, z));
                }
            }
        }

        return spawnPoints;
    }

    private void SetRandomColorToCube(Cube cube)
    {
        if (cube.gameObject.TryGetComponent(out Renderer renderer))
        {
            renderer.material.color = Randomizer.GetColor();
        }
    }
}
