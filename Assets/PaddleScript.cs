using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public Rigidbody2D body;
    public FixedJoint2D joint;
    public GameObject ball;
    public GameObject deconnector;
    public LogicScript logic;
    public float moveSpeed = 5;
    public float length;

    private void Start()
    {
        length = GetComponent<Renderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.RightArrow))
        {
            body.velocity = new Vector2(moveSpeed, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.velocity = new Vector2(-moveSpeed, 0);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            body.velocity = new Vector2(0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (joint.connectedBody == ball.GetComponent<Rigidbody2D>())
            //{
            //    joint.connectedBody = deconnector.GetComponent<Rigidbody2D>();
            //    ball.GetComponent<BallScript>().startBall();
            //    ball.GetComponent<BallScript>().startVector = new Vector2(0, 5);
            //}

            if (logic.multiBallJoints.Count > 0)
            {
                Debug.Log("BallJoints exist");
                if (logic.multiBallJoints[0].connectedBody.gameObject.layer == 8) //Mainball
                {
                    Debug.Log("BallJoint is Main Ball");
                    //ball.GetComponent<BallScript>().ballStarted = true;
                    ball.GetComponent<BallScript>().startVector = new Vector2(1, 5);
                    ball.GetComponent<BallScript>().startBall();
                }
                else
                {
                    Debug.Log("BallJoint is Multi Ball");
                    //logic.multiBallJoints[0].connectedBody.gameObject.GetComponent<MultiBallScript>().ballStarted = true;
                    logic.multiBallJoints[0].connectedBody.gameObject.GetComponent<MultiBallScript>().startBall();
                }
                Destroy(logic.multiBallJoints[0]);
                logic.multiBallJoints.RemoveAt(0);
                Debug.Log("BallJoints destroyed");
            }

        }


        if (transform.position.x < -8)
        {
            transform.position = new Vector2(-7.89f, transform.position.y);
        }
        if(transform.position.x > 8)
        {
            transform.position = new Vector2(7.89f, transform.position.y);
        }
    }



}
