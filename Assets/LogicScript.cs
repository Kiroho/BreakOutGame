using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public int score = 0;
    private int pointsPerBrick = 100;
    public int lifes = 3;
    public GameObject ball;
    public GameObject paddle;
    public GameObject brickSpawner;
    public LifeSpawnerScript lifeSpawner;
    private float checkRate = 2;
    private float timer = 0;
    public TMP_Text scoreText;
    public GameObject gameOverMenu;
    public GameObject goo;
    public GameObject doubleGoo;
    public GameObject doublePaddle;
    public List<FixedJoint2D> multiBallJoints = new List<FixedJoint2D>();
    bool gooActive = false;
    private float gooTimer = 0;
    public float gooDuration = 15;
    bool enlargenActive = false;
    private float enlargenTimer = 0;
    public float enlargenDuration = 15;
    bool doubleActive = false;
    private float doubleTimer = 0;
    public float doubleDuration = 15;
    public GameObject pauseBtn;
    public GameObject pauseScreen;
    public AudioSource gameOverSound;


    private void Start()
    {
        Physics2D.IgnoreLayerCollision(11, 11);//Multiball - MultiBall
        Physics2D.IgnoreLayerCollision(8, 11);//MainBall - MultiBall
        Physics2D.IgnoreLayerCollision(9, 13);//Paddle - Goo
        lifeSpawner.spawnLifes(lifes);
        scoreText.text = "Score: 0";
        Time.timeScale = 1;
        attachBallToPaddle();
    }

    private void Update()
    {
        //Respawn Bricks
        if (timer < checkRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (brickSpawner.GetComponent<BrickSpawnerScript>().brickAmount == 0 && ball.transform.position.y <=-3.5f)
            {
                brickSpawner.GetComponent<BrickSpawnerScript>().spawnBricksInRows(brickSpawner.GetComponent<BrickSpawnerScript>().rows, brickSpawner.GetComponent<BrickSpawnerScript>().colums, brickSpawner.GetComponent<BrickSpawnerScript>().spawnHeight+0);
                timer = 0;
                pointsPerBrick += 20;
                if (ball.GetComponent<BallScript>().speed < 25)
                {
                    ball.GetComponent<BallScript>().speed ++;
                }
                if (paddle.GetComponent<PaddleScript>().moveSpeed < 50)
                {
                    paddle.GetComponent<PaddleScript>().moveSpeed++;
                }
                
            }
        }

        //Goo Duration
        if (gooActive)
        {
            if (gooTimer < gooDuration)
            {
                gooTimer += Time.deltaTime;
            }
            else
            {
                goo.SetActive(false);
                gooActive = false;
                doublePaddle.transform.GetChild(1).gameObject.SetActive(false);
                gooTimer = 0;
                
            }
        }

        //Enlargen Duration
        if (enlargenActive)
        {
            if (enlargenTimer < enlargenDuration)
            {
                enlargenTimer += Time.deltaTime;
            }
            else
            {
                paddle.transform.localScale = new Vector3(1, 1, 1);
                enlargenActive = false;
                enlargenTimer = 0;
            }
        }

        //Double Duration
        if (doubleActive)
        {
            if (doubleTimer < doubleDuration)
            {
                doubleTimer += Time.deltaTime;
            }
            else
            {
                doublePaddle.SetActive(false);
                doublePaddle.transform.GetChild(1).gameObject.SetActive(false);
                doubleActive = false;
                doubleTimer = 0;
                if (ball.GetComponent<BallScript>().ballStarted == false) // Set gooed MainBall from DoublePaddle to Paddle when DoublePaddle ends
                {
                    paddle.GetComponent<PaddleScript>().joint.connectedBody = paddle.GetComponent<PaddleScript>().deconnector.GetComponent<Rigidbody2D>();
                    ball.GetComponent<Rigidbody2D>().position = new Vector2(paddle.transform.position.x, paddle.transform.position.y + 0.45f);
                    paddle.GetComponent<PaddleScript>().joint.connectedBody = ball.GetComponent<Rigidbody2D>();
                }
                for (int i = 0; i < multiBallJoints.Count; i++)
                {
                    multiBallJoints[i].connectedBody.transform.position = new Vector2(paddle.transform.position.x, paddle.transform.position.y + 0.45f);
                }
            }
        }

    }


    public void activateGoo()
    {
        goo.SetActive(true);
        gooTimer = 0;
        gooActive = true;
        if (doubleActive)
        {
            doublePaddle.transform.GetChild(1).gameObject.SetActive(true);
        }

    }
    public void activateEnlargen()
    {
        paddle.transform.localScale = new Vector3(1.5f, 1, 1);
        enlargenTimer = 0;
        enlargenActive = true;
    }
    public void activateDoublePaddle()
    {
        doublePaddle.SetActive(true);
        doubleTimer = 0;
        doubleActive = true;
        if (gooActive)
        {
            doublePaddle.transform.GetChild(1).gameObject.SetActive(true);
        }
    }


    public void attachBallToPaddle()
    {
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        ball.GetComponent<BallScript>().ballStarted = false;
        ball.GetComponent<Rigidbody2D>().position = new Vector2(paddle.transform.position.x, paddle.transform.position.y + 0.55f);
        multiBallJoints.Add(paddle.gameObject.AddComponent<FixedJoint2D>());
        multiBallJoints[multiBallJoints.Count - 1].autoConfigureConnectedAnchor = false;
        multiBallJoints[multiBallJoints.Count - 1].dampingRatio = 1;
        multiBallJoints[multiBallJoints.Count - 1].enableCollision = true;
        multiBallJoints[multiBallJoints.Count - 1].connectedAnchor = new Vector2(0, -0.55f);
        ball.GetComponent<Rigidbody2D>().position = new Vector2(paddle.transform.position.x, paddle.transform.position.y + 0.55f);
        multiBallJoints[multiBallJoints.Count - 1].connectedBody = ball.GetComponent<Rigidbody2D>();
    }



    public void addScore()
    {
        score+=pointsPerBrick;
        scoreText.text = "Score: " + score.ToString();
    }

    public void decreaseLife()
    {
        lifes--;
        lifeSpawner.decreaseLifes(1);
        if (lifes <= 0)
        {
            gameOver();
        }
    }

    public void increaseLife()
    {
        if (lifes < 15)
        {
            lifes++;
            lifeSpawner.increaseLifes(1);
        }
        
    }

    void gameOver()
    {
        gameOverSound.Play();
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        pauseScreen.SetActive(true);
    }

}
