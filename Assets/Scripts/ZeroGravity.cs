using UnityEngine;

public class ZeroGravityZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = true;
        }
    }
}