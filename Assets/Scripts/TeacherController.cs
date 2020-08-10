using UnityEngine;

public class TeacherController : MonoBehaviour
{
    public void Play(LectureFlow flow)
    {
        print($"Playing flow: {flow}");
    }

    public void Stop()
    {
        print($"Stopping.");
    }
}