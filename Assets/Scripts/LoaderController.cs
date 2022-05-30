using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoaderController : MonoBehaviour
{
    public TextMeshProUGUI textLoader;
    public string textInLoader;
    public string NameScene;

    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartToLoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartToLoadGame()
    {
        StartCoroutine(GameLoadRoutine());
        StartCoroutine(LoadingTextRoutine());
    }

    private IEnumerator GameLoadRoutine()
    {
        AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(NameScene);
        op.allowSceneActivation = false;
        yield return new WaitForSeconds(2f);
        op.allowSceneActivation = true;
    }

    private IEnumerator LoadingTextRoutine()
    {
        yield return new WaitForSeconds(0.25f);
        for(int i = 0; i < counter % 4; i++)
        {
            textLoader.text = textLoader.text + ".";
        }
        counter++;
        StartCoroutine(LoadingTextRoutine());
    }
}
