using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Animator animator;
    public float maxInteractionDistance = 10f;
    public InventoryManager inventoryManager;
    public Transform collectPoint;

    [Tooltip("All items' parent")]
    public Transform itemsParent;

    private void Start()
    {
        inventoryManager.animator = animator;
        
        for(int i = 0; i < itemsParent.childCount; i++)
        {
            Item item = itemsParent.GetChild(i).GetComponent<Item>();
            item.clicked += OnItemClick;
            item.entered += OnItemMouseEnter;
            item.exited += OnItemMouseExit;
        }
    }

    public void OnItemClick(Item item)
    {
        if(Vector3.Distance(collectPoint.position, item.transform.position) > maxInteractionDistance || inventoryManager.inventoryGO.activeSelf)
            return;

        inventoryManager.AddItem(item);  
        item.interactionUIGO.SetActive(false);         
    }

    public void OnItemMouseEnter(Item item)
    {
        if(Vector3.Distance(collectPoint.position, item.transform.position) > maxInteractionDistance || inventoryManager.inventoryGO.activeSelf)
        {
            item.interactionUIGO.SetActive(false);
            return;
        }

        item.interactionUIGO.SetActive(true);        
    }

    public void OnItemMouseExit(Item item)
    {
        item.interactionUIGO.SetActive(false);       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(collectPoint.position + collectPoint.forward * maxInteractionDistance, 1f);
    }
}
