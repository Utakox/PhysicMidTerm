using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;

    public string[] lines;
    public AudioClip[] voices;

    public AudioSource speaker;

    public bool playOnStart;

    void Start()
    {
        if (playOnStart)
        {
            StartCoroutine(manager.PlayDialogue(lines, voices, speaker));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(manager.PlayDialogue(lines, voices, speaker));
        }
    }
}