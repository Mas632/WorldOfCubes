using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _radius;

    public void ApplyExplosionForce(List<GameObject> appliedObjects, Vector3 explosionCenter)
    {
        transform.position = explosionCenter;

        if (gameObject.TryGetComponent(out ParticleSystem particleSystem))
        {
            particleSystem.Play();
        }

        foreach (GameObject appliedObject in appliedObjects)
        {
            if (appliedObject.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_force, explosionCenter, _radius);
            }
        }
    }
}
