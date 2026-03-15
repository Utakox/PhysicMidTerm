using UnityEngine;

public class WindForce : MonoBehaviour
{
    public FanController fan;
    public Transform windDirection;

    void OnTriggerStay(Collider other)
    {
        if (!fan.fanOn) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        float mass = rb.mass;

        // F = ma
        float force = mass * fan.windAcceleration;

        if (fan.windAcceleration < mass)
            return;

        Vector3 windForce = windDirection.forward * force;

        rb.AddForce(windForce);
    }
}