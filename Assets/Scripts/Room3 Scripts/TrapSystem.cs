using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TrapSystem : MonoBehaviour
{
    public DropMachine dropMachine;

    [Header("Objects to Activate")]
    public List<GameObject> objectsToActivate; // 🔥 เพิ่มตรงนี้

    public AudioSource musicSource;
    public AudioClip backgroundMusic;

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

    public void StartTrap()
    {
        StartCoroutine(TrapRoutine());
    }

    IEnumerator TrapRoutine()
    {
        // 🔥 เปิดทุก object ที่กำหนด
        foreach (var obj in objectsToActivate)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        // 🔽 machine
        if (dropMachine != null)
            dropMachine.StartDrop();

        // 🎵 music
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        // 🗣️ repair loop
        if (repairVoiceSource != null && repairVoiceClip != null)
            StartCoroutine(RepairLoop());

        // 🌑 fade
        if (blackScreen != null)
            StartCoroutine(FadeToBlack());

        yield return new WaitForSeconds(repairTime);

        TriggerGameOver();
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

        // 🔥 ปิด object ที่เคยเปิด (ถ้าต้องการ)
        foreach (var obj in objectsToActivate)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // กันจอดำบัง
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
                yield return new WaitForSeconds(voice.length);
            }
        }

        Time.timeScale = 0f;
    }

    public void StopTrap()
{
    if (gameOver) return;

    gameOver = true;

    // ❌ หยุด coroutine ทั้งหมด
    StopAllCoroutines();

    // ❌ หยุด drop machine
    if (dropMachine != null)
        dropMachine.StopAllCoroutines();

    // ❌ หยุดเสียงทั้งหมด
    if (musicSource != null)
        musicSource.Stop();

    if (repairVoiceSource != null)
        repairVoiceSource.Stop();

    if (gameOverMusicSource != null)
        gameOverMusicSource.Stop();

    if (gameOverVoiceSource != null)
        gameOverVoiceSource.Stop();

    // ❌ ปิด object ที่เคยเปิด
    foreach (var obj in objectsToActivate)
    {
        if (obj != null)
            obj.SetActive(false);
    }

    // ❌ ปิดจอดำ
    if (blackScreen != null)
    {
        blackScreen.color = new Color(0, 0, 0, 0);
        blackScreen.gameObject.SetActive(false);
    }

    // ❌ ปิด video ถ้ามี
    if (gameOverVideo != null)
    {
        gameOverVideo.Stop();
        gameOverVideo.gameObject.SetActive(false);
    }

    // ❌ ปิด script นี้จริง ๆ
    this.enabled = false;
}
}