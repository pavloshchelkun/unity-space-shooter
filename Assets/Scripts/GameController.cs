using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] Hazards;
    public Vector3 SpawnValues;
    public int HazardCount;
    public float SpawnWait;
    public float StartWait;
    public float WaveWait;

    public Text ScoreText;
    public GameObject RestartButton;
    public Text GameOverText;

    private bool _gameOver;
    private int _score;

    private void Start()
    {
        _gameOver = false;
        RestartButton.SetActive(false);
        GameOverText.text = "";
        _score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(StartWait);
        while (true)
        {
            for (int i = 0; i < HazardCount; i++)
            {
                GameObject hazard = Hazards[Random.Range(0, Hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(SpawnWait);
            }
            yield return new WaitForSeconds(WaveWait);

            if (_gameOver)
            {
                RestartButton.SetActive(true);
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        _score += newScoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        ScoreText.text = "Score: " + _score;
    }

    public void GameOver()
    {
        GameOverText.text = "Game Over!";
        _gameOver = true;
    }
}
