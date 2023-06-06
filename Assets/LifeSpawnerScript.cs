using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpawnerScript : MonoBehaviour
{
    public GameObject life;
    private float lifeSpace = 0.8f;
    private float xPosition;
    private List<GameObject> lifeArray = new List<GameObject>();
    private void Start()
    {
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    decreaseLifes(1);
        //}
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    increaseLifes(1);
        //}
    }

    public void spawnLifes(int amount)
    {

        xPosition = transform.position.x;

        for (int i=0; i<amount; i++)
        {
            lifeArray.Add(Instantiate(life, new Vector3(xPosition, transform.position.y, transform.position.z), life.transform.rotation));
            xPosition -= lifeSpace;
        }
    }

    public void increaseLifes(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            lifeArray.Add(Instantiate(life, new Vector3(xPosition, transform.position.y, transform.position.z), life.transform.rotation));
            xPosition -= lifeSpace;
        }
        
    }

    public void decreaseLifes(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (lifeArray.Count != 0)
                {
                Destroy(lifeArray[lifeArray.Count - 1]);
                lifeArray.RemoveAt(lifeArray.Count - 1);
                xPosition += lifeSpace;
            }
            
        }
        
    }


}
