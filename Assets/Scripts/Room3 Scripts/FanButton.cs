using TMPro;
using UnityEngine;

public class FanButton : MonoBehaviour
{
    public WindZone windZone;

    public TextMeshPro windText;

    public float step = 1f;
    public float minForce = 0f;
    public float maxForce = 20f;

    void Start()
    {
        UpdateText();

        if (windZone == null)
            Debug.LogError("[FanButton] WindZone not assigned!");
    }

    void Update()
    {
        // คลิกซ้าย
        if (Input.GetMouseButtonDown(0))
        {
            TryClick(true);
        }

        // คลิกขวา
        if (Input.GetMouseButtonDown(1))
        {
            TryClick(false);
        }
    }

    void TryClick(bool increase)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // เช็คว่าโดน object นี้มั้ย
            if (hit.transform == transform)
            {
                if (increase)
                    IncreaseWind();
                else
                    DecreaseWind();
            }
        }
    }

    public void IncreaseWind()
    {
        if (windZone == null) return;

        windZone.windForce += step;
        windZone.windForce = Mathf.Clamp(windZone.windForce, minForce, maxForce);

        UpdateText();
    }

    public void DecreaseWind()
    {
        if (windZone == null) return;

        windZone.windForce -= step;
        windZone.windForce = Mathf.Clamp(windZone.windForce, minForce, maxForce);

        UpdateText();
    }

    void UpdateText()
    {
        if (windText != null)
        {
            windText.text = "Wind: " + windZone.windForce.ToString("F1");
        }
    }
}