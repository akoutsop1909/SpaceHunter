using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPickup : MonoBehaviour
{
    public GameObject sound;
    public int scoreValue;
    public GameObject explosion;

    private GameController gameController;
    private PlayerController playerController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) gameController = gameControllerObject.GetComponent<GameController>();
        else Debug.Log("Cannot find 'GameController' script");

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null) playerController = playerControllerObject.GetComponent<PlayerController>();
        else Debug.Log("Cannot find 'PlayerController' script");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(sound, transform.position, transform.rotation);
            gameController.AddScore(scoreValue);

            if (tag == "Powerup") playerController.CallRapidFire();

            if (tag == "Boom")
            {
                GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Small");
                ScoreAndDestroy(asteroids, 35);

                asteroids = GameObject.FindGameObjectsWithTag("Medium");
                ScoreAndDestroy(asteroids, 70);

                asteroids = GameObject.FindGameObjectsWithTag("Large");
                ScoreAndDestroy(asteroids, 105);
            }
        }

        else return;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Boundary") return;
    }

    void ScoreAndDestroy(GameObject[] other, int points)
    {
        foreach (GameObject go in other)
        {
            Instantiate(explosion, go.transform.position, go.transform.rotation);
            Destroy(go);
            gameController.AddScore(points * scoreValue);
        }
    }
}
