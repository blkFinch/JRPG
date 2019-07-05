using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public ItemDatabase itemDatabase;
    public string activeSceneName;


    public static GameManager gm;
    private void Awake() {
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

    //subscriptions
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoadEvent;
        Debug.Log("GM ENABLED");
    }

    //unsubscribe
    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoadEvent;
    }


    // COMBAT STUFF  

    public void LoadCombat() {
        Hero.active.SerializeLocation();
        LoadScene("Combat");
    }

    public void ReturnToWorld() {
        LoadScene("World", true);

    }

    //LEVEL MANAGEMENT
    //

    //Pass true as second parameter to load hero at serialized loc
    public void LoadScene(string sceneName, bool deserialize = false) {

        StartCoroutine(LoadAsyncScene(sceneName, deserialize));
    }

    public void LoadSavedScene(){
        //Gets the saved scene from the hero's save data
        string name = Hero.active.data.SavedSceneName;
        LoadScene(name);
    }
    
    public void SaveCurrentScene(){
        Hero.active.data.SavedSceneName = activeSceneName;
    }
    IEnumerator LoadAsyncScene(string sceneName, bool deserialize) {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (deserialize) { Hero.active.DeserializeLocation(); }
    }

    void OnSceneLoadEvent(Scene scene, LoadSceneMode mode) {
        this.activeSceneName = scene.name;
    
    }

    //DEBUGGING TODO: Remove later
    public void CreateNewHero(string name) {
        Hero.active.data.Name = name;
    }
}
