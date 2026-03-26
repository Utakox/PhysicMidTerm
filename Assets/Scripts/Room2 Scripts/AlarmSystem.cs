using UnityEngine;
using System.Collections;

public class AlarmSystem : MonoBehaviour
{
    public AudioSource buttonSpeaker;   // ลำโพงปุ่ม
    public AudioSource[] alarmSpeakers; // ลำโพง alarm หลายตัว
    public AudioSource endingSpeaker;   // ลำโพงเสียงสุดท้าย

    public AudioClip buttonSound;
    public AudioClip alarmSound;
    public AudioClip endingSound;

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

        // 🔘 เล่นเสียงปุ่ม
        if (buttonSpeaker && buttonSound)
            buttonSpeaker.PlayOneShot(buttonSound);

        yield return new WaitForSeconds(0.5f);

        // 🚨 เล่นเสียง alarm ทุกลำโพง
        foreach (AudioSource speaker in alarmSpeakers)
        {
            if (speaker && alarmSound)
            {
                speaker.clip = alarmSound;
                speaker.loop = true;
                speaker.Play();
            }
        }

        // 💡 เปิดไฟ
        foreach (GameObject light in alarmLight)
        {
            if (light) light.SetActive(true);
        }

        // 🧲 เปิด gravity หลัง delay
        StartCoroutine(EnableGravity());

        yield return new WaitForSeconds(alarmDuration);

        // 🛑 หยุดเสียง alarm
        foreach (AudioSource speaker in alarmSpeakers)
        {
            if (speaker) speaker.Stop();
        }

        // 💡 ปิดไฟ
        foreach (GameObject light in alarmLight)
        {
            if (light) light.SetActive(false);
        }

        // ❌ ทำให้ object หาย
        foreach (GameObject obj in objectsToDisappear)
        {
            if (obj) obj.SetActive(false);
        }

        // ⏱️ หน่วงนิดให้จังหวะดี
        yield return new WaitForSeconds(0.5f);

        // 🔊 เล่นเสียงสุดท้าย
        if (endingSpeaker && endingSound)
            endingSpeaker.PlayOneShot(endingSound);
    }

    IEnumerator EnableGravity()
    {
        yield return new WaitForSeconds(gravityDelay);

        foreach (Rigidbody rb in zeroGravityObjects)
        {
            if (rb) rb.useGravity = true;
        }
    }
}