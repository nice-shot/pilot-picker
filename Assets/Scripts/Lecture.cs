using System.Linq;
using System.Collections;
using System.Collections.Generic;
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

    public static SortedDictionary<float, int> GenerateFlow(
        float duration,
        Dictionary<int, float> distribution,
        int seed=-1)
    {
        if (seed != -1) Random.InitState(seed);

        var flow = new SortedDictionary<float, int>();
        var currentTime = 0f;
        var timedDistribution = distribution.ToDictionary(item => item.Key, item => item.Value * duration);
        var minDuration = 2f;

        while (currentTime < duration)
        {
            var styleIndex = Random.Range(0, timedDistribution.Keys.Count);
            var maxDuration = timedDistribution[styleIndex];
            if (maxDuration <= 0f) continue;

            var styleDuration = Random.Range(
                Mathf.Min(minDuration, maxDuration),
                maxDuration
            );
            timedDistribution[styleIndex] -= styleDuration;
            currentTime += styleDuration;
            flow[currentTime] = styleIndex;
        }

        return flow;
    }

}