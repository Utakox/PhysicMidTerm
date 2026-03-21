using UnityEngine;

public class WindForce : MonoBehaviour
{
    public FanController fan;
    public Transform windDirection;
    public float forceMultiplier = 2f;
    public float dragCoefficient = 0.1f;

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null || fan == null || !fan.fanOn) return;

        float mass = rb.mass;
        if (fan.windAcceleration <= mass) return;

        // Newton: F = m * a
        Vector3 dir = windDirection.forward;
        dir.y = 0f;
        dir.Normalize();
        float effectiveAccel = fan.windAcceleration - mass;
        Vector3 force = dir * effectiveAccel * forceMultiplier;

        // Air resistance: F_drag = -k*v
        Vector3 drag = -rb.velocity * dragCoefficient;

        rb.AddForce(force + drag, ForceMode.Acceleration);
    }
}