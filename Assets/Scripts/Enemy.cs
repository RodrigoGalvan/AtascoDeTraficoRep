using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(1f, 10f)] public float moveSpeed; //Speed para mover el carro
    public Rigidbody2D rig; //RigidBody del carro
    public Transform movepoint; //Mover el carro hacia un punto en frente de el para tener un movimiento conforme al grid
    private bool left; //Vuelta a la izquierda
    private bool right; //Vuelta a la derecha
    private bool down; //Vuelta a hacia abajo
    private bool up; //Vuelta a hacia arriba
    private bool turning; //Si el carro esta volteando
    private Quaternion currentAngle; //Cuanto tiene que voltear el carro
    public bool startMovingLeftOrRight; //Si el carro spawnea de la derecha o la izquierda
    private int xDir; //Dirrecion de movimiento en x
    private int yDir; //Dirrecion de movimiento en y
    public GameObject movePoint; //Punto a donde se dirige el carro
    public GameObject bumper; //El objeto que detecta si hay colision con otro carro

    // Start is called before the first frame update
    void Start()
    {
        //Variables iniciales
        movepoint.parent = null;

        //Si el carro spawnea de la izquierda entonces voltear lo y darle una direccion diferente a que si viene de la derecha
        if (startMovingLeftOrRight == true)
        {
            xDir = 1;
            currentAngle = Quaternion.Euler(0, 0, 180);
            turning = true;
        }
        else
        {
            //Si viene de izquierda entonces se queda igual el sprite y se dirige hacia la izquierda
            xDir = -1;
        }
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
        if (movePoint.GetComponent<PointMoved>().num == 1)
        {
            left = true;
            right = true;
            up = false;
            down = false;
        }

        if (movePoint.GetComponent<PointMoved>().num == 2)
        {
            up = true;
            left = false;
            down = true;
            right = false;
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




        //Aceleracion y frenar del carro del enemigo. Si detecta una colision con un enemigo o si esta en lugar de parada frena
        if (movePoint.GetComponent<PointMoved>().stop == true || bumper.GetComponent<Bumber>().collidedWithEnemy == true)
        {
            if (moveSpeed > 0)
            {
                moveSpeed = moveSpeed - 0.2f;
            }
            else
            {
                moveSpeed = 0f;
            }
        }
        else
        {
            //Si no esta siendo detenido entonces acelera
            if (moveSpeed < 4)
            {
                moveSpeed = moveSpeed + 0.05f;
            }
            else
            {
                moveSpeed = 4f;
            }
        }

        //Mueve un espacio y cuando llega al siguiente espacio programa para ir al siguiente
        //Mueve un espacio y cuando la distancia al punto donde tiene programado
        //llegar entonces se programa para ir al siguiente espacio
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            //Si no esta en un semaforo entonces siguie cambiando el punto para que el carro se mueva hacia adelante
            if (movePoint.GetComponent<PointMoved>().stop == false)
            {
                movepoint.position += new Vector3(xDir, yDir, 0f);

            }
        }

        //Mover hacia el siguiente espacio
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        //El carro se voltea con cierta velocidad para que se vea la vuelta mas organica
        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, currentAngle, 0.2f);
            if (transform.rotation == currentAngle)
            {
                turning = false;
            }
        }
    }



    //Metodos para hacer las vueltas con sus direcciones correctas y cuanto se tiene que rotar el sprite

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

    private void turnLeft()
    {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            movePoint.GetComponent<PointMoved>().turning = true;
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 0);
            xDir = -1;
            yDir = 0;
            left = false;
        }
    }

    private void turnRight()
    {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            movePoint.GetComponent<PointMoved>().turning = true;
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 180);
            xDir = 1;
            yDir = 0;
            right = false;
        }
    }

    private void turnDown()
    {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            movePoint.GetComponent<PointMoved>().turning = true;
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 90);
            xDir = 0;
            yDir = -1;
            down = false;
        }
    }

    private void turnUp()
    {
        if (Vector3.Distance(transform.position, movepoint.position) == 0f)
        {
            movePoint.GetComponent<PointMoved>().turning = true;
            turning = true;
            currentAngle = Quaternion.Euler(0, 0, 270);
            xDir = 0;
            yDir = 1;
            up = false;
        }
    }
}
