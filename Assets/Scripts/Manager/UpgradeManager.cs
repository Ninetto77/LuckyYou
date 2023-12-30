using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgratePanel[] upgratePanels = new UpgratePanel[4];
    void Start()
    {
        //GameObject[] g = GameObject.FindGameObjectsWithTag("Upgrade");
        //for (int i = 0; i < g.Length; i++)
        //{
        //    upgratePanels[i] = g[i].GetComponent<UpgratePanel>();
        //}
        QuestReward.OnUpgradesAppliances += ChangeBarrierLevel;
    }


    public void ChangeBarrierLevel(int level)
    {
        foreach (var panel in upgratePanels)
        {
            panel.ChangeBarrierLevel(level);
        }
    }
}
