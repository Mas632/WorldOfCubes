using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetMovement : MonoBehaviour
{
    [Header("Настройки перемещения объекта")]
    [Tooltip("Скорость перемещения объекта")]
    [SerializeField, Min(0f)] private float _speed;

    private float HorizontalInput => Input.GetAxis("Horizontal");
    private float VerticalInput => Input.GetAxis("Vertical");

    private Vector3 _startPosition = new Vector3(-2.6f, 0, 8.2f);

    private void SetStartPosition()
    {
        transform.position = _startPosition;
    }

    private void Start()
    {
        SetStartPosition();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * HorizontalInput * _speed * Time.deltaTime +
                            Vector3.forward * VerticalInput * _speed * Time.deltaTime);
    }
}
