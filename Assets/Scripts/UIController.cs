using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject ResultsContainer;
    public GameObject IntroMessageContainer;
    public AudioSource PaperAudio;
    public AudioClip PaperFastClip;
    public AudioClip PaperSlowClip;
    public Text ScoreDisplayText;

    private void Awake()
    {
        Reset();
        IntroMessageContainer.SetActive(true);
        if (PaperAudio) PaperAudio.PlayOneShot(PaperSlowClip);
    }

    public void Reset()
    {
        ResultsContainer.SetActive(false);
        IntroMessageContainer.SetActive(false);
        if (PaperAudio) PaperAudio.PlayOneShot(PaperFastClip);
    }

    public void ShowScore(float score)
    {
        print($"Final score: {score}!");
        ResultsContainer.SetActive(true);
        ScoreDisplayText.text = score.ToString("0");
        if (PaperAudio) PaperAudio.PlayOneShot(PaperSlowClip);
    }
}
