using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallScript : MonoBehaviour
{
    public Rigidbody2D body;
    public GameObject paddle;
    public GameObject mainBall;
    private Vector2 currentVelocity;
    private Vector2 startOffVector;
    public float speed = 5;
    public bool ballStarted = true;
    public AudioSource paddleSound;
    public AudioSource brickSound;
    public AudioSource gooSound;
    public AudioSource powerUpSound;
    // Start is called before the first frame update
    void Start()
    {
        paddle = GameObject.FindGameObjectWithTag("Paddle");
        mainBall = GameObject.FindGameObjectWithTag("MainBall");
        startOffVector = new Vector2(0, 5);
        body.velocity = mainBall.GetComponent<Rigidbody2D>().velocity;
        ballStarted = true;
    }
    private void FixedUpdate()
    {
        currentVelocity = body.velocity;
    }
    // Update is called once per frame
    void Update()
    {
        if (ballStarted)
        {
            if (body.velocity.y < 0.2 && body.velocity.y > -0.2)
            {
                body.velocity = new Vector2(body.velocity.x, body.velocity.y + 0.25f);
            }
            body.velocity = body.velocity.normalized * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        body.velocity = Vector2.Reflect(currentVelocity, collision.GetContact(0).normal);

        if (collision.gameObject.layer == 13) //Goo
        {
            gooSound.Play();
        }

        if (collision.gameObject.layer == 9)//Paddel Layer
        {
            //If hit the double Paddle
            if (collision.GetContact(0).point.x > paddle.transform.position.x + paddle.GetComponent<Renderer>().bounds.size.x)
            {
                float doublePaddleX = paddle.transform.GetChild(2).position.x;
                if (collision.GetContact(0).point.x > doublePaddleX + 0.2 || collision.GetContact(0).point.x < doublePaddleX - 0.2)
                {
                    float offset = doublePaddleX - collision.GetContact(0).point.x;
                    body.velocity = new Vector2(body.velocity.x - offset * 4, body.velocity.y);
                }

            }//if hit the normal paddle
            else if (collision.GetContact(0).point.x > paddle.transform.position.x + 0.2 || collision.GetContact(0).point.x < paddle.transform.position.x - 0.2)
            {
                float offset = paddle.transform.position.x - collision.GetContact(0).point.x;
                body.velocity = new Vector2(body.velocity.x - offset * 4, body.velocity.y);
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 9 || collision.gameObject.layer == 13) && ballStarted)//Paddel or Goo
        {
            paddleSound.Play();
        }
        else if (collision.gameObject.layer == 15)//Brick
        {
            brickSound.Play();
        }
        else if (collision.gameObject.layer == 10)//Glow
        {
            brickSound.Play();
            powerUpSound.Play();
        }
        else
        {
            brickSound.Play();
        }
    }


    public void startBall()
    {
        ballStarted = true;
        body.velocity = startOffVector;
        paddleSound.Play();
    }
}
