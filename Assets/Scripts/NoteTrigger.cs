using UnityEngine;

public class NoteTrigger : NoteBase
{
    // ถ้าอยากเพิ่ม behavior เฉพาะของแต่ละโน้ตก็ override ได้
    protected override void ShowNote()
    {
        base.ShowNote();
        Debug.Log("Note opened: " + gameObject.name);
    }

    protected override void HideNote()
    {
        base.HideNote();
        Debug.Log("Note closed: " + gameObject.name);
    }
}
