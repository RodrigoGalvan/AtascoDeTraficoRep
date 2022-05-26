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
    private float spawnerInterval = 3.5f;
    int chance;
    int num;



    // Start is called before the first frame update
    void Start()
    {
        num = 0;
        StartCoroutine(spawnEnemy(spawnerInterval, enemyPrefab, enemyPrefab2));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy, GameObject enemy2)
    {
        chance = Random.Range(1, 3);
        Debug.Log(chance);
        if (chance == 1)
        {
            exclamationRight.SetActive(true);
        }
        else {
            exclamationLeft.SetActive(true);
        }
        yield return new WaitForSeconds(interval);
        if (num < 6)
        {
            num++;
            if (chance == 1)
            {
                GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);

            }
            else {

                GameObject newEnemy = Instantiate(enemy2, transform.position - new Vector3(61f,0,0), Quaternion.identity);
            }
            exclamationLeft.SetActive(false);
            exclamationRight.SetActive(false);
            StartCoroutine(spawnEnemy(interval, enemy, enemy2));
        }
    }
}
