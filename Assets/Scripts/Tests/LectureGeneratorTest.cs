using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LectureFlowTest
    {
        [Test]
        public void TestRandomFlow()
        {
            var s1 = new TeachingStyle();
            var s2 = new TeachingStyle();
            var s3 = new TeachingStyle();
            var s4 = new TeachingStyle();

            var styleDistribution = new Dictionary<IStyle, float>()
            {
                { s1, 0.6f },
                { s2, 0.2f },
                { s3, 0.15f },
                { s4, 0.05f },
            };

            var totalDuration = 30f;
            var flow = new LectureFlow(totalDuration, styleDistribution, 0);

            var outputDistribution = new Dictionary<IStyle, float>();
            foreach (var segment in flow)
            {
                var addedDistributionPercentage = segment.Duration / totalDuration;

                if (outputDistribution.ContainsKey(segment.Style))
                {
                    outputDistribution[segment.Style] += addedDistributionPercentage;
                }
                else
                {
                    outputDistribution[segment.Style] = addedDistributionPercentage;
                }
            }

            Assert.AreEqual(styleDistribution.Count, outputDistribution.Count);
            foreach (var item in styleDistribution)
            {
                Assert.AreEqual(item.Value, outputDistribution[item.Key], 0.0001f);
            }
        }
    }
}
