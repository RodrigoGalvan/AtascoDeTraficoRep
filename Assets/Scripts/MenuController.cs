using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public string NameScene;
    private bool isLoading = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (!isLoading)
        {
            isLoading = true;
            StartCoroutine(GameLoadRoutine());
        }
        
    }

    private IEnumerator GameLoadRoutine()
    {
        AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(NameScene);
        op.allowSceneActivation = false;
        yield return new WaitForSeconds(0.5f);
        op.allowSceneActivation = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
