using UnityEngine;

public class WindPush : MonoBehaviour
{
    public float force = 500f;
    public Vector3 windDirection = Vector3.forward;

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(windDirection.normalized * force * Time.deltaTime, ForceMode.Acceleration);
        }
    }
}