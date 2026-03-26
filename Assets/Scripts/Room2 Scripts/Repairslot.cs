using UnityEngine;
using System.Collections.Generic;

public class RepairSlot : MonoBehaviour
{
    public List<GameObject> requiredObjects = new List<GameObject>();

    public GameObject warpDoor;

    public List<GameObject> objectsToDisable = new List<GameObject>();
    public List<GameObject> objectsToEnable = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (requiredObjects.Contains(other.gameObject))
        {
            requiredObjects.Remove(other.gameObject);

            Destroy(other.gameObject);

            CheckComplete();
        }
    }

    void CheckComplete()
    {
        if (requiredObjects.Count == 0)
        {
            warpDoor.SetActive(true);

            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in objectsToEnable)
            {
                obj.SetActive(true);
            }
        }
    }
}