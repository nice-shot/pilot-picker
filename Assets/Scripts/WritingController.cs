using UnityEngine;

public class WritingController : MonoBehaviour
{
    public PilotPen[] Pens;
    public PilotPen DefaultPen;

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
    }

    public LectureFlow Stop()
    {
        print($"Stopping.");
        _flow.Add(new LectureSegment(Time.time - _previousChange, _previousPen));
        _previousChange = -1f;
        return _flow;
    }
}