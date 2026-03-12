using UnityEngine;

public class GrabSystem : MonoBehaviour
{
    public float grabDistance = 5f;
    public float moveSpeed = 10f;

    private Rigidbody grabbedObject;
    private bool canGrab = false;

    void Update()
    {
        // ถ้าอยู่ในโซน
        if (canGrab)
        {
            // กดคลิกซ้าย
            if (Input.GetMouseButtonDown(0))
            {
                TryGrab();
            }

            // ปล่อยคลิก
            if (Input.GetMouseButtonUp(0))
            {
                Release();
            }
        }

        // ถ้ากำลังถือของ
        if (grabbedObject != null)
        {
            MoveObject();
        }
    }

    void TryGrab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                grabbedObject = rb;
                grabbedObject.useGravity = false;
            }
        }
    }

    void MoveObject()
    {
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;

        grabbedObject.velocity = (targetPosition - grabbedObject.position) * moveSpeed;
    }

    void Release()
    {
        if (grabbedObject != null)
        {
            grabbedObject.useGravity = true;
            grabbedObject = null;
        }
    }

    // ตรวจว่าอยู่ใน zone
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabZone"))
        {
            canGrab = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GrabZone"))
        {
            canGrab = false;
        }
    }
}