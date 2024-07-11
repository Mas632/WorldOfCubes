using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZooming : MonoBehaviour
{
    [Header("��������� �����������/�������� ������")]
    [Tooltip("����������� ������")]
    [SerializeField] private Camera _camera;
    [Tooltip("�������� �����������/�������� ������")]
    [SerializeField] private float _speed;
    [Tooltip("������������ �����������")]
    [SerializeField] private float _maxZoomIn;
    [Tooltip("������������ ��������")]
    [SerializeField] private float _maxZoomOut;
    [Tooltip("����������� ������ �� ������ �����")]
    [SerializeField] private float _zoomAtStartup;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        _camera.orthographic = true;
        _camera.orthographicSize = _zoomAtStartup;
    }

    private void Update()
    {
        float newZoom = _camera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * _speed * Time.deltaTime;
        float correctedZoom = Mathf.Clamp(newZoom, _maxZoomIn, _maxZoomOut);

        _camera.orthographicSize = correctedZoom;
    }
}
