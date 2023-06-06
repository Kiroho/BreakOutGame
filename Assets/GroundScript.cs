using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public LogicScript logic;
    public PaddleScript paddle;
    public GameObject ball;
    public AudioSource ballOut;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.layer == 8)//MainBall
        //{
        //    Debug.Log("Ball out");
        //    paddle.joint.autoConfigureConnectedAnchor = false;
        //    paddle.joint.connectedAnchor = new Vector2(0, 0);
        //    ball.GetComponent<Rigidbody2D>().position = new Vector2(paddle.transform.position.x, paddle.transform.position.y + 0.35f);
        //    paddle.joint.connectedBody = ball.GetComponent<Rigidbody2D>();
        //    ball.GetComponent<BallScript>().ballStarted = false;
        //    logic.decreaseLife();
        //}

        if (collision.gameObject.layer == 8)//MainBall
        {
            Debug.Log("Ball out");
            ballOut.Play();
            logic.attachBallToPaddle();
            logic.decreaseLife();


        }

        if (collision.gameObject.layer == 11)//MultiBall
        {
            ballOut.Play();
            Destroy(collision.gameObject);
        }
    }
}
