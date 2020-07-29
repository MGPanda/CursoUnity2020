using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        loading,
        inGame,
        gameOver
    }

    public GameState gameState;
    public List<GameObject> targetPrefabs;
    private float spawnRate = 1.5f;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI gameOverText;

    public Button restartButton;

    private int _score;

    public GameObject titleScreen;

    private int numberOfLives = 4;

    public List<GameObject> lives;

    private int score
    {
        set { _score = Mathf.Max(value, 0); }
        get { return _score; }
    }

    private void Start()
    {
        ShowMaxScore();
    }

    IEnumerator SpawnTarget()
    {
        while (gameState == GameState.inGame)
        {
            yield return new WaitForSeconds(spawnRate);
            int idx = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[idx]);
        }
    }

    /// <summary>
    /// Actualiza la puntuación y la muestra por pantalla.
    /// </summary>
    /// <param name="scoreToAdd">Puntos a añadir a la puntuación global.</param>
    public void UpdateScore(int scoreToAdd)
    {
        if (gameState == GameState.inGame)
        {
            score += scoreToAdd;
            scoreText.text = "Score:\n" + score;
        }
    }

    public void GameOver()
    {
        numberOfLives--;
        if (numberOfLives >= 0)
        {
            Image heartImage = lives[numberOfLives].GetComponent<Image>();
            Color tempColor = heartImage.color;
            tempColor.a = 0.3f;
            heartImage.color = tempColor;
        }
        
        if (numberOfLives <= 0)
        {
            SetMaxScore();
            gameState = GameState.gameOver;
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        gameState = GameState.inGame;
        titleScreen.SetActive(false);

        spawnRate /= difficulty;
        numberOfLives -= difficulty;

        for (int i = 0; i < numberOfLives; i++)
        {
            lives[i].SetActive(true);
        }

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
    }

    void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MAX_SCORE", 0);
        scoreText.text = "Max Score:\n" + maxScore;
    }

    void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MAX_SCORE", 0);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt("MAX_SCORE", score);
        }
    }
}