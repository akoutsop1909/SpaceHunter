using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject hazard1;
    public GameObject hazard2;
    public GameObject hazard3;

    //pickups
    public GameObject coin;
    public GameObject powerup;
    public GameObject boom;

    public Vector3 spawnValues;
    public float spawnWait;
    public float startWait;

    public Text scoreText;
    public Text hiScoreText;

    public static bool PlayerKilled = false;
    public static bool NewHighScore = false;

    private int score;
    private int highscore;


    void Start()
    {
        score = 0;
        highscore = PlayerPrefs.GetInt("highscore", highscore);
        hiScoreText.text = highscore.ToString();
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            Vector3 spawnPosition1 = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Vector3 spawnPosition2 = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;

            int randomSpawn = Random.Range(1, 50);
            if (randomSpawn == 5)
            {
                Vector3 coinSpawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(coin, coinSpawnPosition, spawnRotation);
            }

            randomSpawn = Random.Range(1, 60);
            if (randomSpawn == 5)
            {
                Vector3 powerupSpawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(powerup, powerupSpawnPosition, spawnRotation);
            }

            randomSpawn = Random.Range(1, 70);
            if (randomSpawn == 5)
            {
                Vector3 boomSpawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(boom, boomSpawnPosition, spawnRotation);
            }

            int hazardType = Random.Range(1, 4);
            switch (hazardType)
            {
                case 1:
                    Instantiate(hazard1, spawnPosition2, spawnRotation);
                    Instantiate(hazard1, spawnPosition1, spawnRotation);
                    break;

                case 2:
                    Instantiate(hazard1, spawnPosition2, spawnRotation);
                    Instantiate(hazard2, spawnPosition1, spawnRotation);
                    break;

                default:
                    Instantiate(hazard1, spawnPosition2, spawnRotation);
                    Instantiate(hazard3, spawnPosition1, spawnRotation);
                    break;
            }

            yield return new WaitForSeconds(spawnWait);
            if (PlayerKilled) break;
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void StopAudio()
    {
        GetComponent<AudioSource>().Pause();
    }

    public void StartAudio()
    {
        GetComponent<AudioSource>().Play();
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString();
        if (score > highscore)
        {
            NewHighScore = true;
            highscore = score;
            hiScoreText.text = highscore.ToString();
            PlayerPrefs.SetInt("highscore", highscore);
            PlayerPrefs.Save();
        }
    }
}
