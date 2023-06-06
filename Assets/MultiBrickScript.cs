using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBrickScript : MonoBehaviour
{
    public LogicScript logic;
    public GameObject multiBall;
    bool gotTriggered = false;

    public Vector2 startVector;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 11)//Ball || Multiball
        {
            if (gotTriggered == false)
            {
                gotTriggered = true;
                Instantiate(multiBall, new Vector3(collision.GetContact(0).point.x + 0.3f, collision.GetContact(0).point.y - 0.3f, 0), multiBall.transform.rotation);
            }
            
            
        }
    }
}
