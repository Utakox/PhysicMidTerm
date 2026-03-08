using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;

    public void StartDialogue(string[] lines, AudioSource speaker, AudioClip voice)
    {
        StartCoroutine(TypeLines(lines, speaker, voice));
    }

    IEnumerator TypeLines(string[] lines, AudioSource speaker, AudioClip voice)
    {
        speaker.PlayOneShot(voice);

        foreach (string line in lines)
        {
            dialogueText.text = "";

            foreach (char c in line)
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(2f);
        }

        dialogueText.text = "";
    }
}