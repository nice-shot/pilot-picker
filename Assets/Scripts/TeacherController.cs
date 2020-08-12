using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeacherController : MonoBehaviour
{
    private readonly int IndexAnimParam = Animator.StringToHash("Index");

    public Text DisplayText;
    public AudioSource Voice;
    public Animator Animator;

    public AudioClip IntroVoiceClip;
    public AudioClip OutroVoiceClip;

    private void ChangeDisplayText(string text)
    {
        if (DisplayText) DisplayText.text = text;
    }

    private void ChangeAnimation(int index)
    {
        if (Animator) Animator.SetInteger(IndexAnimParam, index);
    }

    private IEnumerator PlayRouting(LectureFlow flow)
    {
        foreach (var segment in flow)
        {
            var teachingStyle = segment.Style as TeachingStyle;
            if (teachingStyle)
            {
                ChangeDisplayText(teachingStyle.DisplayText);
                ChangeAnimation(teachingStyle.AnimationIndex);
                Voice.clip = teachingStyle.VoiceClip;
                Voice.loop = true;
                Voice.Play();
            }
            else
            {
                ChangeDisplayText("?");
                Voice.Stop();
            }
            yield return new WaitForSeconds(segment.Duration);
        }
        ChangeDisplayText("DONE");
    }

    public void PlayIntro()
    {
        print("Playing teacher intro.");
        ChangeDisplayText("INTRO");
        ChangeAnimation(0);
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
        ChangeDisplayText("DONE");
        ChangeAnimation(-1);
        Voice.clip = IntroVoiceClip;
        Voice.loop = false;
        Voice.Play();
    }

    public bool IsOutroFinished() => !Voice.isPlaying;
}
