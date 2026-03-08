using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    public string[] lines;

    public AudioSource speaker;
    public AudioClip voice;

    public bool playOnStart;

    void Start()
    {
        if (playOnStart)
        {
            manager.StartDialogue(lines, speaker, voice);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.StartDialogue(lines, speaker, voice);
        }
    }
}