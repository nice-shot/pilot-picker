using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LectureTests
    {
        [Test]
        public void Test_Lecture_GenerateFlow()
        {
            var styleDistribution = new Dictionary<int, float>()
            {
                { 0, 0.6f },
                { 1, 0.2f },
                { 2, 0.15f },
                { 3, 0.05f },
            };
            var duration = 30f;
            var flow = Lecture.GenerateFlow(duration, styleDistribution, 0);

            var outputDistribution = new Dictionary<int, float>();
            var prevKey = 0f;
            foreach (var item in flow)
            {
                var addedDistributionPercentage = (item.Key - prevKey) / duration;

                if (outputDistribution.ContainsKey(item.Value))
                {
                    outputDistribution[item.Value] += addedDistributionPercentage;
                }
                else
                {
                    outputDistribution[item.Value] = addedDistributionPercentage;
                }
                prevKey = item.Key;
            }

            Assert.AreEqual(styleDistribution.Count, outputDistribution.Count);
            foreach (var item in styleDistribution)
            {
                Assert.AreEqual(item.Value, outputDistribution[item.Key], 0.0001f);
            }
        }
    }
}
