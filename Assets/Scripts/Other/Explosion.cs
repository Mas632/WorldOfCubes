using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Настройки взрыва")]
    [Tooltip("Силы взрыва")]
    [SerializeField, Min(0f)] private float _force = 500f;
    [Tooltip("Расстояние от центра, на котором действует сила взрыва")]
    [SerializeField, Min(0f)] private float _radius = 3f;

    public void ApplyExplosionForce(List<Cube> appliedObjects, Vector3 explosionCenter)
    {
        transform.position = explosionCenter;

        if (gameObject.TryGetComponent(out ParticleSystem particleSystem))
        {
            particleSystem.Play();
        }

        foreach (Cube appliedObject in appliedObjects)
        {
            if (appliedObject.gameObject.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_force, explosionCenter, _radius);
            }
        }
    }
}
