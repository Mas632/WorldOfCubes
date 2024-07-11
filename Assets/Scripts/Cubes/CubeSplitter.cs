using System.Collections.Generic;
using UnityEngine;

public class CubeSplitter : MonoBehaviour
{
    [Header("Настройки расщепления кубов")]
    [Tooltip("Объект, который может и хочет создавать кубы")]
    [SerializeField] private CubesCreator _cubesCreator;
    [Tooltip("Минимальное количество дочерних кубов")]
    [SerializeField, Range(1, 8)] private int _minChildCubesCount;
    [Tooltip("Максимальное количество дочерних кубов")]
    [SerializeField, Range(1, 8)] private int _maxChildCubesCount;

    [Header("Настройки взрыва создаваемых кубов")]
    [Tooltip("Тот объект, который будет их подрывать")]
    [SerializeField] private Explosion _explosion;
    [Tooltip("А надо ли их вообще подрывать?")]
    [SerializeField] private bool _isExplodable;

    private List<Vector3> GenerateSpawnPoints(GameObject parentCube)
    {
        List<Vector3> spawnPoints = new();
        float step = parentCube.transform.localScale.x / 2;
        float halfStep = step / 2;
        Vector3 firstSpawnPoint = parentCube.transform.position - Vector3.one * halfStep;
        Vector3 lastSpawnPoint = parentCube.transform.position + Vector3.one * halfStep;

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

    private void OnValidate()
    {
        if (_maxChildCubesCount < _minChildCubesCount)
        {
            _maxChildCubesCount = _minChildCubesCount;
        }
    }

    public void DoWork(GameObject parentCube)
    {
        int childCubesCount = Random.Range(_minChildCubesCount, _maxChildCubesCount + 1);
        int randomIndexForSpawnPoint;
        List<Vector3> spawnPoints = GenerateSpawnPoints(parentCube);
        List<GameObject> childCubes = new();

        for (int i = 0; i < childCubesCount; i++)
        {
            randomIndexForSpawnPoint = Random.Range(0, spawnPoints.Count);
            childCubes.Add(_cubesCreator.CreateCube(parentCube, spawnPoints[randomIndexForSpawnPoint]));
            spawnPoints.RemoveAt(randomIndexForSpawnPoint);

            if (spawnPoints.Count == 0)
            {
                break;
            }
        }

        if (_isExplodable)
        {
            _explosion.ApplyExplosionForce(childCubes, parentCube.transform.position);
        }

        Destroy(parentCube);
    }
}
