using UnityEngine;

public class WritingController : MonoBehaviour
{
    public void Listen(float duration)
    {
        print($"Started listening.");
    }

    public LectureFlow Stop()
    {
        print($"Stopping.");
        return new LectureFlow();
    }
}