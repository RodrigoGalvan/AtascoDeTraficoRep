using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(0f, 10f)] public float moveSpeed; //Speed para mover el carro
    public Rigidbody2D rig; //RigidBody del carro
    public Transform movepoint; //Mover el carro hacia un punto en frente de el para tener un movimiento conforme al grid
    private bool left; //Vuelta a la izquierda
    private bool right; //Vuelta a la derecha
    private bool down; //Vuelta a hacia abajo
    private bool up; //Vuelta a hacia arriba
    public GameObject GameOverMenu;

    private bool turning;
    private Quaternion currentAngle;

    private int xDir; //Dirrecion de movimiento en x
    private int yDir; //Dirrecion de movimiento en y
    public TextMeshProUGUI textOnDisplay;
     public GameObject movePoint;
    // Start is called before the first frame update
    void Start()
    {
        //Variables iniciales
        movepoint.parent = null;
        xDir = -1;
        yDir = 0;
        left = false;
        right = false;
        up = false;
        down = false;
        turning = false;

    }

    

    // Update is called once per frame
    void Update()
    {
        //De acuerdo al input a que dirrecion va
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            left = true;
            right = false;
            up = false;
            down = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            right = true;
            up = false;
            down = false;
            left = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            up = true;
            left = false;
            down = false;
            right = false;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            down = true;
            left = false;
            right = false;
            up = false;
        }

        

        if (Input.GetKey(KeyCode.E))
        {
            if (moveSpeed < 8)
            {
                moveSpeed = moveSpeed + 0.01f;
            }
            else
            {
                moveSpeed = 8.0f;
            }
        }

        chageText("Velocidad: " + Math.Round(moveSpeed,2).ToString("0.00")) ;

        if (Input.GetKey(KeyCode.Q)) {
            if (moveSpeed > 0)
            {
                moveSpeed = moveSpeed - 0.03f;
            }
            else
            {
                moveSpeed = 0.0f;
            }
        }
        
 
        
        //Si no ha dado la vuelta

        //Si esta en el tile correcto y si no hay un collider arriba de el
        //Si el siguiente tile al que va el jugador es verdadero entonces tiene esas opciones para moverse
        if (movePoint.GetComponent<PointMoved>().canTurnLeftUp == true && movePoint.GetComponent<PointMoved>().turning == false)
        {
            //Si es derecha o arriba
            if (left)
            {
                turnLeft();
            }
            if (up)
            {
                turnUp();
            }
        }

        if (movePoint.GetComponent<PointMoved>().canTurnLeftDown == true && movePoint.GetComponent<PointMoved>().turning == false)
        {
            if (left)
            {
                turnLeft();
            }
            if (down)
            {
                turnDown();
            }

        }

        if (movePoint.GetComponent<PointMoved>().canTurnRightUp == true && movePoint.GetComponent<PointMoved>().turning == false)
        {
            if (right)
            {
                turnRight();
            }
            if (up)
            {
                turnUp();
            }
        }

        if (movePoint.GetComponent<PointMoved>().canTurnRightDown == true && movePoint.GetComponent<PointMoved>().turning == false)
        {
            if (right)
            {
                turnRight();
            }
            if (down)
            {
                turnDown();
            }
        }


        if (movePoint.GetComponent<PointMoved>().canTurnDown == true)
        {
            turnStraightDown();
        }

        if (movePoint.GetComponent<PointMoved>().canTurnRight == true)
        {
            turnStraightRight();

        }

        if (movePoint.GetComponent<PointMoved>().canTurnLeft == true)
        {
            turnStraightLeft();

        }

        if (movePoint.GetComponent<PointMoved>().canTurnUp == true)
        {
            turnStraightUp();
        }


        //Mueve un espacio y cuando llega al siguiente espacio programa para ir al siguiente
        //Mueve un espacio y cuando la distancia al punto donde tiene programado
        //llegar entonces se programa para ir al siguiente espacio
        if (Vector3.Distance(transform.position, movepoint.position) == 0f )
        {
             movepoint.position += new Vector3(xDir, yDir, 0f);
        }

        //Mover hacia el siguiente espacio
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, currentAngle, 0.2f);
            if (transform.rotation == currentAngle)
            {
                turning = false;
            }
        }
    }

    private void turnStraightUp()
    {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 270);
            xDir = 0;
            yDir = 1;
            up = false;
        }
    }

    private void turnStraightLeft()
    {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 0);
            xDir = -1;
            yDir = 0;
            left = false;
        }
    }

    private void turnStraightRight()
    {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 180);
            xDir = 1;
            yDir = 0;
            right = false;
        }
    }

    private void turnStraightDown()
    {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 90);
            xDir = 0;
            yDir = -1;
            down = false;
        }
    }

    private void chageText(string text)
    {
        textOnDisplay.text = text;
    }

    private void turnLeft() {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f )
        {
            movePoint.GetComponent<PointMoved>().turning = true;
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 0);
            xDir = -1;
            yDir = 0;
            left = false;
        }
    }

    private void turnRight() {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f )
        {
            movePoint.GetComponent<PointMoved>().turning = true;
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 180);
            xDir = 1;
            yDir = 0;
            right = false;
        }
    }

    private void turnDown() {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f )
        {
            movePoint.GetComponent<PointMoved>().turning = true;
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 90);
            xDir = 0;
            yDir = -1;
            down = false;
        }
    }

    private void turnUp() {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f )
        {
            movePoint.GetComponent<PointMoved>().turning = true;
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 270);
            xDir = 0;
            yDir = 1;
            up = false;
        }
    }

    //Cuando hace colision entonces manda datos a base de datos     
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) {
            Time.timeScale = 0;
            GameOverMenu.SetActive(true);
            if (GameObject.Find("PlayerInfo") != null) {
                GameObject.Find("PlayerInfo").GetComponent<Login>().PostData();
            }
        }
    }

}
