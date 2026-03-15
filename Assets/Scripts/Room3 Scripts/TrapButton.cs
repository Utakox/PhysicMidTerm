using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TrapButton : MonoBehaviour
{
    public AudioSource announcerSource;

    public List<AudioClip> announcementVoices = new List<AudioClip>();
    public List<GameObject> sirenLights = new List<GameObject>();

    public AudioClip backgroundMusic;
    public AudioSource musicSource;

    public RawImage blackScreen;

    public float repairTime = 180f;

    [Header("Game Over")]
    public VideoPlayer gameOverVideo;

    public AudioSource gameOverMusicSource;
    public AudioClip gameOverMusic;

    public AudioSource gameOverVoiceSource;
    public List<AudioClip> gameOverVoices = new List<AudioClip>();

    private bool activated = false;
    private bool gameOver = false;

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    void OnMouseDown()
    {
        if (!activated)
        {
            StartCoroutine(StartTrap());
        }
    }

    IEnumerator StartTrap()
    {
        activated = true;

        // เล่นเสียง narrator
        foreach (AudioClip voice in announcementVoices)
        {
            announcerSource.clip = voice;
            announcerSource.Play();
            yield return new WaitForSecondsRealtime(voice.length);
        }

        // เปิดไฟ alarm
        foreach (GameObject light in sirenLights)
        {
            light.SetActive(true);
        }

        // เล่น background music
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();

        StartCoroutine(FadeToBlack());

        yield return new WaitForSecondsRealtime(repairTime);

        TriggerGameOver();
    }

    IEnumerator FadeToBlack()
    {
        float time = 0f;
        float duration = repairTime;

        Color c = blackScreen.color;

        while (time < duration)
        {
            time += Time.deltaTime;

            float alpha = time / duration;

            blackScreen.color = new Color(c.r, c.g, c.b, alpha);

            yield return null;
        }

        blackScreen.color = new Color(c.r, c.g, c.b, 1f);
    }

    void TriggerGameOver()
    {
        if (gameOver) return;

        gameOver = true;
        activated = false;

        musicSource.Stop();
        announcerSource.Stop();

        foreach (GameObject light in sirenLights)
        {
            light.SetActive(false);
        }

        if (blackScreen != null)
        {
            blackScreen.gameObject.SetActive(false);
        }

        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        // เล่น video
        if (gameOverVideo != null)
        {
            gameOverVideo.gameObject.SetActive(true);
            gameOverVideo.Stop();
            gameOverVideo.frame = 0;
            gameOverVideo.Play();
        }

        // เล่นเพลง Game Over
        if (gameOverMusicSource != null && gameOverMusic != null)
        {
            gameOverMusicSource.clip = gameOverMusic;
            gameOverMusicSource.loop = true;
            gameOverMusicSource.Play();
        }

        // dialogue ทีละประโยค
        foreach (AudioClip voice in gameOverVoices)
        {
            gameOverVoiceSource.clip = voice;
            gameOverVoiceSource.Play();

            yield return new WaitForSecondsRealtime(voice.length + 0.2f);
        }

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StopTrap()
    {
        activated = false;

        musicSource.Stop();

        foreach (GameObject light in sirenLights)
        {
            light.SetActive(false);
        }
    }
}