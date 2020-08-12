using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaskWritingDisplay : WritingDisplay
{
    // Assumes the lines are ordered in the hierarchy from top to bottom.
    private LineRenderer[] _lineRenderers;
    private float _currentDisplayedAmount;

    /// <summary>
    /// Adds additional points to the line. Assumes there are only two points at the start.
    /// </summary>
    private void AddIntermidiatePoints(LineRenderer line, int amount)
    {
        var originalPositions = new Vector3[2];
        line.GetPositions(originalPositions);

        var newPositions = new Vector3[amount + 2];
        newPositions[0] = originalPositions[0];
        newPositions[newPositions.Length - 1] = originalPositions[1];

        for (int i = 0; i < amount; i++)
        {
            var rel = (i + 1) * (1f / amount);
            var newPosition = Vector3.Lerp(originalPositions[0], originalPositions[1], rel);
            newPositions[i + 1] = newPosition;
        }

        line.positionCount = newPositions.Length;
        line.SetPositions(newPositions);
    }

    /// <summary>
    /// From 0-1 - How much to show from the text.
    /// </summary>
    private void SetDisplayedAmount(float value)
    {
        _currentDisplayedAmount = value;
        var numOfLines = _lineRenderers.Length;
        var multipliedValue = value * numOfLines;

        var full = new GradientAlphaKey[]
        {
            new GradientAlphaKey(1, 0),
            new GradientAlphaKey(1, 1)
        };

        var empty = new GradientAlphaKey[]
        {
            new GradientAlphaKey(0, 0),
            new GradientAlphaKey(1, 0)
        };

        for (int i = 0; i < numOfLines; i++)
        {
            var line = _lineRenderers[i];
            GradientAlphaKey[] alphaGradient;

            if (i < Mathf.Floor(multipliedValue))
            {
                alphaGradient = full;
            }
            else if (i > Mathf.Floor(multipliedValue))
            {
                alphaGradient = empty;
            }
            else {
                alphaGradient = new GradientAlphaKey[]
                {
                    new GradientAlphaKey(1, 0),
                    new GradientAlphaKey(1, multipliedValue % 1),
                    new GradientAlphaKey(0, (multipliedValue % 1) + Mathf.Epsilon),
                    new GradientAlphaKey(0, 1)
                };
            }

            var gradient = line.colorGradient;
            gradient.alphaKeys = alphaGradient;
            line.colorGradient = gradient;
        }
    }

    private IEnumerator WritingRoutine(float duration)
    {
        var startTime = Time.time;
        var factor = 0f;

        while (factor < 1f)
        {
            SetDisplayedAmount(factor);
            yield return null;
            factor = (Time.time - startTime) / duration;
        }

        SetDisplayedAmount(1f);
    }

    private void ResetColor(Color color)
    {
        var colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(color, 0),
            new GradientColorKey(color, 1)
        };

        foreach (var line in _lineRenderers)
        {
            var gradient = line.colorGradient;
            gradient.colorKeys = colorKeys;
            line.colorGradient = gradient;
        }
    }

    private void Awake()
    {
        _lineRenderers = GetComponentsInChildren<LineRenderer>();
        // Adds more points to allow more detailed effects for the gradient.
        foreach (var line in _lineRenderers) AddIntermidiatePoints(line, 20);
    }

    public override void ChangeColor(Color color)
    {
        var multipliedAmount = _currentDisplayedAmount * _lineRenderers.Length;
        for (int i = (int)multipliedAmount; i < _lineRenderers.Length; i++)
        {
            var line = _lineRenderers[i];
            var gradient = line.colorGradient;
            if (i > Mathf.Floor(multipliedAmount))
            {
                gradient.colorKeys = new GradientColorKey[]
                {
                    new GradientColorKey(color, 0)
                };
            }
            else {
                var previousGrad = new List<GradientColorKey>(gradient.colorKeys);
                var lastKey = previousGrad[previousGrad.Count - 1];

                previousGrad[previousGrad.Count - 1] = new GradientColorKey(color, 1);
                previousGrad.Add(new GradientColorKey(lastKey.color, (multipliedAmount % 1) - Mathf.Epsilon));
                previousGrad.Add(new GradientColorKey(color, multipliedAmount % 1));
                gradient.colorKeys = previousGrad.ToArray();
            }

            line.colorGradient = gradient;
        }
    }

    public override void StartWriting(float duration, Color startingColor)
    {
        ResetColor(startingColor);
        SetDisplayedAmount(0);
        ChangeColor(startingColor);
        StartCoroutine(WritingRoutine(duration));
    }

    public override void StopWriting()
    {
        StopAllCoroutines();
        SetDisplayedAmount(1f);
    }

    public override void Reset()
    {
        SetDisplayedAmount(0);
    }
}
