using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TextWritingDisplay : MonoBehaviour
{
    public Text UIText;

    private string _originalText;
    private int _originalTextIndex = 0;

    private void WriteLetters(int amount)
    {
        if (amount == 0) return;

        var colorClosingTag = "</color>";
        // Remove color closing tag.
        var currentText = UIText.text.Substring(0, UIText.text.Length - colorClosingTag.Length);

        currentText += _originalText.Substring(_originalTextIndex, amount);
        currentText += colorClosingTag;
        UIText.text = currentText;

        _originalTextIndex += amount;
    }

    private void SetAmountWritten(float amount)
    {
        var totalLettersToShow = (int)(_originalText.Length * amount);
        if (totalLettersToShow < _originalTextIndex)
        {
            throw new System.NotImplementedException("Can't write less then what was already written.");
        }
        WriteLetters(totalLettersToShow - _originalTextIndex);
        _originalTextIndex = totalLettersToShow;
    }

    private IEnumerator WritingRoutine(float duration)
    {
        var startTime = Time.time;
        var factor = 0f;

        while (factor < 1f)
        {
            SetAmountWritten(factor);
            yield return null;
            factor = (Time.time - startTime) / duration;
        }

        SetAmountWritten(1f);
    }

    private void Awake()
    {
        _originalText = UIText.text;
    }

    public void ChangeColor(Color color)
    {
        // Only change if we're not done writing.
        if (_originalTextIndex < _originalText.Length)
        {
            UIText.text += $"<color=\"#{ColorUtility.ToHtmlStringRGB(color)}\"></color>";
        }
    }

    public void StartWriting(float duration, Color startingColor)
    {
        UIText.text = "";
        _originalTextIndex = 0;
        ChangeColor(startingColor);
        StartCoroutine(WritingRoutine(duration));
    }

    public void StopWriting() => StopAllCoroutines();
}
