using UnityEngine;
using UnityEngine.UI;

public class PenSwitcher : MonoBehaviour
{
    [System.Serializable]
    public class PenButton
    {
        public Button Button;
        public Pen Pen;
    }

    public PenButton[] PenButtons;
    public TextWriting Writing;

    private void Awake()
    {
        foreach (var penButton in PenButtons)
        {
            penButton.Button.onClick.AddListener(() => Writing.ChangePen(penButton.Pen));
        }
    }
}