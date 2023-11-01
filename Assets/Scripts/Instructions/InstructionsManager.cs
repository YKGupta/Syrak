using System.Collections;
using UnityEngine;
using TMPro;

public class InstructionsManager : MonoBehaviour
{
    public GameObject instructionsPanel;
    public TMP_Text instructionsText;
    public PlayerMovement playerMovement;
    public InventoryManager inventoryManager;

    public float waitTimeBetweenInstructions;
    [Tooltip("0 - Movement\n1-Jump\n2-Inventory")]
    public string[] instructions;

    private int i;

    private void Start()
    {
        playerMovement.playerJumped += OnPlayerJump;
        playerMovement.playerMoved += OnPlayerMove;
        inventoryManager.itemClicked += OnItemCollect;
        inventoryManager.inventoryOpened += OnInventoryOpen;
        i = 0;
        instructionsText.text = instructions[i];
        instructionsPanel.SetActive(true);
    }

    public void OnPlayerMove()
    {
        if(i != 0)
            return;

        StartCoroutine(WaitBeforeReenable(waitTimeBetweenInstructions));
        instructionsText.text = instructions[++i];
    }

    public void OnPlayerJump()
    {
        if(i != 1 || !instructionsPanel.activeSelf)
            return;

        instructionsText.text = instructions[i];
        instructionsPanel.SetActive(false);
        i++;
    }

    public void OnItemCollect()
    {
        if(i != 2 || instructionsPanel.activeSelf)
            return;
        
        instructionsText.text = instructions[i];
        instructionsPanel.SetActive(true);
    }

    public void OnInventoryOpen()
    {
        if(i != 2 || !instructionsPanel.activeSelf)
            return;
        
        instructionsPanel.SetActive(false);
        i++;
    }

    private IEnumerator WaitBeforeReenable(float waitTime)
    {
        instructionsPanel.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        instructionsPanel.SetActive(true);
    }
}
