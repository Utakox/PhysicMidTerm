using UnityEngine;
using System.Collections.Generic;

public class RepairSlot : MonoBehaviour
{
    public List<GameObject> requiredObjects = new List<GameObject>();

    public AudioSource audioSource;
    public AudioClip repairSound;

    public GameObject warpDoor; // ประตูที่มีอยู่แล้วใน scene

    void OnTriggerEnter(Collider other)
    {
        if (requiredObjects.Contains(other.gameObject))
        {
            requiredObjects.Remove(other.gameObject);

            Destroy(other.gameObject);

            audioSource.PlayOneShot(repairSound);

            CheckComplete();
        }
    }

    void CheckComplete()
    {
        if (requiredObjects.Count == 0)
        {
            warpDoor.SetActive(true);
        }
    }
}