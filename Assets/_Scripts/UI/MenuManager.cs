using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

    public MainMenu MainMenuPrefab;
    public ItemMenu ItemMenuPrefab;

    public SamplerMenu SamplerMenuPrefab;

    private Stack<Menu> menuStack = new Stack<Menu>();

    private void Start()
    {
        InputManager.im.notifyMenuButtonObservers += OpenMainMenu;
        InputManager.im.notifyCancelButtonObservers += CloseMenu;
    }

    public void OpenMenu(Menu instance)
    {
        if(menuStack.Count <= 0){
            InputManager.im.DisableMovement();
        }
        // De-activate top menu
        if (menuStack.Count > 0)
        {
            if (instance.DisableMenusUnderneath)
            {
                foreach (var menu in menuStack)
                {
                    menu.gameObject.SetActive(false);

                    if (menu.DisableMenusUnderneath)
                        break;
                }
            }

            var topCanvas = instance.GetComponent<Canvas>();
            var previousCanvas = menuStack.Peek().GetComponent<Canvas>();
            topCanvas.sortingOrder = previousCanvas.sortingOrder + 1;
        }

        menuStack.Push(instance);
    }

    private T GetPrefab<T>() where T : Menu
    {
        // Get prefab dynamically, based on public fields set from Unity
        // You can use private fields with SerializeField attribute too
        var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach (var field in fields)
        {
            var prefab = field.GetValue(this) as T;
            if (prefab != null)
            {
                return prefab;
            }
        }

        throw new MissingReferenceException("Prefab not found for type " + typeof(T));
    }


    public void CreateInstance<T>() where T : Menu
    {
        var prefab = GetPrefab<T>();

        Instantiate(prefab, transform);
    }

    public void OpenMainMenu()
    {
        if (menuStack.Count <= 0)
            MainMenu.Show();
    }

    public void OpenItemMenu(){
       ItemMenu.Show();
    }

    public void OpenSamplerMenu(){
       SamplerMenu.Show();
    }

    public void CloseMenu()
    {
        if (menuStack.Count > 0)
        {
            var _menu = menuStack.Pop();
            Destroy(_menu.gameObject);

            //Reactivate Topmenu
            if (menuStack.Count > 0) { menuStack.Peek().gameObject.SetActive(true); }
            else{
                InputManager.im.EnableMovement();
            }
        }

    }
}