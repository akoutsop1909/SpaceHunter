using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public Canvas pauseMenuUI;

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) gameController = gameControllerObject.GetComponent<GameController>();
        else Debug.Log("Cannot find 'GameController' script");
    }

    void Update()
    {
        if (!GameController.PlayerKilled)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused) Resume();
                else Pause();
            }
        }
    }

    public void Resume()
    {
        GetComponent<AudioSource>().Play();

        pauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        gameController.StartAudio();
        Cursor.visible = false;
    }

    void Pause()
    {
        GetComponent<AudioSource>().Play();

        pauseMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        gameController.StopAudio();
        Cursor.visible = true;
    }
}