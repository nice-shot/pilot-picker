using UnityEngine;

public abstract class WritingDisplay : MonoBehaviour
{
    public abstract void ChangeColor(Color color);

    public abstract void StartWriting(float duration, Color startingColor);

    public abstract void StopWriting();

    public abstract void Reset();
}
