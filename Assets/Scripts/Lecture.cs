using System.Collections;
using UnityEngine;

public class Lecture : MonoBehaviour
{
    public TextWriting Writing;
    public float Duration;

    private IEnumerator Start()
    {
        var startTime = Time.time;
        var factor = 0f;

        while (factor < 1f)
        {
            Writing.SetAmountWritten(factor);
            yield return null;
            factor = (Time.time - startTime) / Duration;
        }

        Writing.SetAmountWritten(1f);

        foreach (var item in Writing.GetDistribution())
        {
            if (item.Value is ScriptableObject)
            {
                print($"{(item.Value as ScriptableObject).name} - {item.Key}");
            }
        }
    }

}