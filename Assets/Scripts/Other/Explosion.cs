using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Настройки взрыва")]
    [Tooltip("Силы взрыва")]
    [SerializeField, Min(0f)] private float _force = 500f;
    [Tooltip("Расстояние от центра, на котором действует сила взрыва")]
    [SerializeField, Min(0f)] private float _radius = 3f;

    public void Explode(Vector3 explosionCenter, float explosionModifier = 1f)
    {
        float force = _force * explosionModifier;
        float radius = _radius * explosionModifier;

        transform.position = explosionCenter;

        if (gameObject.TryGetComponent(out ParticleSystem particleSystem))
        {
            particleSystem.Play();
        }

        Collider[] explodingObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider explodingObject in explodingObjects)
        {
            if (explodingObject.gameObject.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(force, explosionCenter, radius);
            }
        }
    }
}
