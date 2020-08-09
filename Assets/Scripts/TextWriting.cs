using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TextWriting : MonoBehaviour
{
    private string _originalText;
    private int _originalTextIndex = 0;

    public Text UIText;
    [Range(0f, 1f)]
    public float Value = 0f;

    private void WriteLetters(int amount)
    {
        var colorClosingTag = "</color>";
        // Remove color closing tag.
        var currentText = UIText.text.Substring(0, UIText.text.Length - colorClosingTag.Length);

        currentText += _originalText.Substring(_originalTextIndex, amount);
        currentText += colorClosingTag;
        UIText.text = currentText;

        _originalTextIndex += amount;
    }

    private void Awake()
    {
        _originalText = UIText.text;
        UIText.text = "";
        // ChangeColor(UIText.color);
    }

    private IEnumerator Start()
    {
        foreach (var c in _originalText)
        {
            yield return new WaitForSeconds(0.01f);
            WriteLetters(1);
        }
    }

    public void ChangeColor(Color color)
    {
        UIText.text += $"<color=\"#{ColorUtility.ToHtmlStringRGB(color)}\"></color>";
    }
}
