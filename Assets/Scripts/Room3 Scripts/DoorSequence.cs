using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorSequence : MonoBehaviour
{
    [Header("Alarm")]
    public AudioSource alarmSource;
    public float alarmDuration = 3f;

    [Header("Objects To Enable")]
    public List<GameObject> objectsToEnable = new List<GameObject>();

    [Header("Door Settings")]
    public Transform door;
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    private bool isActivated = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isActivated)
        {
            CheckClick();
        }
    }

    void CheckClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            // ถ้ากดตรง object นี้เอง
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                Activate();
            }
        }
    }

    public void Activate()
    {
        if (isActivated) return;
        isActivated = true;

        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        // เปิด objects
        foreach (GameObject obj in objectsToEnable)
        {
            if (obj != null) obj.SetActive(true);
        }

        // เล่น alarm
        if (alarmSource != null)
        {
            alarmSource.loop = true;
            alarmSource.Play();
        }

        // เลื่อนประตู
        if (door != null)
        {
            Vector3 startPos = door.position;
            Vector3 targetPos = startPos + Vector3.down * moveDistance;

            while (Vector3.Distance(door.position, targetPos) > 0.01f)
            {
                door.position = Vector3.MoveTowards(door.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            door.position = targetPos;
        }

        // รอ alarm ครบเวลา
        if (alarmSource != null)
        {
            yield return new WaitForSeconds(alarmDuration);
            alarmSource.Stop();
        }

        // ปิด objects
        foreach (GameObject obj in objectsToEnable)
        {
            if (obj != null) obj.SetActive(false);
        }
    }
}