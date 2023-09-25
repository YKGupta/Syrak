using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ProgressManager : MonoBehaviour
{
    public DoorManager doorManager;
    public Slider progressImage;
    public TMP_Text doorNumberText;
    public PointerEvents doorNumberHoverPointer;
    public GameObject hoverGO;

    private void Start()
    {
        SetProgressUI();
        doorNumberHoverPointer.onPointerEnter += PointerEntered;
        doorNumberHoverPointer.onPointerExit += PointerExited;
    }

    private void Update()
    {
        SetProgressUI();
    }

    private void SetProgressUI()
    {
        progressImage.value = doorManager.GetProgress();
        doorNumberText.text = doorManager.GetOpenedDoorsNumber().ToString() + "/" + doorManager.GetTotalDoors().ToString();
    }

    private void PointerEntered(PointerEventData data)
    {
        hoverGO.SetActive(true);
    }

    private void PointerExited(PointerEventData data)
    {
        hoverGO.SetActive(false);
    }
}
