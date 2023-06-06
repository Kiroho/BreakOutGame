using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawnerScript : MonoBehaviour
{
    public GameObject brick;
    public GameObject healthBrick;
    public GameObject gooBrick;
    public GameObject enlargenBrick;
    public GameObject doubleBrick;
    public GameObject multiBallBrick;
    public int rows = 5; //1-10
    public int colums = 8; //1-8
    public float spawnHeight = 4.5f;
    private float spawnpointX = 0;
    private int c = 0;
    private Color[] rainbow = { Color.red, Color.blue, Color.blue, Color.magenta, Color.magenta, Color.cyan, Color.cyan, Color.yellow, Color.yellow, Color.green};
    public int brickAmount = 0;


    // Start is called before the first frame update
    void Start()
    {
        float spawnHeightAtStart=spawnHeight;
        spawnBricksInRows(rows, colums, spawnHeight);
    }

    public void spawnBricksInRows(int rows, int colums, float spawnHeight)
    {
        setSpawnpoint();
        for(int i=0; i < rows; i++)
        {
            for(int j=0; j<colums; j++)
            {
                int brickRNG = (int)Random.Range(1.0f, 100.0f);
                if (brickRNG <= 1) //Spawn health Brick 1%
                {
                    GameObject currentBrick = Instantiate(healthBrick, new Vector3(spawnpointX, spawnHeight, 0), healthBrick.transform.rotation);

                }
                else if (brickRNG >= 2 && brickRNG <= 3) //Spawn goo brick 1%
                {
                    Instantiate(gooBrick, new Vector3(spawnpointX, spawnHeight, 0), gooBrick.transform.rotation);
                }
                else if (brickRNG >= 4 && brickRNG <= 5) //Spawn enlargen brick 1%
                {
                    Instantiate(enlargenBrick, new Vector3(spawnpointX, spawnHeight, 0), enlargenBrick.transform.rotation);
                }
                else if (brickRNG >= 6 && brickRNG <= 7) //Spawn double brick 1%
                {
                    Instantiate(doubleBrick, new Vector3(spawnpointX, spawnHeight, 0), doubleBrick.transform.rotation);
                }
                else if (brickRNG >= 10 && brickRNG <= 11) //Spawn multiBall brick 1%
                {
                    Instantiate(multiBallBrick, new Vector3(spawnpointX, spawnHeight, 0), multiBallBrick.transform.rotation);
                }
                else //Spawn normal Brick
                {
                    GameObject currentBrick = Instantiate(brick, new Vector3(spawnpointX, spawnHeight, 0), brick.transform.rotation);
                    currentBrick.GetComponent<SpriteRenderer>().color = rainbow[c];
                }
                
                spawnpointX += brick.GetComponent<SpriteRenderer>().bounds.size.x + 0.0001f;
                brickAmount++;
                
            }
            
            setSpawnpoint();
            spawnHeight = spawnHeight - brick.GetComponent<SpriteRenderer>().bounds.size.y - 0.01f;
            c++;
            if (c >= rainbow.Length)
            {
                c = 0;
            }
        }
    }

    void setSpawnpoint()
    {
        if (colums % 2 == 0)
        {
            spawnpointX = (brick.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f);
        }
        else
        {
            spawnpointX = 0;
        }

        spawnpointX -= ((colums/2) * brick.GetComponent<SpriteRenderer>().bounds.size.x);
    }
}
