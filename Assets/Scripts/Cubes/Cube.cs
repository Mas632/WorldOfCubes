using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _chanceToSplit = 1f;

    public float ChanceToSplit
    {
        get { return _chanceToSplit; }
        private set { _chanceToSplit = Mathf.Clamp(value, 0f, 1f);}
    }

    public void SetChanceToSplit(float value)
    {
        _chanceToSplit = value;
    }
}
