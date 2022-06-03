using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSemaforesDe3 : MonoBehaviour
{
    [SerializeField]
    public GameObject Sem1, Sem2, Sem3;    //Semaforos
    // Start is called before the first frame update
    void Start()
    {
        //Se escoge un timer random para los semaforos
        float randWaitLarge = Random.Range(3.0f, 5.0f);
        float randWaitSmall = Random.Range(1.0f, 1.5f);
        StartCoroutine(changeLights(randWaitLarge, randWaitSmall));
    }

    private IEnumerator changeLights(float bigWait, float smallWait)
    {
        //Se cambian los semaforos entre verde, rojo y amarillo
        Green(Sem1);
        Red(Sem2);
        Red(Sem3);
        yield return new WaitForSeconds(bigWait);
        Yellow(Sem1);
        yield return new WaitForSeconds(smallWait);
        Green(Sem2);
        Red(Sem1);
        Red(Sem3);
        yield return new WaitForSeconds(bigWait);
        Yellow(Sem2);
        yield return new WaitForSeconds(smallWait);
        Green(Sem3);
        Red(Sem1);
        Red(Sem2);
        yield return new WaitForSeconds(bigWait);
        Yellow(Sem3);
        yield return new WaitForSeconds(smallWait);

        //Vlueve a empezar el metodo infinitamente
        StartCoroutine(changeLights(bigWait, smallWait));
    }

    //Cambiar semaforo a verde, desactivar otras luces y hacer el collider que avisa al carro que esta en rojo falso
    public void Green(GameObject obj)
    {
        obj.transform.GetChild(0).gameObject.SetActive(true);
        obj.transform.GetChild(1).gameObject.SetActive(false);
        obj.transform.GetChild(2).gameObject.SetActive(false);
        obj.transform.GetChild(3).gameObject.SetActive(false);


    }

    //Cambiar semaforo a rojo, desactivar otras luces
    public void Red(GameObject obj)
    {
        obj.transform.GetChild(0).gameObject.SetActive(false);
        obj.transform.GetChild(1).gameObject.SetActive(true);
        obj.transform.GetChild(2).gameObject.SetActive(false);
    }

    //Cambiar semaforo a rojo, desactivar otras luces y hacer el collider que avisa al carro que esta en rojo verdadero
    public void Yellow(GameObject obj)
    {
        obj.transform.GetChild(0).gameObject.SetActive(false);
        obj.transform.GetChild(1).gameObject.SetActive(false);
        obj.transform.GetChild(2).gameObject.SetActive(true);
        obj.transform.GetChild(3).gameObject.SetActive(true);
    }


}
