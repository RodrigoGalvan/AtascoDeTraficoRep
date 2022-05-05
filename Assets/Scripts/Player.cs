using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1f, 10f)] public int moveSpeed; //Speed para mover el carro
    public Rigidbody2D rig; //RigidBody del carro
    public Transform movepoint; //Mover el carro hacia un punto en frente de el para tener un movimiento conforme al grid
    private bool left; //Vuelta a la izquierda
    private bool right; //Vuelta a la derecha
    private bool down; //Vuelta a hacia abajo
    private bool up; //Vuelta a hacia arriba
    private int xDir; //Dirrecion de movimiento en x
    private int yDir; //Dirrecion de movimiento en y
    private bool leftDown; //Tile que permite ir hacia la izquierda o abajo
    private bool rightUp; //Tile que permite ir hacia la derecha o arriba
    private bool rightDown; //Tile que permite ir hacia la derecha o abajo
    private bool leftUp; //Tile que permite ir hacia la izquierda o arriba
    private RaycastHit2D hit; //Objeto de raycast
    private RaycastHit2D hit2; //Objeto de raycast
    bool turning; //Si ya dio la vuelta el carro
    public TextMeshProUGUI textOnDisplay;
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

        //Si no ha dado la vuelta
        if (turning == false)
        {
            //Si esta en el tile correcto y si no hay un collider arriba de el
            if (leftUp == true && isFree())
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

            if (leftDown == true && isFree())
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

            if (rightUp == true && isFree())
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

            if (rightDown == true && isFree())
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
        }

        //Mueve un espacio y cuando llega al siguiente espacio programa para ir al siguiente
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            movepoint.position += new Vector3(xDir, yDir, 0f);
        }

        //Mover hacia el siguiente espacio
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //En que tile esta

        switch (collision.tag) {
            case "Crossing":
                turning = true;
                break;
            case "LeftUp":
                leftUp = true;
                break;
            case "LeftDown":
                leftDown = true;
                break;
            case "RightUp":
                rightUp = true;
                break;
            case "RightDown":
                rightDown = true;
                break;
        };
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //De que tile acaba de salir
        if (collision.tag == "Crossing")
        {
            turning = false;
        }
        leftDown = false;
        leftUp = false;
        rightUp = false;
        rightDown = false;

    }

    private void chageText(string text)
    {
        textOnDisplay.text = text;
    }

    private void turnLeft() {
        turning = true;
        transform.eulerAngles = new Vector3(0, 0, 0);
        xDir = -1;
        yDir = 0;
        left = false;
    }

    private void turnRight() {
        turning = true;
        transform.eulerAngles = new Vector3(0, 0, 180);
        xDir = 1;
        yDir = 0;
        right = false;
    }

    private void turnDown() {
        turning = true;
        transform.eulerAngles = new Vector3(0, 0, 90);
        xDir = 0;
        yDir = -1;
        down = false;
    }

    private void turnUp() {
        turning = true;
        transform.eulerAngles = new Vector3(0, 0, 270);
        xDir = 0;
        yDir = 1;
        up = false;
    }

    private bool isFree()
    {
        //Raycast dependiendo de la rotacion del carro

        switch (transform.rotation.z) {
            case 0:
                hit = Physics2D.Raycast(transform.position + new Vector3(0, 1f, 0), transform.up, 0.2f);
                hit2 = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 1f, 0), transform.up, 0.2f);
                break;
            case 90:
                hit = Physics2D.Raycast(transform.position + new Vector3(1, 0f, 0), transform.up, 0.2f);
                hit2 = Physics2D.Raycast(transform.position + new Vector3(1f, -0.5f, 0), transform.up, 0.2f);
                break;
            case 180:
                hit = Physics2D.Raycast(transform.position + new Vector3(0, -1f, 0), transform.up, 0.2f);
                hit2 = Physics2D.Raycast(transform.position + new Vector3(0.5f, 1f, 0), transform.up, 0.2f);
                break;
            case 270:
                hit = Physics2D.Raycast(transform.position + new Vector3(-1, 0f, 0), transform.up, 0.2f);
                hit2 = Physics2D.Raycast(transform.position + new Vector3(-1f, 0.5f, 0), transform.up, 0.2f);
                break;
        };

        //Checar si hay collider arriba del carro lo cual significa que no puede dar la vuelta
        if (hit.collider != null) {
            return hit.collider.tag == "Crossing" && hit2.collider.tag == "Crossing";
        }
        return hit.collider == null && hit2.collider == null; 
    }
}
