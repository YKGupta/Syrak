using UnityEngine;

public class ItemButton : MonoBehaviour
{
    private Item item;

    public void RemoveItem()
    {
        InventoryManager.instance.RemoveItem(item);
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
}
