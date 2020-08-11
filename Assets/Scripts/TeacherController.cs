using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeacherController : MonoBehaviour
{
    public Text DisplayText;
    public AudioSource Voice;

    public AudioClip IntroVoiceClip;
    public AudioClip OutroVoiceClip;

    private IEnumerator PlayRouting(LectureFlow flow)
    {
        foreach (var segment in flow)
        {
            var teachingStyle = segment.Style as TeachingStyle;
            if (teachingStyle)
            {
                DisplayText.text = teachingStyle.DisplayText;
                Voice.clip = teachingStyle.VoiceClip;
                Voice.loop = true;
                Voice.Play();
            }
            else
            {
                DisplayText.text = "?";
                Voice.Stop();
            }
            yield return new WaitForSeconds(segment.Duration);
        }
        DisplayText.text = "DONE";
    }

    public void PlayIntro()
    {
        print("Playing teacher intro.");
        DisplayText.text = "INTRO";
        Voice.clip = IntroVoiceClip;
        Voice.loop = false;
        Voice.Play();
    }

    public void Play(LectureFlow flow)
    {
        print("Started playing lecture flow.");
        StartCoroutine(PlayRouting(flow));
    }

    public void Stop()
    {
        print("Stopping.");
        StopAllCoroutines();
        DisplayText.text = "DONE";
        Voice.clip = IntroVoiceClip;
        Voice.loop = false;
        Voice.Play();
    }
}
