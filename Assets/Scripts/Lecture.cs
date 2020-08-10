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

        Teacher.Play(flow);
        Writing.Listen(Duration);

        yield return new WaitForSeconds(Duration);

        Teacher.Stop();
        var playerFlow = Writing.Stop();

        print($"Got writing lecture flow: {playerFlow}");

        UI.ShowScore(flow.Compare(playerFlow));






        // var startTime = Time.time;
        // var factor = 0f;

        // while (factor < 1f)
        // {
        //     Writing.SetAmountWritten(factor);
        //     yield return null;
        //     factor = (Time.time - startTime) / Duration;
        // }

        // Writing.SetAmountWritten(1f);

        // foreach (var item in Writing.GetDistribution())
        // {
        //     if (item.Value is ScriptableObject)
        //     {
        //         print($"{(item.Value as ScriptableObject).name} - {item.Key}");
        //     }
        // }
    }
}