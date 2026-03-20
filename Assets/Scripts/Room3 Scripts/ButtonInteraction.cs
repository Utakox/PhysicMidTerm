using System.Collections;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip pressSound;
    public AudioClip confirm1;
    public AudioClip confirm2;
    public AudioClip confirm3;

    public TrapSystem trapSystem;

    private int state = 0;
    private bool isPlaying = false;

    void OnMouseDown()
    {
        if (isPlaying) return;

        StartCoroutine(HandlePress());
    }

    IEnumerator HandlePress()
    {
        isPlaying = true;

        // 🔊 เสียงกด
        yield return PlayClip(pressSound);

        if (state == 0)
        {
            yield return PlayClip(confirm1);
            state = 1;
        }
        else if (state == 1)
        {
            yield return PlayClip(confirm2);
            state = 2;
        }
        else if (state == 2)
        {
            yield return PlayClip(confirm3);
            state = 3;

            if (trapSystem != null)
                trapSystem.StartTrap();
        }

        isPlaying = false;
    }

    IEnumerator PlayClip(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Clip is NULL!");
            yield break;
        }

        audioSource.PlayOneShot(clip);
        yield return new WaitForSecondsRealtime(clip.length + 0.1f);
    }
}