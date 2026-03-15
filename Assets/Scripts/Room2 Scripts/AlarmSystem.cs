using UnityEngine;
using System.Collections;

public class AlarmSystem : MonoBehaviour
{
    public AudioSource buttonSpeaker;   // ลำโพงปุ่ม

    public AudioSource[] alarmSpeakers; // ลำโพง alarm หลายตัว

    public AudioClip buttonSound;
    public AudioClip alarmSound;

    public GameObject[] alarmLight;

    public Rigidbody[] zeroGravityObjects;

    public GameObject[] objectsToDisappear;

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

        // เล่นเสียงปุ่ม
        buttonSpeaker.PlayOneShot(buttonSound);

        yield return new WaitForSeconds(0.5f);

        // เล่นเสียง alarm ทุกลำโพง
        foreach (AudioSource speaker in alarmSpeakers)
        {
            speaker.clip = alarmSound;
            speaker.loop = true;
            speaker.Play();
        }

        // เปิดไฟ
        foreach (GameObject light in alarmLight)
        {
            light.SetActive(true);
        }

        StartCoroutine(EnableGravity());

        yield return new WaitForSeconds(alarmDuration);

        // หยุดเสียงทุกลำโพง
        foreach (AudioSource speaker in alarmSpeakers)
        {
            speaker.Stop();
        }

        // ปิดไฟ
        foreach (GameObject light in alarmLight)
        {
            light.SetActive(false);
        }

        // object หาย
        foreach (GameObject obj in objectsToDisappear)
        {
            obj.SetActive(false);
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