using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LectureFlow : List<LectureSegment>
{
    /// <summary>
    /// Generates a random lecture flow with the given duration and distribution.
    /// </summary>
    public void Generate(float duration, Dictionary<IStyle, float> distribution, int seed = -1)
    {
        Clear();

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
            Add(new LectureSegment(styleDuration, style));
        }
    }

    /// <summary>
    /// Returns how close this flow is to the other based on the distribution.
    /// '0' means equal and anything above shows how different it is.
    /// </summary>
    public float Compare(LectureFlow other)
    {
        // Order distributions by the most prominent style.
        var myDist = GetDistribution().OrderBy(item => item.Value).ToList();
        var otherDist = other.GetDistribution().OrderBy(item => item.Value).ToList();
        var diff = 0f;

        // Iterate on the bigger of the two to get all of the values.
        var biggerDist = myDist.Count > otherDist.Count ? myDist : otherDist;
        var smallerDist = myDist == biggerDist ? otherDist : myDist;

        for (int i = 0; i < biggerDist.Count; i++)
        {
            var first = biggerDist[i];
            if (i < smallerDist.Count)
            {
                var second = smallerDist[i];
                diff += Mathf.Abs(first.Value - second.Value);
            }
            else
            {
                diff += first.Value;
            }
        }

        return diff;
    }

    public Dictionary<IStyle, float> GetDistribution()
    {
        var distribution = new Dictionary<IStyle, float>();
        var totalDuration = this.Sum(segment => segment.Duration);

        foreach (var segment in this)
            {
                var addedDistributionPercentage = segment.Duration / totalDuration;

                if (distribution.ContainsKey(segment.Style))
                {
                    distribution[segment.Style] += addedDistributionPercentage;
                }
                else
                {
                    distribution[segment.Style] = addedDistributionPercentage;
                }
            }

        return distribution;
    }

    public override string ToString()
    {
        var output = new StringBuilder("Flow:\n");
        foreach (var segment in this)
        {
            output.AppendLine($"{segment.Duration} - {segment.Style}");
        }
        return output.ToString();
    }
}