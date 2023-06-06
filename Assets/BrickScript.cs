using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public GameObject spawner;
    public GameObject logic;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        logic = GameObject.FindGameObjectWithTag("Logic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 11) // MainBall or MultiBall
        {
            Destroy(gameObject);
            spawner.GetComponent<BrickSpawnerScript>().brickAmount--;
            logic.GetComponent<LogicScript>().addScore();
        }
    }

}
