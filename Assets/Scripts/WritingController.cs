using UnityEngine;

public class WritingController : MonoBehaviour
{
    public Pen[] Pens;
    public Pen DefaultPen;
    public WritingDisplay WritingDisplay;

    private LectureFlow _flow = new LectureFlow();
    private float _previousChange = -1f;
    private Pen _previousPen;

    private void ChangePen(Pen pen)
    {
        if (_previousChange < 0) return;

        print($"Changing pen from: {_previousPen} to: {pen}");
        _flow.Add(new LectureSegment(Time.time - _previousChange, _previousPen));
        _previousPen.Deselect();
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
        Reset();
    }

    public void Listen(float duration)
    {
        print($"Started listening.");
        foreach (var pen in Pens) pen.Selectable = true;
        _previousPen = DefaultPen;
        _previousPen.Select();
        _previousChange = Time.time;
        _flow.Clear();
        if (WritingDisplay) WritingDisplay.StartWriting(duration, DefaultPen.Color);
    }

    public LectureFlow Stop()
    {
        print($"Stopping.");
        // Add the last pen's duration.
        _flow.Add(new LectureSegment(Time.time - _previousChange, _previousPen));
        _previousChange = -1f;
        _previousPen.Deselect();
        if (WritingDisplay) WritingDisplay.StopWriting();
        foreach (var pen in Pens) pen.Selectable = false;
        return _flow;
    }

    public void Reset()
    {
        if (WritingDisplay) WritingDisplay.Reset();
        foreach (var pen in Pens)
        {
            pen.Reset();
            pen.Selectable = false;
        }
    }
}
