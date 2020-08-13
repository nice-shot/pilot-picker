using UnityEngine;

public class Pen2D : Pen
{
    private void OnMouseDown() => Select();

    override public void Select()
    {
        gameObject.SetActive(false);
        base.Select();
    }

    override public void Deselect()
    {
        gameObject.SetActive(true);
    }
}
