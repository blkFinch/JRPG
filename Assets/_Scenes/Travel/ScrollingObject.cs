using UnityEngine;

public abstract class ScrollingObject<T> : ScrollingObject where T : ScrollingObject<T>
{
    
}

public abstract class ScrollingObject : MonoBehaviour
{
    public float xDestroyDistance = 12f;
    public ScrollingObjectSpawner parent;

    public float scrollSpeed = 5f;

}