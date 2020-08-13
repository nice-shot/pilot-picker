using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject ResultsContainer;
    public GameObject IntroMessageContainer;
    public Text ScoreDisplayText;

    private void Awake()
    {
        Reset();
        IntroMessageContainer.SetActive(true);
    }

    public void Reset()
    {
        ResultsContainer.SetActive(false);
        IntroMessageContainer.SetActive(false);
    }

    public void ShowScore(float score)
    {
        print($"Final score: {score}!");
        ResultsContainer.SetActive(true);
        ScoreDisplayText.text = score.ToString("0");
    }
}
