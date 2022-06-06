using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMoved : MonoBehaviour
{

    public bool canTurn;
    public bool canTurnRightDown;
    public bool canTurnRightUp;
    public bool canTurnLeftDown;
    public bool canTurnLeftUp;
    public bool canTurnRight;
    public bool canTurnDown;
    public bool canTurnLeft;
    public bool canTurnUp;
    public bool turning;
    public int num;
    public bool stop;
    public bool enemyInFront;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //De acuerdo con que colisiona el punto hacia donde se mueve el carro es que valor de true o false tiene cada variable
    private void OnTriggerStay2D(Collider2D collision)
    {
        num = Random.Range(1, 3);
        switch (collision.tag)
        {
            case "LeftUp":
                canTurnLeftUp = true;
                break;
            case "LeftDown":
                canTurnLeftDown = true;
                break;
            case "RightUp":
                canTurnRightUp = true;
                break;

            case "RightDown":
                canTurnRightDown = true;
                break;
            case "StraightDown":
                canTurnDown = true;
                break;

            case "StraightLeft":
                canTurnLeft = true;
                break;
            case "StraightRight":
                canTurnRight = true;
                break;

            case "StraightUp":
                canTurnUp = true;
                break;
            case "Crossing":
                turning = false;
                break;
            case "TrafficStop":
                stop = true;
                break;
            case "Enemy":
                enemyInFront = true;
                break;

        };

    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "LeftUp":
                canTurnLeftUp = false;
                break;

            case "LeftDown":
                canTurnLeftDown = false;
                break;
            case "RightUp":
                canTurnRightUp = false;
                break;

            case "RightDown":
                canTurnRightDown = false;
                break;
            case "StraightDown":
                canTurnDown = false;
                break;

            case "StraightLeft":
                canTurnLeft = false;
                break;
            case "StraightRight":
                canTurnRight = false;
                break;

            case "StraightUp":
                canTurnUp = false;
                break;
            case "TrafficStop":
                stop = false;
                break;
            case "Enemy":
                enemyInFront = false;
                break;
        };
    }


}
