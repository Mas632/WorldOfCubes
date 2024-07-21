using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubesClicker : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CubesCreator _cubesCreator;
    [Tooltip("Взрыв для разброса дочерних кубов")]
    [SerializeField] private Explosion _explosion;

    private int _cubesCount = 0;

    public event UnityAction<int> CubesCountChanged;

    private void OnEnable()
    {
        _cubesCreator.NewCubesCreated += IncreaseCubesCount;
    }

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out Cube cube))
                {
                    if (Randomizer.IsSuccessed(cube.ChanceToSplit))
                    {
                        List<Cube> childCubes = _cubesCreator.CreateChildCubes(cube);
                        _explosion.ApplyExplosionForce(childCubes, cube.gameObject.transform.position);
                    }

                    Destroy(cube.gameObject);
                    IncreaseCubesCount(-1);
                }
            }
        }
    }

    private void OnDisable()
    {
        _cubesCreator.NewCubesCreated -= IncreaseCubesCount;
    }

    private void IncreaseCubesCount(int value)
    {
        _cubesCount += value;
        CubesCountChanged?.Invoke(_cubesCount);
    }
}
