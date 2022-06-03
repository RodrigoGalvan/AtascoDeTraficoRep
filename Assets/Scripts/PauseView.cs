using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseView : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    private bool isLoading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Boton para continuar regresa el tiempo a 1 y desactiva menu de pausa
    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    //Reinicia la escena y cambia el tiempo a 1
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    //Se sale del juego al menu.
    public void Exit()
    {
        //Solo se puede picar al boton una vez
        if (!isLoading)
        {
            Destroy(GameObject.Find("PlayerInfo"));
            isLoading = true;
            Time.timeScale = 1;
            AsyncOperation op = SceneManager.LoadSceneAsync("Menu");
            op.allowSceneActivation = true;
        }
    }
}
