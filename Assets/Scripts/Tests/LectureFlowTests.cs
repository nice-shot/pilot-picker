using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LectureFlowTests
    {
        [Test]
        public void TestInitializeRandom()
        {
            var s1 = ScriptableObject.CreateInstance<TeachingStyle>();
            var s2 = ScriptableObject.CreateInstance<TeachingStyle>();
            var s3 = ScriptableObject.CreateInstance<TeachingStyle>();
            var s4 = ScriptableObject.CreateInstance<TeachingStyle>();

            var styleDistribution = new Dictionary<IStyle, float>()
            {
                { s1, 0.6f },
                { s2, 0.2f },
                { s3, 0.15f },
                { s4, 0.05f },
            };

            var totalDuration = 30f;
            var flow = new LectureFlow();
            flow.Generate(totalDuration, styleDistribution, 0);

            var outputDistribution = flow.GetDistribution();
            Assert.AreEqual(styleDistribution.Count, outputDistribution.Count);
            foreach (var item in styleDistribution)
            {
                Assert.AreEqual(item.Value, outputDistribution[item.Key], 0.0001f);
            }
        }

        [Test]
        public void TestFlowComparison()
        {
            var ts1 = ScriptableObject.CreateInstance<TeachingStyle>();
            var ts2 = ScriptableObject.CreateInstance<TeachingStyle>();
            var ts3 = ScriptableObject.CreateInstance<TeachingStyle>();
            var ws1 = ScriptableObject.CreateInstance<WritingStyle>();
            var ws2 = ScriptableObject.CreateInstance<WritingStyle>();
            var ws3 = ScriptableObject.CreateInstance<WritingStyle>();

            var teachingFlow = new LectureFlow
            {
                new LectureSegment(4f, ts1),
                new LectureSegment(6f, ts3),
                new LectureSegment(2f, ts1),
                new LectureSegment(4f, ts2)
            };
            var writingFlow = new LectureFlow
            {
                new LectureSegment(4f, ws1),
                new LectureSegment(6f, ws3),
                new LectureSegment(2f, ws1),
                new LectureSegment(4f, ws2)
            };

            Assert.AreEqual(0f, teachingFlow.Compare(writingFlow));

            writingFlow.Add(new LectureSegment(2f, ws1));

            Assert.AreEqual(0.138f, teachingFlow.Compare(writingFlow), 0.001f);

            writingFlow = new LectureFlow
            {
                new LectureSegment(2f, ws1),
                new LectureSegment(8f, ws2),
                new LectureSegment(1f, ws3),
                new LectureSegment(5f, ws2)
            };

            Assert.AreEqual(0.875f, teachingFlow.Compare(writingFlow));

        }
    }
}
