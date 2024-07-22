using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Настройки взрыва")]
    [Tooltip("Силы взрыва")]
    [SerializeField, Min(0f)] private float _force = 500f;
    [Tooltip("Расстояние от центра, на котором действует сила взрыва")]
    [SerializeField, Min(0f)] private float _radius = 3f;

    public void ExplodeAround(Vector3 explosionCenter, float explosionModifier = 1f)
    {
        List<Cube> cubesAround = new();
        Collider[] affectedColliders = Physics.OverlapSphere(explosionCenter, _radius * explosionModifier);

        foreach (Collider collider in affectedColliders)
        {
            if (collider.gameObject.TryGetComponent(out Cube cube))
            {
                cubesAround.Add(cube);
            }
        }

        ExplodeCubes(explosionCenter, cubesAround, explosionModifier);
    }

    public void ExplodeCubes(Vector3 explosionCenter, List<Cube> cubes, float explosionModifier = 1f)
    {
        float force = _force * explosionModifier;
        float radius = _radius * explosionModifier;

        transform.position = explosionCenter;

        if (gameObject.TryGetComponent(out ParticleSystem particleSystem))
        {
            particleSystem.Play();
        }

        foreach (Cube cube in cubes)
        {
            if (cube.gameObject.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(force, explosionCenter, radius);
            }
        }
    }
}
