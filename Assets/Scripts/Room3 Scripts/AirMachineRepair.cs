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

    [Header("Particle (ก่อนซ่อม)")]
    public ParticleSystem activeParticle; // 🔥 เพิ่ม

    [Header("Complete Voice")]
    public AudioSource voiceSource;
    public AudioClip completeVoice;

    [Header("Fixed Machine Sound (หลังซ่อม)")]
    public AudioSource fixedMachineSound;

    [Header("Fade Settings")]
    public float fadeDuration = 3f;
    public float fadeStartDelay = 1f;

    private bool isCompleted = false;

    void Start()
    {
        // เสียงเครื่องพัง
        if (activeMachineSound != null)
        {
            activeMachineSound.loop = true;
            activeMachineSound.Play();
        }

        // particle ทำงาน
        if (activeParticle != null)
        {
            activeParticle.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
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

        // 🔥 หยุด Trap
        if (trapSystem != null)
        {
            trapSystem.StopTrap();
        }

        // 🔇 fade out เสียงเครื่องพัง
        if (activeMachineSound != null)
        {
            StartCoroutine(FadeOutSound(activeMachineSound, 1.5f));
        }

        // 💨 ปิด particle
        if (activeParticle != null)
        {
            activeParticle.Stop();
        }

        // 🔥 เริ่ม sequence เสียง + เครื่องใหม่
        StartCoroutine(CompleteSequence());
    }

    IEnumerator CompleteSequence()
    {
        // 🗣️ เล่นเสียง complete ก่อน
        if (voiceSource != null && completeVoice != null)
        {
            voiceSource.clip = completeVoice;
            voiceSource.Play();

            yield return new WaitForSeconds(completeVoice.length);
        }

        // 🎧 แล้วค่อยเปิดเสียงเครื่องใหม่แบบ fade in
        if (fixedMachineSound != null)
        {
            fixedMachineSound.loop = true;
            StartCoroutine(FadeInSound(fixedMachineSound, fadeDuration, fadeStartDelay));
        }
    }

    IEnumerator FadeInSound(AudioSource audio, float duration, float delay)
    {
        yield return new WaitForSeconds(delay);

        audio.volume = 0f;
        audio.pitch = 0.5f;
        audio.Play();

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            audio.volume = Mathf.Lerp(0f, 1f, t * t);
            audio.pitch = Mathf.Lerp(0.5f, 1f, t);

            yield return null;
        }

        audio.volume = 1f;
        audio.pitch = 1f;
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