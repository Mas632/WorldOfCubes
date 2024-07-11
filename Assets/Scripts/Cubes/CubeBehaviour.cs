using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CubeBehaviour : MonoBehaviour
{
    [Header("Механизм самоуничтожения объекта")]
    [Tooltip("Механизм самоуничтожения объекта активен")]
    [SerializeField] private bool _isSelfDestroyMechanismActived;
    [Tooltip("Максимальная дистанция от начала координат, на которой происходит самоуничтожение объекта")]
    [SerializeField, Min(0f)] private float _maxDistance;

    private float _chanceToSplit = 1f;

    private bool ShouldBeDestroyed => transform.position.magnitude >= _maxDistance;
    public float ChanceToSplit => _chanceToSplit;

    public event UnityAction Created;
    public event UnityAction Destroyed;
    public event UnityAction<GameObject> SplitMe;

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        Created?.Invoke();
    }

    private void Update()
    {
        if (ShouldBeDestroyed)
        {
            SelfDestroy();
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            if (Randomizer.IsSuccessed(_chanceToSplit))
            {
                SplitMe?.Invoke(gameObject);
            }
            else
            {
                SelfDestroy();
            }
        }
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    public void ChangeChanceToSplit(float value)
    {
        float minValueForChanceToSplit = 0f;
        float maxValueForChanceToSplit = 1f;

        _chanceToSplit = Mathf.Clamp(value, minValueForChanceToSplit, maxValueForChanceToSplit);
    }
}
