using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;

    public string[] lines;
    public AudioClip[] voices;

    public AudioSource speaker;

    public bool playOnStart;

    private bool hasPlayed = false;

    void Start()
    {
        if (playOnStart && !hasPlayed)
        {
            hasPlayed = true;
            StartCoroutine(manager.PlayDialogue(lines, voices, speaker));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;
            StartCoroutine(manager.PlayDialogue(lines, voices, speaker));
        }
    }
}