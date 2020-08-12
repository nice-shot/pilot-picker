using UnityEngine;

public class WritingController : MonoBehaviour
{
    public PilotPen[] Pens;
    public PilotPen DefaultPen;
    public WritingDisplay WritingDisplay;

    private LectureFlow _flow = new LectureFlow();
    private float _previousChange = -1f;
    private PilotPen _previousPen;

    private void ChangePen(PilotPen pen)
    {
        if (_previousChange < 0) return;

        print($"Changing pen from: {_previousPen} to: {pen}");
        _flow.Add(new LectureSegment(Time.time - _previousChange, pen));
        _previousChange = Time.time;
        _previousPen = pen;
        if (WritingDisplay) WritingDisplay.ChangeColor(pen.Color);
    }

    private void Awake()
    {
        foreach (var pen in Pens)
        {
            pen.OnPenSeleted += () => ChangePen(pen);
        }
    }

    public void Listen(float duration)
    {
        print($"Started listening.");
        _previousChange = Time.time;
        _previousPen = DefaultPen;
        _flow.Clear();
        if (WritingDisplay) WritingDisplay.StartWriting(duration, DefaultPen.Color);
    }

    public LectureFlow Stop()
    {
        print($"Stopping.");
        // Add the last pen's duration.
        _flow.Add(new LectureSegment(Time.time - _previousChange, _previousPen));
        _previousChange = -1f;
        if (WritingDisplay) WritingDisplay.StopWriting();
        return _flow;
    }

    public void Reset()
    {
        if (WritingDisplay) WritingDisplay.Reset();
    }
}
