using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    //Singleton
    #region 
    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
    #endregion

    public Menu menuPrefab;

    private Stack<Menu> menuStack = new Stack<Menu>();

    public void OpenMenu()
    {
        Instantiate(menuPrefab, transform);

        if (menuStack.Count > 0) { menuStack.Peek().gameObject.SetActive(false); }

        menuStack.Push(menuPrefab);
    }

    public void CloseMenu(){
        var instance = menuStack.Pop();
        Destroy(Instance.gameObject);

        //Reactivate Topmenu
        if (menuStack.Count > 0) { menuStack.Peek().gameObject.SetActive(true); }

    }
}