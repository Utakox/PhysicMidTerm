using UnityEngine;

public class Door : MonoBehaviour
{
    public float requiredMass = 20f;
    public Collider triggerZone;

    public Transform teleportTarget;

    public AudioSource audioSource;
    public AudioClip openSound;

    public GameObject tpEffect;

    bool activated = false;

    void Start()
    {
        if (tpEffect)
            tpEffect.SetActive(false);
    }

    void Update()
    {
        if (!activated)
        {
            CheckMass();
        }
    }

    void CheckMass()
    {
        Collider[] objects = Physics.OverlapBox(
            triggerZone.bounds.center,
            triggerZone.bounds.extents,
            triggerZone.transform.rotation
        );

        foreach (Collider obj in objects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            if (rb != null && rb.mass >= requiredMass)
            {
                ActivateDoor();
                break;
            }
        }
    }

    void ActivateDoor()
    {
        activated = true;

        if (tpEffect)
            tpEffect.SetActive(true);

        if (audioSource && openSound)
            audioSource.PlayOneShot(openSound);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!activated) return;

        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportTarget.position;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb) rb.velocity = Vector3.zero;
        }
    }
}