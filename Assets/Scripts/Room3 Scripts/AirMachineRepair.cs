using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AirMachineRepair : MonoBehaviour
{
    [Header("Required Items")]
    public List<GameObject> requiredItems = new List<GameObject>();

    [Header("Trap System")]
    public TrapSystem trapSystem;

    [Header("Active Machine Sound (ก่อนซ่อม)")]
    public AudioSource activeMachineSound;

    [Header("Objects To Disable (ก่อนซ่อม)")] // 🔥 เปลี่ยนเป็น List
    public List<GameObject> objectsToDisable = new List<GameObject>();

    [Header("Complete Voice")]
    public AudioSource voiceSource;
    public AudioClip completeVoice;

    [Header("Alarm Sounds (หลังซ่อม)")]
    public List<AudioSource> alarmSources = new List<AudioSource>();

    [Header("Fixed Machine Sound (หลังซ่อม)")]
    public AudioSource fixedMachineSound;

    [Header("New Music (Fade In)")] // 🔥 เพิ่ม
    public AudioSource newMusicSource;

    [Header("Lights")]
    public List<Light> sceneLights = new List<Light>();

    [Header("Activate Object After Complete")] 
    public GameObject objectToActivate;

    private bool isCompleted = false;
    private bool isActive = false;

    void Start()
    {
        isActive = false;

        if (activeMachineSound != null)
        {
            activeMachineSound.loop = true;
            activeMachineSound.Play();
        }

        // 🔥 เปิด object ทั้งหมดก่อน
        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    public void EnableRepair()
    {
        isActive = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (isCompleted) return;

        if (requiredItems.Contains(other.gameObject))
        {
            requiredItems.Remove(other.gameObject);
            Destroy(other.gameObject);

            CheckComplete();
        }
    }

    void CheckComplete()
    {
        if (requiredItems.Count == 0 && !isCompleted)
        {
            ShutdownMachines();
        }
    }

    void ShutdownMachines()
    {
        isCompleted = true;

        if (trapSystem != null)
        {
            trapSystem.StopTrap();
        }

        // 🔊 ปิดเสียงเครื่องเก่า
        if (activeMachineSound != null)
        {
            StartCoroutine(FadeOutSound(activeMachineSound, 1.5f));
        }

        // 🔥 ปิด object ทั้งหมดใน list
        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        StartCoroutine(CompleteSequence());
    }

    IEnumerator CompleteSequence()
    {
        // 🗣️ เสียงพูด
        if (voiceSource != null && completeVoice != null)
        {
            voiceSource.clip = completeVoice;
            voiceSource.Play();
            yield return new WaitForSeconds(completeVoice.length);
        }

        // 🚨 alarm
        foreach (AudioSource alarm in alarmSources)
        {
            if (alarm != null)
            {
                alarm.Play();
            }
        }

        // 💡 เปิดไฟ
        foreach (Light l in sceneLights)
        {
            if (l != null)
            {
                l.gameObject.SetActive(true);
                l.enabled = true;
                l.color = Color.white;
            }
        }

        // 🔥 เปิด object ใหม่
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        // 🎧 เสียงเครื่องใหม่
        if (fixedMachineSound != null)
        {
            fixedMachineSound.loop = true;
            fixedMachineSound.Play();
        }

        // 🎵 เพลงใหม่ fade ขึ้น 3 วิ
        if (newMusicSource != null)
        {
            StartCoroutine(FadeInMusic(newMusicSource, 3f));
        }
    }

    IEnumerator FadeInMusic(AudioSource audio, float duration)
    {
        audio.volume = 0f;
        audio.loop = true;
        audio.Play();

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            audio.volume = Mathf.Lerp(0f, 1f, t / duration);
            yield return null;
        }

        audio.volume = 0.8f;
    }

    IEnumerator FadeOutSound(AudioSource audio, float duration)
    {
        float startVolume = audio.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            audio.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        audio.Stop();
        audio.volume = startVolume;
    }
}