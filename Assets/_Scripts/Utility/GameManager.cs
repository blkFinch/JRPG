using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject heroDisplay;

    public static GameManager gm;
    private void Awake()
    {
        if (gm != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
            gm = this;
        }

        DontDestroyOnLoad(gm.gameObject);
    }

    //TODO: consider renaming as "NameHero"
    public void CreateNewHero(string name)
    {
        Hero.active.data.Name = name;
    }


    // TODO: consider breaking this into seperate functions - load combat, load map etc...
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
