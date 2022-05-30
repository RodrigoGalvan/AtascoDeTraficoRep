using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Exit()
    {
        if (!isLoading)
        {
            isLoading = true;
            Time.timeScale = 1;
            AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Menu");
            op.allowSceneActivation = true;
        }
    }
}
