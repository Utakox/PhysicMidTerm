using UnityEngine;

public class MagneticMassPlate : MonoBehaviour
{
    public float requiredMass = 5f;
    public Transform snapPoint;

    public float pullSpeed = 3f;
    public float hoverHeight = 0.5f;

    public Door door;

    bool activated = false;

    void OnTriggerStay(Collider other)
    {
        if (activated) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null) return;

        if (rb.mass >= requiredMass)
        {
            rb.useGravity = false;

            Vector3 target = snapPoint.position + Vector3.up * hoverHeight;

            rb.MovePosition(Vector3.Lerp(rb.position, target, pullSpeed * Time.deltaTime));

            if (Vector3.Distance(rb.position, target) < 0.1f)
            {
                rb.velocity = Vector3.zero;
                rb.constraints = RigidbodyConstraints.FreezePosition;

                activated = true;
               // door.ActivatePortal(); 
            }
        }
    }
}