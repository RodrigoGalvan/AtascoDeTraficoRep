using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private int pointSpotAmount; 
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    private GameObject currentSpot;
    public AudioSource audioSource;
    public AudioClip getPackage;
    public GameObject GameOverMenu;
    public AudioClip gameOver;
    float counter;
    bool once;
    // Start is called before the first frame update
    void Start()
    {
        pointSpotAmount = transform.childCount;
        currentSpot = transform.GetChild(Random.Range(0, pointSpotAmount)).gameObject;
        currentSpot.SetActive(true);
        once = false;
        counter = 20.00f;
        timerText.text = System.Math.Round(counter, 2).ToString("0.00");
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (counter <= 0 && once == false) {
            Time.timeScale = 0;
            GameOverMenu.SetActive(true);
        }

        if (GameOverMenu.activeSelf && once == false) {
            once = true;
            audioSource.Stop();
            audioSource.PlayOneShot(gameOver,1f);
            if (GameObject.Find("PlayerInfo") != null)
            {
                GameObject.Find("PlayerInfo").GetComponent<Login>().PostData(score);
            }
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(.01f);
        counter = counter - 0.01f;
        timerText.text = System.Math.Round(counter, 2).ToString("0.00");
;
        StartCoroutine(Timer());
    }

    public void pointGet()
    {
        int rand;
        score++;
        counter = 20.00f;
        timerText.text = System.Math.Round(counter, 2).ToString("0.00");
        audioSource.PlayOneShot(getPackage,1f);
        scoreText.text = score.ToString();
        currentSpot.SetActive(false);
        rand = (Random.Range(0, pointSpotAmount));
        do
        {
            currentSpot = transform.GetChild(rand).gameObject;
        }
        while (currentSpot == transform.GetChild(rand));

        currentSpot.SetActive(true);
        currentSpot.GetComponent<PointSpot>().triggered = false;
    }
}
