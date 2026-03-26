using UnityEngine;
using System.Collections;

public class LightBlink : MonoBehaviour
{
    public Light targetLight;
    public float blinkDuration = 0.1f; // ระยะเวลาที่ไฟดับ/ติด
    public float delayBetweenBlink = 1f; // หน่วงก่อนกระพริบรอบถัดไป

    void Start()
    {
        StartCoroutine(BlinkLoop());
    }

    IEnumerator BlinkLoop()
    {
        while (true)
        {
            // ปิดไฟ
            targetLight.enabled = false;
            yield return new WaitForSeconds(blinkDuration);

            // เปิดไฟ
            targetLight.enabled = true;
            yield return new WaitForSeconds(delayBetweenBlink);
        }
    }
}