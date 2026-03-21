using UnityEngine;
using TMPro;

public class FanController : MonoBehaviour
{
    public float windAcceleration = 5f;  // ค่าเริ่มต้น
    public float windStep = 1f;          // เพิ่ม/ลดทีละกี่
    public float maxWind = 20f;          
    public bool fanOn = false;

    public TextMeshPro windText;         // แสดงค่า
    public AudioSource fanSound;

    void Start()
    {
        windAcceleration = Mathf.Clamp(windAcceleration, 0, maxWind);
        UpdateText();
    }

    // เปิด/ปิดพัดลม
    public void ToggleFan()
    {
        fanOn = !fanOn;

        if (fanSound == null) return;

        if (fanOn)
        {
            fanSound.loop = true;
            fanSound.Play();
        }
        else
        {
            fanSound.Stop();
        }

        UpdateText();
    }

    // เพิ่มแรงลม
    public void IncreaseWind()
    {
        if (!fanOn) return;

        windAcceleration += windStep;
        windAcceleration = Mathf.Clamp(windAcceleration, 0, maxWind);
        UpdateText();
    }

    // ลดแรงลม
    public void DecreaseWind()
    {
        if (!fanOn) return;

        windAcceleration -= windStep;
        windAcceleration = Mathf.Clamp(windAcceleration, 0, maxWind);
        UpdateText();
    }

    void UpdateText()
    {
        if (windText == null) return;

        windText.text = fanOn ? $"Wind: {windAcceleration:F1}" : "Wind: OFF";
    }
}