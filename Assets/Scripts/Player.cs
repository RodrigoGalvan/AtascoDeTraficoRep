using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Range(0.5f, 10f)] public float moveSpeed; //Speed para mover el carro
    public Rigidbody2D rig; //RigidBody del carro
    public Transform movepoint; //Mover el carro hacia un punto en frente de el para tener un movimiento conforme al grid
    private bool left; //Vuelta a la izquierda
    private bool right; //Vuelta a la derecha
    private bool down; //Vuelta a hacia abajo
    private bool up; //Vuelta a hacia arriba
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

    }

    // Update is called once per frame
    void Update()
    {
        //De acuerdo al input a que dirrecion va
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            chageText("Siguiente Vuelta: Izquierda");
            left = true;
            right = false;
            up = false;
            down = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            chageText("Siguiente Vuelta: Derecha");
            right = true;
            up = false;
            down = false;
            left = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            chageText("Siguiente Vuelta: Arriba");
            up = true;
            left = false;
            down = false;
            right = false;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            chageText("Siguiente Vuelta: Abajo");
            down = true;
            left = false;
            right = false;
            up = false;
        }
        
        //Si el siguiente tile al que va el jugador es verdadero entonces tiene esas opciones para moverse
        if (movePoint.GetComponent<PointMoved>().canTurnLeftUp == true )
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

        if (movePoint.GetComponent<PointMoved>().canTurnLeftDown == true )
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

        if (movePoint.GetComponent<PointMoved>().canTurnRightUp == true )
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

        if (movePoint.GetComponent<PointMoved>().canTurnRightDown == true )
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
            turnDown();
        }

        if (movePoint.GetComponent<PointMoved>().canTurnRight == true)
        {
            turnRight();

        }

        if (movePoint.GetComponent<PointMoved>().canTurnLeft == true)
        {
            turnLeft();

        }

        if (movePoint.GetComponent<PointMoved>().canTurnUp == true)
        {
            turnUp();
        }

        //Mueve un espacio y cuando la distancia al punto donde tiene programado
        //llegar entonces se programa para ir al siguiente espacio
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            movepoint.position += new Vector3(xDir, yDir, 0f);
        }

        //Mover hacia el espacio definido en cualquier momento a cierta velocidad
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

    }

    private void chageText(string text)
    {
        textOnDisplay.text = text;
    }

    private void turnLeft() {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            xDir = -1;
            yDir = 0;
            left = false;
        }
    }

    private void turnRight() {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            xDir = 1;
            yDir = 0;
            right = false;
        }
    }

    private void turnDown() {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            xDir = 0;
            yDir = -1;
            down = false;
        }
    }

    private void turnUp() {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
            xDir = 0;
            yDir = 1;
            up = false;
        }
    }
}
