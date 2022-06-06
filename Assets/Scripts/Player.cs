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
    private GameObject arrow;
    public AudioClip acelerate;
    public AudioClip collisionAudio;
    public AudioClip gameOver;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    private bool turning;
    private bool arrowTurning;
    private Quaternion currentAngle;
    private Quaternion arrowAngle;
    private bool hitOnce;
    private int xDir; //Dirrecion de movimiento en x
    private int yDir; //Dirrecion de movimiento en y
    public TextMeshProUGUI textOnDisplay;
     public GameObject movePoint;
    // Start is called before the first frame update
    void Start()
    {
        //Variables iniciales
        hitOnce = false;
        movepoint.parent = null;
        xDir = -1;
        yDir = 0;
        left = false;
        right = false;
        up = false;
        down = false;
        turning = false;
        arrowTurning = false;

        arrow = transform.GetChild(0).gameObject;
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
            arrowTurning = true;
            arrowAngle = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Le pico a la flecha de la derecha y las variables se vuelven en true o false ");
            right = true;
            up = false;
            down = false;
            left = false;
            arrowTurning = true;
            arrowAngle = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            up = true;
            left = false;
            down = false;
            right = false;
            arrowTurning = true;
            arrowAngle = Quaternion.Euler(0, 0, 270);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            down = true;
            left = false;
            right = false;
            up = false;
            arrowTurning = true;
            arrowAngle = Quaternion.Euler(0, 0, 90);
        }



        if (Input.GetKey(KeyCode.E) && Time.timeScale != 0)
        {
            if (moveSpeed < 8)
            {
                if(this.gameObject.GetComponent<AudioSource>().isPlaying == false){
                    this.gameObject.GetComponent<AudioSource>().Play();
                    gameObject.GetComponent<AudioSource>().volume = 0;
                }
                gameObject.GetComponent<AudioSource>().volume += 0.0001f;
                moveSpeed = moveSpeed + 0.01f;
            }
            else
            {
                gameObject.GetComponent<AudioSource>().volume = 0.05f;
                moveSpeed = 8.0f;
            }
        }

        chageText("Velocity: " + Math.Round(moveSpeed,2).ToString("0.00")) ;

        if (Input.GetKey(KeyCode.Q) && Time.timeScale != 0) {
            if (moveSpeed > 0)
            {
                moveSpeed = moveSpeed - 0.03f;
                gameObject.GetComponent<AudioSource>().volume -= 0.0002f;
            }
            else
            {
                moveSpeed = moveSpeed + 0.0f;
                moveSpeed = 0.0f;
            }
        }

        if (pauseMenu.activeSelf || gameOverMenu.activeSelf) {
            this.gameObject.GetComponent<AudioSource>().Stop();
        }

        
        //Si no ha dado la vuelta

        //Si esta en el tile correcto y si no hay un collider arriba de el
        //Si el siguiente tile al que va el jugador es verdadero entonces tiene esas opciones para moverse
        if (movePoint.GetComponent<PointMoved>().canTurnLeftUp == true)
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

        if (movePoint.GetComponent<PointMoved>().canTurnRightUp == true) 
        {
            Debug.Log("Esta en el tile de RightUp");
            if (right)
            {
                Debug.Log("Esta en el tile de RightUp y la derecha es verdadero");
                turnRight();
            }
            if (up)
            {
                turnUp();
            }
        }

        if (movePoint.GetComponent<PointMoved>().canTurnRightDown == true)
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

        if (arrowTurning)
        {
            arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, arrowAngle, 0.2f);
            if (arrow.transform.rotation == currentAngle)
            {
                arrowTurning = false;
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
            Debug.Log("Esta en el tile de RightUp");
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
        if (collision.CompareTag("Enemy") && hitOnce == false) {
            hitOnce = true;
            this.gameObject.GetComponent<AudioSource>().volume = 1;
            this.gameObject.GetComponent<AudioSource>().Stop();
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(collisionAudio, .5f);
            Time.timeScale = 0;
            GameOverMenu.SetActive(true);
        }
    }

}
