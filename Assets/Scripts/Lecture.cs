using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture : MonoBehaviour
{
    [System.Serializable]
    public class StyleDistribution
    {
        public TeachingStyle Style;
        public float Amount;
    }

    public WritingController Writing;
    public TeacherController Teacher;
    public UIController UI;
    public float Duration;
    public StyleDistribution[] Distribution;


    private IEnumerator Start()
    {
        var flow = new LectureFlow();
        flow.Generate(Duration, Distribution.ToDictionary(item => (IStyle)item.Style, item => item.Amount));

        print($"Generated lecture flow: {flow}");

        UI.Reset();
        Teacher.PlayIntro();

        // Pause before starting.
        yield return new WaitForSeconds(3f);
        Teacher.Play(flow);
        Writing.Listen(Duration);

        yield return new WaitForSeconds(Duration);

        Teacher.Stop();
        var playerFlow = Writing.Stop();

        print($"Got writing lecture flow: {playerFlow}");

        var score = (1 - flow.Compare(playerFlow)) * 100;
        UI.ShowScore(score);
    }
}
