using UnityEngine;
using UnityEngine.UI;

public class EndGameTrigger : MonoBehaviour
{
    public GameObject endScreenUI; // UI จอดำ + ข้อความ

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        endScreenUI.SetActive(true);

        Time.timeScale = 0f; // หยุดเกม
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}