using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooScript : MonoBehaviour
{
    public PaddleScript paddle;
    public GameObject ball;
    public LogicScript logic;
    public Vector2 autoX;

    private void Start()
    {
        paddle = GameObject.FindGameObjectWithTag("Paddle").GetComponent<PaddleScript>();
        ball = GameObject.FindGameObjectWithTag("MainBall");
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == 8 && ball.GetComponent<BallScript>().ballStarted)//MainBall
        //{
        //    Debug.Log("Ball Gooed");
        //    ball.GetComponent<BallScript>().ballStarted = false;
        //    paddle.joint.autoConfigureConnectedAnchor = true;
        //    paddle.joint.connectedBody = ball.GetComponent<Rigidbody2D>();
        //    ball.GetComponent<Rigidbody2D>().position = new Vector2(ball.transform.position.x, paddle.transform.position.y + 0.45f);
        //}

        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 8)//MultiBall or MainBall
        {
            if(collision.gameObject.layer == 11)
            {
                collision.gameObject.GetComponent<MultiBallScript>().ballStarted = false;
            }
            else
            {
                collision.gameObject.GetComponent<BallScript>().ballStarted = false;
            }

            collision.gameObject.GetComponent<Rigidbody2D>().position = new Vector2(collision.gameObject.transform.position.x, paddle.transform.position.y + 0.40f);
            logic.multiBallJoints.Add(paddle.gameObject.AddComponent<FixedJoint2D>());
            logic.multiBallJoints[logic.multiBallJoints.Count - 1].autoConfigureConnectedAnchor = true;
            logic.multiBallJoints[logic.multiBallJoints.Count - 1].dampingRatio = 1;
            logic.multiBallJoints[logic.multiBallJoints.Count - 1].enableCollision = true;
            logic.multiBallJoints[logic.multiBallJoints.Count - 1].connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
        }

    }
}
