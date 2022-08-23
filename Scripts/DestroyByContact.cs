using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public Sprite broken;
    public Sprite pieces;
    public GameObject explosion;
    public GameObject playerExplosion;

    public int scoreValue;
    public int hitValue;

    private int health = 100;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) gameController = gameControllerObject.GetComponent<GameController>();
        else Debug.Log("Cannot find 'GameController' script");
    }

    void PlayerKilled(Collider2D other)
    {
        Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(other.gameObject);
        Destroy(gameObject);

        GameController.PlayerKilled = true;
    }

    void AsteroidsCollide(Collider2D other)
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Coin" || other.tag == "Powerup" || other.tag == "Boom") return;
        if (other.tag == "Small" || other.tag == "Medium" || other.tag == "Large")
        {
            AsteroidsCollide(other);
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "Coin" || other.tag == "Powerup" || other.tag == "Boom") return;

        //destroy object tagged as Small
        if (tag == "Small")
        {
            if (other.tag == "Small" || other.tag == "Medium" || other.tag == "Large")
            {
                AsteroidsCollide(other);
                return;
            }

            if (other.tag == "Player")
            {
                PlayerKilled(other);
                return;
            }

            Instantiate(explosion, transform.position, transform.rotation);
            gameController.AddScore(scoreValue);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        //destroy object tagged as Medium after it takes 2 hits
        else if (tag == "Medium")
        {
            if (other.tag == "Small" || other.tag == "Medium" || other.tag == "Large")
                {
                    AsteroidsCollide(other);
                    return;
                }

                if (health == 0)
                {
                    if (other.tag == "Player")
                    {
                        PlayerKilled(other);
                        return;
                    }

                    Instantiate(explosion, transform.position, transform.rotation);
                    gameController.AddScore(scoreValue);

                    Destroy(other.gameObject);
                    Destroy(gameObject);
            }

            else
            {
                if (other.tag == "Player")
                {
                    PlayerKilled(other);
                    return;
                }

                health = health - 50;
                gameObject.GetComponent<SpriteRenderer>().sprite = broken;
                GetComponent<AudioSource>().Play();
                gameController.AddScore(hitValue);

                Destroy(other.gameObject);
            }
        }

        //destroy object tagged as Large after it takes 3 hits
        else if (tag == "Large")
        {
            if (other.tag == "Small" || other.tag == "Medium" || other.tag == "Large")
            {
                AsteroidsCollide(other);
                return;
            }

            if (health < 0)
            {
                if (other.tag == "Player")
                {
                    PlayerKilled(other);
                    return;
                }

                Instantiate(explosion, transform.position, transform.rotation);
                gameController.AddScore(scoreValue);

                Destroy(other.gameObject);
                Destroy(gameObject);
            }

            else
            {
                if (other.tag == "Player")
                {
                    PlayerKilled(other);
                    return;
                }

                GetComponent<AudioSource>().Play();
                health = health - 32;
                gameController.AddScore(hitValue);

                Destroy(other.gameObject);
            }

            if (health < 20) gameObject.GetComponent<SpriteRenderer>().sprite = pieces;
            else if (health < 70) gameObject.GetComponent<SpriteRenderer>().sprite = broken;
        }
    }
}