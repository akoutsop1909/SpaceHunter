using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Canvas gameOverMenuUI;
    public Text resetSuccessful;
    public Text hasNewHighScore;
    public Text hiScoreText;

    // Update is called once per frame
    void Update()
    {
        if (GameController.PlayerKilled) GameOver();
    }

    void GameOver()
    {
        Cursor.visible = true;
        gameOverMenuUI.gameObject.SetActive(true);
        if (GameController.NewHighScore) StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            hasNewHighScore.text = "NEW HIGH SCORE";
            yield return new WaitForSeconds(.5f*2);
            hasNewHighScore.text = "";
            yield return new WaitForSeconds(.5f*2);

        }
    }

    public void Quit()
    {
        GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    public void Restart()
    {
        GetComponent<AudioSource>().Play();

        GameController.PlayerKilled = false;
        GameController.NewHighScore = false;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ResetHiScore()
    {
        GetComponent<AudioSource>().Play();

        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.Save();
        resetSuccessful.text = "HI-SCORE HAS BEEN RESET";
        hiScoreText.text = "0";
    }
}
