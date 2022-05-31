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

    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }


    public void Exit()
    {
        if (!isLoading)
        {
            isLoading = true;
            Time.timeScale = 1;
            AsyncOperation op = SceneManager.LoadSceneAsync("Menu");
            op.allowSceneActivation = true;
        }
    }
}
