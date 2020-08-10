using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LectureFlow : IEnumerable<LectureSegment>
{
    private List<LectureSegment> _segments = new List<LectureSegment>();

    /// <summary>
    /// Creates an empty LectureFlow.
    /// </summary>
    public LectureFlow() {}

    /// <summary>
    /// Generates a random lecture flow with the given duration and distribution.
    /// </summary>
    public LectureFlow(float duration, Dictionary<IStyle, float> distribution, int seed=-1)
    {
        if (seed != -1) Random.InitState(seed);

        var calculatedTime = 0f;
        var timedDistribution = distribution.ToDictionary(item => item.Key, item => item.Value * duration);
        var styles = timedDistribution.Keys.ToArray();
        var minDuration = 2f;

        while (calculatedTime < duration)
        {
            var style = styles[Random.Range(0, styles.Length)];
            var maxDuration = timedDistribution[style];
            if (maxDuration <= 0f) continue;

            var styleDuration = Random.Range(
                Mathf.Min(minDuration, maxDuration),
                maxDuration
            );
            timedDistribution[style] -= styleDuration;
            calculatedTime += styleDuration;
            _segments.Add(new LectureSegment { Duration = styleDuration, Style = style });
        }
    }

    public IEnumerator<LectureSegment> GetEnumerator()
    {
        return _segments.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _segments.GetEnumerator();
    }
}