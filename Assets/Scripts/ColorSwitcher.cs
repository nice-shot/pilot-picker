using UnityEngine;
using UnityEngine.UI;

public class ColorSwitcher : MonoBehaviour
{
    public Button[] Buttons;
    public TextWriting Writing;

    private void Start()
    {
        foreach (var button in Buttons)
        {
            button.onClick.AddListener(() => Writing.ChangeColor(button.colors.normalColor));
        }

        if (Buttons.Length > 0)
        {
            Writing.ChangeColor(Buttons[0].colors.normalColor);
        }
    }
}