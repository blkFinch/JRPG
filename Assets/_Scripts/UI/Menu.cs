using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu<T> : Menu where T : Menu<T>
{

    public static Menu Instance { get; private set; }

    //Menu will be inheritable and overwritable so it must use protected virtual methods
    protected virtual void Awake()
    {
        Instance = this;
    }

    protected virtual void Start()
    {
        InputManager.im.notifyCancelButtonObservers += OnBackPressed;
    }

    protected virtual void OnDestroy()
    {
        Instance = null;
    }
}

public abstract class Menu : MonoBehaviour
{
    public abstract void OnBackPressed();
}
