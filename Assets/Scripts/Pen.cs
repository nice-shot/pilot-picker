using UnityEngine;

[CreateAssetMenu]
public class Pen : ScriptableObject, IPen
{
    [SerializeField] private Color _color = Color.white;
    public Color Color => _color;
}