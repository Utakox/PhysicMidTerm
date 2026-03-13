using UnityEngine;
using System.Collections.Generic;

public class AirMachineRepair : MonoBehaviour
{
    public List<GameObject> requiredItems = new List<GameObject>();

    public List<AudioSource> airMachineSounds = new List<AudioSource>();

    public List<MonoBehaviour> airMachineScripts = new List<MonoBehaviour>();

    public TrapButton trapSystem;

    void OnTriggerEnter(Collider other)
    {
        if (requiredItems.Contains(other.gameObject))
        {
            requiredItems.Remove(other.gameObject);

            Destroy(other.gameObject);

            CheckComplete();
        }
    }

    void CheckComplete()
    {
        if (requiredItems.Count == 0)
        {
            ShutdownMachines();
        }
    }

    void ShutdownMachines()
    {
        // ปิดเสียงเครื่องเป่า
        foreach (AudioSource a in airMachineSounds)
        {
            a.Stop();
        }

        // ปิด script ที่ทำให้เครื่องเป่า
        foreach (MonoBehaviour script in airMachineScripts)
        {
            script.enabled = false;
        }

        // ปิดระบบ trap
        trapSystem.StopTrap();
    }
}