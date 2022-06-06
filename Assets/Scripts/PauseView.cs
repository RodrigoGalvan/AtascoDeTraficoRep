using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseView : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    private bool isLoading = false;
    [SerializeField]
    private AudioClip audioClip;
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
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    //Reinicia la escena y cambia el tiempo a 1
    public void Restart()
    {
        Time.timeScale = 1;
        this.gameObject.GetComponent<AudioSource>().volume = 1;
        this.gameObject.GetComponent<AudioSource>().Stop();
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(audioClip, 1f);
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitWhile(() => this.gameObject.GetComponent<AudioSource>().isPlaying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    //Se sale del juego al menu.
    public void Exit()
    {
        Time.timeScale = 1;
        //Solo se puede picar al boton una vez
        if (!isLoading)
        {
            Destroy(GameObject.Find("PlayerInfo"));
            isLoading = true;
            AsyncOperation op = SceneManager.LoadSceneAsync("Menu");
            op.allowSceneActivation = true;
        }
    }
}
