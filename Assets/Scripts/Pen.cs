using System;
using UnityEngine;

public abstract class Pen : MonoBehaviour, IStyle
{
    public event Action OnPenSeleted;
    public Color Color;

    virtual public void Select() => OnPenSeleted?.Invoke();

    virtual public void Deselect() { }
}
