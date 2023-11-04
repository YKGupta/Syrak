using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TriggerEvents endTrigger;
    public TimerManager timerManager;

    public GameObject winPanelGO;
    public GameObject losePanelGO;
    public Behaviour[] behavioursToDisableAtEnd;

    private void Start()
    {
        endTrigger.onTriggerEnter += OnGameWon;
        timerManager.timeOver += OnGameLost;
    }

    public void OnGameWon(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        
        Time.timeScale = 0f;
        winPanelGO.SetActive(true);
        DisableBehaviours();
    }

    public void OnGameLost()
    {
        Time.timeScale = 0f;
        losePanelGO.SetActive(true);
        DisableBehaviours();
    }

    private void DisableBehaviours()
    {
        foreach(Behaviour b in behavioursToDisableAtEnd)
            b.enabled = false;
    }
}
