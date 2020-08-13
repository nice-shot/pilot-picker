using UnityEngine;

public class Pen2D : Pen
{
    public AudioSource PickupAudio;
    public AudioSource DropAudio;
    public GameObject Graphics;
    public GameObject Collider;

    private void OnMouseDown() => Select();

    override public void Select()
    {
        if (!Selectable) return;

        Graphics.SetActive(false);
        Collider.SetActive(false);
        PickupAudio.Play();

        base.Select();
    }

    override public void Deselect()
    {
        Graphics.SetActive(true);
        Collider.SetActive(true);
        DropAudio.Play();
    }

    override public void Reset()
    {
        if (!Graphics.activeSelf) Deselect();
    }
}
