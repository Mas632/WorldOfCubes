using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private const string RotationAxisName = "Rotation";

    [Header("Настройки вращения объекта")]
    [Tooltip("Скорость вращения")]
    [SerializeField] private float _speed;

    private float _startRotationAroundUpVector = 45;

    private float RotationInput => Input.GetAxis(RotationAxisName);

    private void Start()
    {
        SetStartRotation();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, RotationInput * _speed * Time.deltaTime);    
    }

    private void SetStartRotation()
    {
        transform.rotation = Quaternion.Euler(_startRotationAroundUpVector * Vector3.up);
    }
}
