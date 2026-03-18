using UnityEngine;
using System.Collections;

public class DropMachine : MonoBehaviour
{
    public Transform machine; // ตัว object ที่จะเลื่อนลง
    public float dropDistance = 5f; // ระยะที่จะลง
    public float dropSpeed = 2f; // ความเร็ว

    private bool isDropping = false;

    public void StartDrop()
    {
        if (!isDropping)
        {
            StartCoroutine(Drop());
        }
    }

    IEnumerator Drop()
    {
        isDropping = true;

        Vector3 startPos = machine.position;
        Vector3 targetPos = startPos + Vector3.down * dropDistance;

        while (Vector3.Distance(machine.position, targetPos) > 0.01f)
        {
            machine.position = Vector3.MoveTowards(
                machine.position,
                targetPos,
                dropSpeed * Time.deltaTime
            );

            yield return null;
        }

        machine.position = targetPos;
    }
}