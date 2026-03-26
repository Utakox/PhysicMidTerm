using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light targetLight;

    [Header("Flicker Settings")]
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;
    public float flickerSpeed = 0.05f;

    [Header("Emission Settings")]
    public Material targetMaterial;     // Mesh ที่จะเรืองแสง
    public Color emissionColor = Color.white;
    public float emissionMultiplier = 1f; // คูณความแรง

    private Material mat;

    void Start()
    {
        if (targetLight == null)
            targetLight = GetComponent<Light>();

        if (targetMaterial != null)
        {
            mat = targetMaterial;
            mat.EnableKeyword("_EMISSION");
        }
    }

    void Update()
    {
        if (targetLight == null) return;

        // สุ่มความสว่างไฟ
        float randomIntensity = Random.Range(minIntensity, maxIntensity);
        targetLight.intensity = Mathf.Lerp(targetLight.intensity, randomIntensity, flickerSpeed);

        // 🔥 Sync emission กับ light
        if (mat != null)
        {
            float emissionStrength = targetLight.intensity * emissionMultiplier;
            Color finalColor = emissionColor * emissionStrength;

            mat.SetColor("_EmissionColor", finalColor);
        }
    }
}