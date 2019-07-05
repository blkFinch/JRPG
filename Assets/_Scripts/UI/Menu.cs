using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu<T> : Menu where T : Menu<T>
{

    public static Menu Instance { get; private set; }

    //Menu will be inheritable and overwritable so it must use protected virtual methods
    protected virtual void Awake()
    {
        Instance = (T)this;
    }

    protected static void Open()
	{
		if (Instance == null)
			MenuManager.Instance.CreateInstance<T>();
		else
			Instance.gameObject.SetActive(true);
		
		MenuManager.Instance.OpenMenu(Instance);
	}

    protected virtual void OnDestroy()
    {
        Instance = null;
    }

    public static void Show()
	{
		Open();
	}
}

public abstract class Menu : MonoBehaviour
{
	[Tooltip("Destroy the Game Object when menu is closed (reduces memory usage)")]
	public bool DestroyWhenClosed = true;

	[Tooltip("Disable menus that are under this one in the stack")]
	public bool DisableMenusUnderneath = true;
	

}



