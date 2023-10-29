using UnityEngine;
using TMPro;
using System;

public class QuestManager : MonoBehaviour
{
    public TMP_Text objectiveText;
    public Quest[] quests;
    
    public DoorManager doorManager;

    private int index = 0;

    private void Start()
    {
        NextQuest();
        doorManager.doorOpened += NextQuest;
    }

    public void NextQuest()
    {
        if(index >= quests.Length)
            return;

        Quest quest = Array.Find<Quest>(quests, x => x.index == index);
        SetObjective(quest.quest);
        index++;
    }

    public void SetObjective(string ob)
    {
        objectiveText.text = ob;
    }
}
