using UnityEngine;
using System.Collections;

public class AlarmSystem : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip buttonSound;
    public AudioClip alarmSound;

    public GameObject[] alarmLight;

    public Rigidbody[] zeroGravityObjects;

    public float gravityDelay = 3f;
    public float alarmDuration = 10f;

    private bool activated = false;

    void OnMouseDown()
    {
        if (!activated)
        {
            StartCoroutine(ActivateAlarm());
        }
    }

    IEnumerator ActivateAlarm()
    {
        activated = true;

        audioSource.PlayOneShot(buttonSound);

        yield return new WaitForSeconds(0.5f);

        audioSource.clip = alarmSound;
        audioSource.loop = true;
        audioSource.Play();

        // เปิดไฟทุกดวง
        foreach (GameObject light in alarmLight)
        {
            light.SetActive(true);
        }

        StartCoroutine(EnableGravity());

        yield return new WaitForSeconds(alarmDuration);

        audioSource.Stop();

        // ปิดไฟทุกดวง
        foreach (GameObject light in alarmLight)
        {
            light.SetActive(false);
        }
    }

    IEnumerator EnableGravity()
    {
        yield return new WaitForSeconds(gravityDelay);

        foreach (Rigidbody rb in zeroGravityObjects)
        {
            rb.useGravity = true;
        }
    }
}