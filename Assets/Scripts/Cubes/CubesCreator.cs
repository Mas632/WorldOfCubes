using System.Collections.Generic;
using UnityEngine;

public class CubesCreator : MonoBehaviour
{
    [Header("Настройки создания кубов")]
    [Tooltip("Прототип, используемый для создания кубов")]
    [SerializeField] private GameObject _cubePrototype;
    [Tooltip("Область пространста, в которой появляются создаваемые кубы")]
    [SerializeField] private Transform _spawnArea;
    [Tooltip("Минимальное количество кубов, создаваемых за один раз")]
    [SerializeField, Range(1, 100)] private int _minCubesCount;
    [Tooltip("Максимальное количество кубов, создаваемых за один раз")]
    [SerializeField, Range(1, 100)] private int _maxCubesCount;
    [Tooltip("Делать новые кубики разноцветными?")]
    [SerializeField] private bool _areNewCubesColored;

    [Space(20)]
    [Tooltip("Ссылка на менеджер сцены")]
    [SerializeField] private Manager _manager;

    private void SubscribeCubeEvents(CubeBehaviour cubeBehaviour)
    {
        cubeBehaviour.Created += _manager.IncrementCubesCount;
        cubeBehaviour.Destroyed += _manager.DecrementCubesCount;
        cubeBehaviour.SplitMe += _manager.SplitCube;
    }

    private void SetRandomColorToCube(GameObject cube)
    {
        if (cube.TryGetComponent(out Renderer renderer))
        {
            renderer.material.color = Randomizer.GetColor();
        }
    }

    private GameObject CreateOriginalCube(Vector3 spawnPoint)
    {
        GameObject newOriginalCube = Instantiate(_cubePrototype, spawnPoint, Random.rotation);

        if (newOriginalCube.TryGetComponent(out CubeBehaviour cubeBehaviour))
        {
            SubscribeCubeEvents(cubeBehaviour);
        }

        if (_areNewCubesColored)
        {
            SetRandomColorToCube(newOriginalCube);
        }

        return newOriginalCube;
    }

    private void OnValidate()
    {
        if (_maxCubesCount < _minCubesCount)
        {
            _maxCubesCount = _minCubesCount;
        }
    }

    public GameObject CreateCube(GameObject prototype, Vector3 spawnPoint)
    {
        float half = 0.5f;
        float halfInThirdDegree = Mathf.Pow(half, 3);

        GameObject newCube = Instantiate(prototype, spawnPoint, Quaternion.identity);

        newCube.transform.localScale = prototype.transform.localScale * half;
        SetRandomColorToCube(newCube);

        if (newCube.TryGetComponent(out CubeBehaviour childCubeBehaviour) && prototype.TryGetComponent(out CubeBehaviour parentCubeBehaviour))
        {
            childCubeBehaviour.ChangeChanceToSplit(parentCubeBehaviour.ChanceToSplit * half);
            SubscribeCubeEvents(childCubeBehaviour);
        }

        if (newCube.TryGetComponent(out Rigidbody childRigidBody) && prototype.TryGetComponent(out Rigidbody parentRigidBody))
        {
            childRigidBody.mass = parentRigidBody.mass * halfInThirdDegree;
        }

        return newCube;
    }

    public List<GameObject> CreateOriginalCubes()
    {
        int cubesCount = Random.Range(_minCubesCount, _maxCubesCount + 1);
        List<GameObject> _cubes = new();

        for (int i = 0; i < cubesCount; i++)
        {
            _cubes.Add(CreateOriginalCube(Randomizer.GetPoint(_spawnArea)));
        }

        return _cubes;
    }
}
