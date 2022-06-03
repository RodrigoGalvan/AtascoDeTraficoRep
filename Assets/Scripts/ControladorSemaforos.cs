using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSemaforos : MonoBehaviour
{
    //Lo mismo que el semaforo de 3
    [SerializeField]
    public GameObject sem1, sem2, sem3, sem4;
    // Start is called before the first frame update
    void Start()
    {
        float randWaitLarge = Random.Range(3.0f, 5.0f);
        float randWaitSmall = Random.Range(1.0f, 1.5f);

        StartCoroutine(changeLights(randWaitLarge, randWaitSmall));
    }

    private IEnumerator changeLights(float bigWait, float smallWait) {
        Green(sem1);
        Red(sem2);
        Red(sem3);
        Red(sem4);
        yield return new WaitForSeconds(bigWait);
        Yellow(sem1);
        yield return new WaitForSeconds(smallWait);
        Green(sem2);
        Red(sem1);
        Red(sem3);
        Red(sem4);
        yield return new WaitForSeconds(bigWait);
        Yellow(sem2);
        yield return new WaitForSeconds(smallWait);
        Green(sem3);
        Red(sem1);
        Red(sem2);
        Red(sem4);
        yield return new WaitForSeconds(bigWait);
        Yellow(sem3);
        yield return new WaitForSeconds(smallWait);
        Green(sem4);
        Red(sem1);
        Red(sem2);
        Red(sem3);
        yield return new WaitForSeconds(bigWait);
        Yellow(sem4);
        yield return new WaitForSeconds(smallWait);


        StartCoroutine(changeLights(bigWait,smallWait));
    }

    public void Green(GameObject obj) {
        obj.transform.GetChild(0).gameObject.SetActive(true);
        obj.transform.GetChild(1).gameObject.SetActive(false);
        obj.transform.GetChild(2).gameObject.SetActive(false);
        obj.transform.GetChild(3).gameObject.SetActive(false);

    }

    public void Red(GameObject obj)
    {
        obj.transform.GetChild(0).gameObject.SetActive(false);
        obj.transform.GetChild(1).gameObject.SetActive(true);
        obj.transform.GetChild(2).gameObject.SetActive(false);
        obj.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void Yellow(GameObject obj)
    {
        obj.transform.GetChild(0).gameObject.SetActive(false);
        obj.transform.GetChild(1).gameObject.SetActive(false);
        obj.transform.GetChild(2).gameObject.SetActive(true);
        obj.transform.GetChild(3).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
