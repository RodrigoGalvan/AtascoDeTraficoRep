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
    private GameObject currentSpot;
    // Start is called before the first frame update
    void Start()
    {
        pointSpotAmount = transform.childCount;
        currentSpot = transform.GetChild(Random.Range(0, pointSpotAmount)).gameObject;
        currentSpot.SetActive(true);
    }

    public void pointGet()
    {
        int rand;
        score++;
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
