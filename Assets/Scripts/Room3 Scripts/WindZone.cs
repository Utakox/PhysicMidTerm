using UnityEngine;

public class WindZone : MonoBehaviour
{
    public float windForce = 0f;
    public float wideness = 5f;
    public float maxDistance = 50f;

    void FixedUpdate()
    {
        // วาดทิศลมใน Scene
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.blue);

        // ยิง SphereCast
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            wideness,
            transform.forward,
            maxDistance
        );

        foreach (RaycastHit hit in hits)
        {
            if (hit.rigidbody != null)
            {
                // ใช้ทิศของ object จริง
                Vector3 direction = transform.forward;

                // ใส่แรงลม
                hit.rigidbody.AddForce(direction * windForce, ForceMode.Acceleration);
            }
        }
    }
}