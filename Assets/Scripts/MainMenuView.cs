using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private AudioClip audioClip;


    public int time;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    //Pauzaar juego activa la pantalla de pausa y cambia el tiempo a 0
    public void PauseGame()
    {
        this.gameObject.GetComponent<AudioSource>().Stop();
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(audioClip, 1f);
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
}
