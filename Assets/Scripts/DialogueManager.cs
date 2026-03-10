using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;

    public IEnumerator PlayDialogue(string[] lines, AudioClip[] voices, AudioSource speaker)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            dialogueText.text = "";

            // เริ่มเสียง
            speaker.clip = voices[i];
            speaker.Play();

            // พิมพ์ข้อความ
            foreach (char c in lines[i])
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }

            // รอเสียงจบ
            yield return new WaitWhile(() => speaker.isPlaying);

            // เว้นช่วงก่อนประโยคต่อไป
            yield return new WaitForSeconds(1f);
        }

        dialogueText.text = "";
    }
}