using UnityEngine;

public abstract class NoteBase : MonoBehaviour
{
    [Header("Note Image")]
    public GameObject noteImage; // รูปโน้ตที่จะเปิด/ปิด

    protected bool playerInRange = false; // อยู่ในระยะหรือไม่

    // เมื่อผู้เล่นเข้ามาใกล้
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            ShowNote();
        }
    }

    // เมื่อผู้เล่นออกห่าง
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HideNote();
        }
    }

    // ฟังก์ชันเปิดโน้ต (ลูกสามารถ override ได้)
    protected virtual void ShowNote()
    {
        if (noteImage != null)
            noteImage.SetActive(true);
    }

    // ฟังก์ชันปิดโน้ต
    protected virtual void HideNote()
    {
        if (noteImage != null)
            noteImage.SetActive(false);
    }
}
