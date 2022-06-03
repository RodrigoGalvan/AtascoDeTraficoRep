using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumber : MonoBehaviour
{
    //Si el objeto enfrente del carro del jugador choca con otro carro
    public bool collidedWithEnemy;
    // Start is called before the first frame update
    void Start()
    {
        collidedWithEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Si choca con otro carro entonces la variable ens true
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collidedWithEnemy = true;
        }
    }

    //Si deja de chocar con el carro de enfrente entonces se espera 0.5 segundos y avanza
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        collidedWithEnemy = false;

    }
}
