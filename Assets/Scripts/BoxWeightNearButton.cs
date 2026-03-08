using UnityEngine;

public class BoxWeightNearButton : MonoBehaviour
{
    public Transform button;          // ปุ่ม
    public float maxDistance;    // ระยะที่เริ่มมีผล
    public float normalMass;     // น้ำหนักปกติ
    public float heavyMass;     // น้ำหนักมากสุด

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = normalMass;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, button.position);

        if (distance < maxDistance)
        {
            float t = 1 - (distance / maxDistance);
            rb.mass = Mathf.Lerp(normalMass, heavyMass, t);
        }
        else
        {
            rb.mass = normalMass;
        }
    }
}