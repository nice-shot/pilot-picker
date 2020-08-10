using System;
using UnityEngine;

public class PilotPen : MonoBehaviour, IStyle
{
    public event Action OnPenSeleted;
    public Color Color;

    public void Select() => OnPenSeleted?.Invoke();
}