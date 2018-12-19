using UnityEngine;
[CreateAssetMenu]

/* 
    Thank you to TonyLi from https://forum.unity.com/threads/how-to-serialize-save-inventory-items.465844/
    for this cool idea on item database scriptable to use so I can serialize an inventory and call 
    saved items by their id
*/

public class ItemDatabase : ScriptableObject
{
    public Item[] items;
 
    public Item GetItem(int itemID)
    {
        foreach (var item in items)
        {
            if (item != null && item.ID == itemID) return item;
        }
        return null;
    }
}