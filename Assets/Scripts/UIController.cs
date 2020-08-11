using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject ResultsContainer;
    public Text ScoreDisplayText;

    public void Reset()
    {
        ResultsContainer.SetActive(false);
    }

    public void ShowScore(float score)
    {
        print($"Final score: {score}!");
        ResultsContainer.SetActive(true);
        ScoreDisplayText.text = score.ToString("0");
    }
}
