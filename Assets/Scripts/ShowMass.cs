using UnityEngine;
using TMPro;

public class ShowMass : MonoBehaviour
{
    public Rigidbody targetRigidbody;
    public TextMeshProUGUI massText;

    void Update()
    {
        massText.text = "" + targetRigidbody.mass.ToString("F1");
    }
}