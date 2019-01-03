using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public ItemDatabase itemDatabase;

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
    
    // COMBAT STUFF  
    public void LoadCombat(){
        Hero.active.SerializeLocation();
        LoadScene("Combat");
    }
    public void ReturnToWorld(){
        LoadScene("World", true);
        
    }

    //LEVEL MANAGEMENT

    // TODO: consider breaking this into seperate functions - load combat, load map etc...
    public void LoadScene(string sceneName, bool deserialize = false)
    {
        StartCoroutine(LoadAsyncScene(sceneName, deserialize));
    }

    IEnumerator LoadAsyncScene(string sceneName, bool deserialize)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if(deserialize){ Hero.active.DeserializeLocation(); }
    }

}
