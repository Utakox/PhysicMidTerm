using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // 🔥 สำคัญ

public class TrapSystem : MonoBehaviour
{
    public DropMachine dropMachine;
    public AirMachineRepair repairSystem;

    [Header("Objects to Activate")]
    public List<GameObject> objectsToActivate;

    [Header("Audio")]
    public AudioSource ambientSource;
    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    [Header("Timer UI (World Text)")]
    public TextMeshPro timerText;

    public RawImage blackScreen;
    public float repairTime = 180f;

    [Header("Repair Voice")]
    public AudioSource repairVoiceSource;
    public AudioClip repairVoiceClip;
    public float repairDelay = 4f;

    [Header("Game Over")]
    public VideoPlayer gameOverVideo;
    public AudioSource gameOverMusicSource;
    public AudioClip gameOverMusic;

    public AudioSource gameOverVoiceSource;
    public List<AudioClip> gameOverVoices;

    private bool gameOver = false;

    void Update()
    {
        // 🔥 กด R เพื่อ Restart
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void StartTrap()
    {
        StartCoroutine(TrapRoutine());
    }

    IEnumerator TrapRoutine()
    {
        if (ambientSource != null)
            StartCoroutine(FadeOutSound(ambientSource, 1.5f));

        if (repairSystem != null)
            repairSystem.EnableRepair();

        foreach (var obj in objectsToActivate)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        if (dropMachine != null)
            dropMachine.StartDrop();

        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        if (repairVoiceSource != null && repairVoiceClip != null)
            StartCoroutine(RepairLoop());

        if (blackScreen != null)
            StartCoroutine(FadeToBlack());

        StartCoroutine(UpdateTimer());

        yield return new WaitForSeconds(repairTime);

        TriggerGameOver();
    }

    IEnumerator UpdateTimer()
    {
        float timeLeft = repairTime;

        while (timeLeft > 0 && !gameOver)
        {
            timeLeft -= Time.deltaTime;

            if (timerText != null)
            {
                int seconds = Mathf.CeilToInt(timeLeft);
                timerText.text = "<b><color=#FF0000>" + seconds + "s</color></b>";
            }

            yield return null;
        }

        if (timerText != null)
            timerText.text = "<b><color=#FF0000>0s</color></b>";
    }

    IEnumerator FadeOutSound(AudioSource audio, float duration)
    {
        float startVolume = audio.volume;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            audio.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        audio.volume = 0f;
    }

    IEnumerator RepairLoop()
    {
        yield return new WaitForSeconds(2f);

        while (!gameOver)
        {
            repairVoiceSource.PlayOneShot(repairVoiceClip);
            yield return new WaitForSeconds(repairVoiceClip.length + repairDelay);
        }
    }

    IEnumerator FadeToBlack()
    {
        float t = 0;
        Color c = blackScreen.color;

        while (t < repairTime)
        {
            t += Time.deltaTime;
            float a = t / repairTime;
            blackScreen.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }
    }

    void TriggerGameOver()
    {
        if (gameOver) return;

        gameOver = true;

        if (musicSource != null)
            musicSource.Stop();

        if (repairVoiceSource != null)
            repairVoiceSource.Stop();

        foreach (var obj in objectsToActivate)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        if (blackScreen != null)
            blackScreen.gameObject.SetActive(false);

        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        if (gameOverVideo != null)
        {
            gameOverVideo.gameObject.SetActive(true);
            gameOverVideo.Play();
        }

        if (gameOverMusicSource != null && gameOverMusic != null)
        {
            gameOverMusicSource.clip = gameOverMusic;
            gameOverMusicSource.loop = true;
            gameOverMusicSource.Play();
        }

        foreach (var voice in gameOverVoices)
        {
            if (gameOverVoiceSource != null)
            {
                gameOverVoiceSource.PlayOneShot(voice);
                yield return new WaitForSecondsRealtime(voice.length); // 🔥 สำคัญ
            }
        }

        // 🔥 แสดงข้อความ restart
        if (timerText != null)
        {
            timerText.text = "<b><color=#FF0000>PRESS R TO RESTART</color></b>";
        }

        Time.timeScale = 0f; // 🔥 หยุดเวลา
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StopTrap()
    {
        if (gameOver) return;

        gameOver = true;

        StopAllCoroutines();

        if (dropMachine != null)
            dropMachine.StopAllCoroutines();

        if (musicSource != null)
            musicSource.Stop();

        if (repairVoiceSource != null)
            repairVoiceSource.Stop();

        if (gameOverMusicSource != null)
            gameOverMusicSource.Stop();

        if (gameOverVoiceSource != null)
            gameOverVoiceSource.Stop();

        foreach (var obj in objectsToActivate)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        if (blackScreen != null)
        {
            blackScreen.color = new Color(0, 0, 0, 0);
            blackScreen.gameObject.SetActive(false);
        }

        if (gameOverVideo != null)
        {
            gameOverVideo.Stop();
            gameOverVideo.gameObject.SetActive(false);
        }

        this.enabled = false;
    }
}