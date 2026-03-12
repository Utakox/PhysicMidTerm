using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Mouse Look")]
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    float verticalLookRotation = 0f;

    [Header("Head Bob")]
    public float bobSpeed = 6f;
    public float bobAmount = 0.05f;

    float bobTimer;
    Vector3 camStartPos;

    Rigidbody rb;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        

        camStartPos = cameraHolder.localPosition;
        
    }

    void Update()
    {
        MouseLook();
        Jump();
        HeadBob();
    }

    void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;
        Move();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        Vector3 velocity = new Vector3(move.x * moveSpeed, rb.velocity.y, move.z * moveSpeed);

        rb.velocity = velocity;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HeadBob()
{
    Vector3 horizontalVelocity = rb.velocity;
    horizontalVelocity.y = 0;

    float speed = horizontalVelocity.magnitude;

    if (speed > 0.1f && isGrounded)
    {
        bobTimer += Time.deltaTime * bobSpeed;
    }

    float bobY = Mathf.Sin(bobTimer) * bobAmount * speed;
    float bobX = Mathf.Cos(bobTimer * 0.5f) * bobAmount * speed;

    Vector3 targetPos = camStartPos + new Vector3(bobX, bobY, 0);

    cameraHolder.localPosition = Vector3.Lerp(
        cameraHolder.localPosition,
        targetPos,
        Time.deltaTime * 8f
    );
}

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < 45)
            {
                isGrounded = true;
                return;
            }
        }

        isGrounded = false;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}