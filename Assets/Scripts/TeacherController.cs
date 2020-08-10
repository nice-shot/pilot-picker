using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeacherController : MonoBehaviour
{
    public Text DisplayText;


    private void Awake() => DisplayText.text = "INTRO";

    private IEnumerator PlayRouting(LectureFlow flow)
    {
        foreach (var segment in flow)
        {
            var teachingStyle = segment.Style as TeachingStyle;
            if (teachingStyle)
            {
                DisplayText.text = teachingStyle.DisplayText;
            }
            else
            {
                DisplayText.text = "?";
            }
            yield return new WaitForSeconds(segment.Duration);
        }
        DisplayText.text = "DONE";
    }

    public void Play(LectureFlow flow)
    {
        print("Started playing lecture flow.");
        StartCoroutine(PlayRouting(flow));
    }

    public void Stop()
    {
        print("Stopping.");
        StopAllCoroutines();
        DisplayText.text = "DONE";
    }
}