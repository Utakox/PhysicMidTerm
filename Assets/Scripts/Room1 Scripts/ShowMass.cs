using UnityEngine;
using TMPro;

public class ShowMass : MonoBehaviour
{
    public Rigidbody targetRigidbody;
    public TextMeshPro massText;

    void Update()
    {
        // เพิ่มเงื่อนไขเช็คว่าทั้ง Rigidbody และ TextMeshPro ต้องไม่ว่างเปล่า
        if (targetRigidbody != null && massText != null)
        {
            massText.text = targetRigidbody.mass.ToString("F1");
        }
    }
}