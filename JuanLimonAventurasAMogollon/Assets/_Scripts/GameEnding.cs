using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;

    public float displayImageDuration = 1f;

    private bool playerAtExit, playerCaught;

    public GameObject player;

    public CanvasGroup exitBackgroundImageCanvasGroup, caughtBackgroundImageCanvasGroup;

    private float timer;

    public AudioSource exitAudio, caughtAudio;

    public bool hasAudioPlayed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerAtExit = true;
        }
    }

    private void Update()
    {
        if (playerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (playerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = Mathf.Clamp(timer / fadeDuration, 0, 1);
        if (timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Application.Quit();
            }
        }
    }
    public void CatchPlayer()
    {
        playerCaught = true;
    }
}