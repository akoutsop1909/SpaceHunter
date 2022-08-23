using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawnLeft;
    public Transform shotSpawnRight;
    public Transform HiddenShotSpawnLeft;
    public Transform HiddenShotSpawnRight;
    public float fireRate;

    private float nextFire;
    private bool flag = false;
    private bool isCalled = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
    }

    void Update()
    {
        //fires 2 bolts 
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawnLeft.position, shotSpawnLeft.rotation);
                Instantiate(shot, shotSpawnRight.position, shotSpawnRight.rotation);
                if (flag)
                {
                    Instantiate(shot, HiddenShotSpawnLeft.position, HiddenShotSpawnLeft.rotation);
                    Instantiate(shot, HiddenShotSpawnRight.position, HiddenShotSpawnRight.rotation);
                }
                GetComponent<AudioSource>().Play();
            }
        }
    }

    void FixedUpdate()
    {
        //player movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = movement * speed;

        //boundary
        rb2d.position = new Vector2 (Mathf.Clamp(rb2d.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(rb2d.position.y, boundary.yMin, boundary.yMax));
    }

    public void CallRapidFire()
    {
        if (isCalled == false) StartCoroutine(RapidFire());
        else return;
    }

    IEnumerator RapidFire()
    {
        isCalled = true;
        float temptFireRate = fireRate;

        fireRate = 0.1f;
        flag = true;

        yield return new WaitForSeconds(10);

        fireRate = temptFireRate;
        flag = false;
        isCalled = false;
    }
}
