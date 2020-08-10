using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TextWriting : MonoBehaviour, IWriting
{
    public Text UIText;
    public Pen DefaultPen;

    private string _originalText;
    private int _originalTextIndex = 0;
    private IPen _currentPen;

    private SortedDictionary<int, IPen> _penDistribution = new SortedDictionary<int, IPen>();

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

    private void ChangeColor(Color color)
    {
        if (_originalTextIndex < _originalText.Length)
        {
            UIText.text += $"<color=\"#{ColorUtility.ToHtmlStringRGB(color)}\"></color>";
        }
    }

    private void Awake()
    {
        _originalText = UIText.text;
        UIText.text = "";
        ChangePen(DefaultPen);
        _penDistribution[0] = _currentPen;
    }

    public void SetAmountWritten(float amount)
    {
        var totalLettersToShow = (int)(_originalText.Length * amount);
        if (totalLettersToShow < _originalTextIndex)
        {
            throw new System.NotImplementedException("Can't write less then what was already written.");
        }
        WriteLetters(totalLettersToShow - _originalTextIndex);
        _originalTextIndex = totalLettersToShow;
    }

    public void ChangePen(IPen pen)
    {
        if (_currentPen != pen)
        {
            _penDistribution.Add(_originalTextIndex, _currentPen);
        }
        ChangeColor(pen.Color);
        _currentPen = pen;
    }

    public SortedDictionary<int, IPen> GetDistribution()
    {
        var distribution = new SortedDictionary<int, IPen>(_penDistribution);
        distribution[0] = DefaultPen;
        distribution.Add(_originalTextIndex, _currentPen);
        return distribution;
    }
}
