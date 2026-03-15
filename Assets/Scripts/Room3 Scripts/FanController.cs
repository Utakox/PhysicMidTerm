using UnityEngine;
using TMPro;

public class FanController : MonoBehaviour
{
    public float windAcceleration = 5f;
    public float windStep = 1f;
    public float maxWind = 20f;

    public bool fanOn = false;

    public TextMeshPro windText;
    public AudioSource fanSound;

    void Start()
    {
        UpdateText();
    }

    public void ToggleFan()
    {
        fanOn = !fanOn;

        if (fanOn)
            fanSound.Play();
        else
            fanSound.Stop();
    }

    public void IncreaseWind()
    {
        if (!fanOn) return;

        windAcceleration += windStep;
        windAcceleration = Mathf.Clamp(windAcceleration, 0, maxWind);

        UpdateText();
    }

    public void DecreaseWind()
    {
        if (!fanOn) return;

        windAcceleration -= windStep;
        windAcceleration = Mathf.Clamp(windAcceleration, 0, maxWind);

        UpdateText();
    }

    void UpdateText()
    {
        if (windText != null)
            windText.text = "Wind: " + windAcceleration.ToString("F1");
    }
}