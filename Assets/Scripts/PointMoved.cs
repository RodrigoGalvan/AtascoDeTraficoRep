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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

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
        };
    }


}
