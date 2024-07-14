using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZooming : MonoBehaviour
{
    private const string ZoomAxisName = "Mouse ScrollWheel";

    [Header("Настройки приближения/удаления камеры")]
    [Tooltip("Управляемая камера")]
    [SerializeField] private Camera _camera;
    [Tooltip("Скорость приближения/удаления камеры")]
    [SerializeField] private float _speed;
    [Tooltip("Максимальное приближение")]
    [SerializeField] private float _maxZoomIn;
    [Tooltip("Максимальное удаление")]
    [SerializeField] private float _maxZoomOut;
    [Tooltip("Приближение камеры на старте сцены")]
    [SerializeField] private float _zoomAtStartup;

    private float ZoomInput => Input.GetAxis(ZoomAxisName);

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
        float newZoom = _camera.orthographicSize - ZoomInput * _speed * Time.deltaTime;
        float correctedZoom = Mathf.Clamp(newZoom, _maxZoomIn, _maxZoomOut);

        _camera.orthographicSize = correctedZoom;
    }
}
