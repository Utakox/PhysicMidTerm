using UnityEngine;
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

        // เล่นเสียงประกาศทีละอัน
        foreach (AudioClip voice in announcementVoices)
        {
            announcerSource.clip = voice;
            announcerSource.Play();
            yield return new WaitForSeconds(voice.length);
        }

        // เริ่ม Alarm
        alarmSource.clip = alarmSound;
        alarmSource.loop = true;
        alarmSource.Play();

        // เปิดไฟ siren
        foreach (GameObject light in sirenLights)
        {

            light.SetActive(true);

        }

        // เล่นเพลงพื้นหลัง
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopTrap()
    {
        alarmSource.Stop();
        musicSource.Stop();

        foreach (GameObject light in sirenLights)
        {
            light.SetActive(false);
        }
    }
}