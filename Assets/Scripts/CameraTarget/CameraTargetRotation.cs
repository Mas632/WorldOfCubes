using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetRotation : MonoBehaviour
{
    [Header("Настройки вращения объекта")]
    [Tooltip("Скорость вращения")]
    [SerializeField] private float _speed;

    private float _startRotationAroundUpVector = 45;

    private float RotationInput => Input.GetAxis("Rotation");

    private void SetStartRotation()
    {
        transform.rotation = Quaternion.Euler(_startRotationAroundUpVector * Vector3.up);
    }

    private void Start()
    {
        SetStartRotation();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, RotationInput * _speed * Time.deltaTime);    
    }
}
