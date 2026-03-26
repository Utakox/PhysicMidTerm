using UnityEngine;

public class ActivateByMass : MonoBehaviour
{
    public Rigidbody rb;
    public float targetMass = 20f;

    public GameObject targetObject;
    public GameObject targetObject2; // เพิ่มตัวที่สอง

    bool triggered = false;

    void Update()
    {
        if (!triggered && rb.mass >= targetMass)
        {
            triggered = true;

            targetObject.SetActive(true);
            targetObject2.SetActive(true); // เปิดอีก object
        }
    }
}