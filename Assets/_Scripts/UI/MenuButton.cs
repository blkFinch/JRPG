using UnityEngine;

public abstract class MenuButton<T> : MenuButton where T : MenuButton<T>
{
    private void Awake() {
        if (SelectOnInput.active != null)
        {
            Debug.Log("SOI FOUND");
            SelectOnInput.active.AddButton(this);
        }
        else
        {
            Debug.Log("SOI NULL");
        }
    }
}

public abstract class MenuButton : MonoBehaviour
{

}