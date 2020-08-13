using System;
using UnityEngine;

public abstract class Pen : MonoBehaviour, IStyle
{
    public event Action OnPenSeleted;
    public Color Color;

    public bool Selectable { get; set; } = true;

    virtual public void Select()
    {
        if (Selectable) OnPenSeleted?.Invoke();
    }

    virtual public void Deselect() { }

    virtual public void Reset() { }
}
