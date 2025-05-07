using UnityEngine;

public class EilerFormula
{
    /* Это инкапсулированный скрипт, который вычисляет нужное число по формуле C = C0 * e(l) */

    //private const float E = 2.7f;

    /* E = 2.7f; Эта хрень не нужна теперь, поскольку мы уже с других скриптов, откуда делаем вызов,
    предоставляем множитель вместе с другими вводными данными */

    public float EilerCounting(float startCounting, int level, float scaler)
    {
        float result = startCounting * Mathf.Pow(scaler, (float)level);
        return result;
    }
}
