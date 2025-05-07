using UnityEngine;

public class EilerFormula
{
    //private const float E = 2.7f;

    public float EilerCounting(float startCounting, int level, float scaler)
    {
        float result = startCounting * Mathf.Pow(scaler, (float)level);
        return result;
    }
}
