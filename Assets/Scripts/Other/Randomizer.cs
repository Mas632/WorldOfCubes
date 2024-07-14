using UnityEngine;

public static class Randomizer
{
    private static Color[] _colors = new Color[]
    {
        Color.black,
        Color.blue,
        Color.cyan,
        Color.green,
        Color.grey,
        Color.magenta,
        Color.red,
        Color.yellow,
        Color.white
    };

    public static bool IsSuccessed(float chance)
    {
        return chance >= Random.value;
    }

    public static Color GetColor()
    {
        return _colors[Random.Range(0, _colors.Length)];
    }

    public static Vector3 GetPoint(Transform sourceArea)
    {
        return new Vector3(
            Random.Range(sourceArea.transform.position.x - sourceArea.transform.localScale.x / 2, sourceArea.transform.position.x + sourceArea.transform.localScale.x / 2),
            Random.Range(sourceArea.transform.position.y - sourceArea.transform.localScale.y / 2, sourceArea.transform.position.y + sourceArea.transform.localScale.y / 2),
            Random.Range(sourceArea.transform.position.z - sourceArea.transform.localScale.z / 2, sourceArea.transform.position.z + sourceArea.transform.localScale.z / 2));
    }
}
