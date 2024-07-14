using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    [Header("Механизм самоуничтожения объекта")]
    [Tooltip("Механизм самоуничтожения объекта активен")]
    [SerializeField] private bool _isSelfDestroyMechanismActived = true;
    [Tooltip("Максимальная дистанция от начала координат, на которой происходит самоуничтожение объекта")]
    [SerializeField, Min(0f)] private float _maxDistance = 100f;

    private float _chanceToSplit = 1f;

    public event UnityAction Created;
    public event UnityAction Destroyed;
    public event UnityAction<Cube> SuccessedCliked;

    private bool ShouldBeDestroyed => transform.position.magnitude >= _maxDistance;
    public float ChanceToSplit => _chanceToSplit;

    private void Start()
    {
        Created?.Invoke();
    }

    private void Update()
    {
        if (_isSelfDestroyMechanismActived)
        {
            if (ShouldBeDestroyed)
            {
                SelfDestroy();
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (Randomizer.IsSuccessed(_chanceToSplit))
            {
                SuccessedCliked(this);
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

    public void SetChanceToSplitByHalf(float value)
    {
        _chanceToSplit = Mathf.Clamp(value, 0f, 1f);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
