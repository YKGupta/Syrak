using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InventoryManager : MonoBehaviour
{
    [BoxGroup("UI")]
    public GameObject inventoryGO;
    [BoxGroup("UI")]
    public GameObject itemUIPrefab;
    [BoxGroup("UI")]
    public Transform itemsUIParent;
    [BoxGroup("UI")]
    public GameObject loadingScreenGO;

    public KeyCode inventoryToggleKey;

    [HideInInspector]
    public Animator animator; // Set by PlayerInventory

    private List<Item> items;

    public static InventoryManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        items = new List<Item>();
        PlayerManager.instance.isPlayerAllowedToOpenInventory = true;
    }

    private void Update()
    {
        if(!PlayerManager.instance.isPlayerAllowedToOpenInventory)
            return;
            
        if(Input.GetKeyDown(inventoryToggleKey))
        {
            SetInventory(!inventoryGO.activeSelf);
        }
    }

    public void SetInventory(bool state)
    {
        ClearInventory();

        if(state)
        {
            SetLoadingScreen(true);

            foreach(Item item in items)
            {
                GameObject tempGO = Instantiate(itemUIPrefab, itemsUIParent);
                tempGO.GetComponent<ItemButton>().SetItem(item);
                UI_Initialiser temp = tempGO.GetComponent<UI_Initialiser>();
                temp.SetText(item.name);
                temp.SetImage(item.sprite);
            }

            SetLoadingScreen(false);
        }

        PlayerManager.instance.isPlayerAllowedToMove = !state;
        PlayerManager.instance.isPlayerAllowedToLook = !state;

        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;

        inventoryGO.SetActive(state);
        animator.SetBool("openInventory", state);
    }

    public void ClearInventory(bool removeItemsFromListAsWell = false)
    {
        for(int i = 0; i < itemsUIParent.childCount; i++)
        {
            Destroy(itemsUIParent.GetChild(i).gameObject);
        }

        if(!removeItemsFromListAsWell)
            return;
    }

    public void SetLoadingScreen(bool state, string text = "Loading...")
    {
        loadingScreenGO.SetActive(state);
        UI_Initialiser temp = loadingScreenGO.GetComponent<UI_Initialiser>();
        temp.SetText(text);
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        item.gameObject.GetComponent<Renderer>().enabled = false;
        item.gameObject.GetComponent<Collider>().enabled = false;
        item.enabled = false;
    }
    
    public void RemoveItem(Item item, bool isUsed = false, bool showInventory = true)
    {
        items.Remove(item);
        item.gameObject.GetComponent<Renderer>().enabled = !isUsed;
        item.gameObject.GetComponent<Collider>().enabled = !isUsed;
        item.enabled = !isUsed;

        SetInventory(showInventory);
    }

    public Item FindItem(int id)
    {
        Item temp = items.Find(x => x.id == id);
        return temp;
    }
}
