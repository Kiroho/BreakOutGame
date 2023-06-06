using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooBrickScript : MonoBehaviour
{
    public LogicScript logic;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 11)//Ball || Multiball
        {
            logic.activateGoo();
        }
    }
}
