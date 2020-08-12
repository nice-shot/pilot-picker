using System.Collections;
using UnityEngine;

public class LineMaskWritingDisplay : MonoBehaviour
{
    // Assumes the lines are ordered in the hierarchy from top to bottom.
    private LineRenderer[] lineRenderers;

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
        var numOfLines = lineRenderers.Length;
        var multipliedValue = value * numOfLines;

        var full = new GradientAlphaKey[] {
            new GradientAlphaKey(1, 0),
            new GradientAlphaKey(1, 1)
        };

        var empty = new GradientAlphaKey[] {
            new GradientAlphaKey(0, 0),
            new GradientAlphaKey(1, 0)
        };

        for (int i = 0; i < numOfLines; i++)
        {
            var line = lineRenderers[i];
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
                alphaGradient = new GradientAlphaKey[] {
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

    private void Awake()
    {
        lineRenderers = GetComponentsInChildren<LineRenderer>();
        // Adds more points to allow more detailed effects for the gradient.
        foreach (var line in lineRenderers) AddIntermidiatePoints(line, 20);
        StartCoroutine(WritingRoutine(20));
    }
}
