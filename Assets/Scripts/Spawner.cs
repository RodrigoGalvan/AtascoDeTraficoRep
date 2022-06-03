using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyPrefab2;
    [SerializeField]
    private GameObject exclamationLeft;
    [SerializeField]
    private GameObject exclamationRight;

    [SerializeField]
    private float spawnerInterval = 2f;
    int chance;
    int num;



    // Start is called before the first frame update
    void Start()
    {
        num = 0;
        //Empieza a spawnear
        StartCoroutine(spawnEnemy(spawnerInterval, enemyPrefab, enemyPrefab2));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy, GameObject enemy2)
    {
        if (num < 25)
            //Hay dos spawners entonces se escoge random entre los dos
            chance = Random.Range(1, 3);
        //Se le avisa al jugador cuando va a aparecer un carro de uno de los lados
        if (chance == 1)
        {
            exclamationRight.SetActive(true);
        }
        else
        {
            exclamationLeft.SetActive(true);
        }

        //Se espera para que spawneen los carros
        yield return new WaitForSeconds(interval);

        //Cantidad de carros a que salgan

        {
            num++;
            //Dependiendo de cual fue el numero random es por donde salen
            if (chance == 1)
            {
                GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);

            }
            else
            {

                GameObject newEnemy = Instantiate(enemy2, transform.position - new Vector3(61f, 0, 0), Quaternion.identity);
            }
            //Desactivar signos de exclamacion porque ya salieron los carros
            exclamationLeft.SetActive(false);
            exclamationRight.SetActive(false);
            StartCoroutine(spawnEnemy(interval, enemy, enemy2));
        }
    }
}
