using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TrapButton : MonoBehaviour
{
    public AudioSource announcerSource;

    public List<AudioClip> announcementVoices = new List<AudioClip>();

    public List<GameObject> sirenLights = new List<GameObject>();

    public AudioClip alarmSound;
    public AudioClip backgroundMusic;

    public AudioSource alarmSource;
    public AudioSource musicSource;

    public RawImage blackScreen;

    public float alarmLoopDelay = 5f;
    public float repairTime = 180f;

    private bool activated = false;

    void OnMouseDown()
    {
        if (!activated)
        {
            StartCoroutine(StartTrap());
        }
    }

    IEnumerator StartTrap()
    {
        activated = true;

        // เล่นเสียงประกาศ
        foreach (AudioClip voice in announcementVoices)
        {
            announcerSource.clip = voice;
            announcerSource.Play();
            yield return new WaitForSeconds(voice.length);
        }

        // เปิดไฟ siren
        foreach (GameObject light in sirenLights)
        {
            light.SetActive(true);
        }

        // เริ่มเพลง background
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();

        // เริ่ม alarm loop
        StartCoroutine(AlarmLoop());

        // เริ่มจอค่อย ๆ ดำ
        StartCoroutine(FadeToBlack());
    }

    IEnumerator AlarmLoop()
    {
        while (activated)
        {
            alarmSource.clip = alarmSound;
            alarmSource.Play();

            yield return new WaitForSeconds(alarmSound.length);
            yield return new WaitForSeconds(alarmLoopDelay);
        }
    }

    IEnumerator FadeToBlack()
    {
        float time = 0f;
        float duration = repairTime;

        Color c = blackScreen.color;

        while (time < duration)
        {
            time += Time.deltaTime;

            float alpha = time / duration;

            blackScreen.color = new Color(c.r, c.g, c.b, alpha);

            yield return null;
        }

        blackScreen.color = new Color(c.r, c.g, c.b, 1f);
    }

    public void StopTrap()
    {
        activated = false;

        alarmSource.Stop();
        musicSource.Stop();

        foreach (GameObject light in sirenLights)
        {
            light.SetActive(false);
        }
    }
}