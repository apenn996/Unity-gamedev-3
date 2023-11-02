using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator : MonoBehaviour
{
    [SerializeField] private GameObject towerSelectionPanel;
    [SerializeField] private GameObject towerUpgradePanel;
    public bool hasTower = false;
    public GameObject tower;
    // Start is called before the first frame update
    void Awake()
    {
        towerSelectionPanel = GameObject.Find("TowerSelectionPanel");
        towerUpgradePanel = GameObject.Find("TowerUpgradePanel");
    }

    private void Start()
    {
        towerSelectionPanel.SetActive(false);
        towerUpgradePanel.SetActive(false);


    }
    public void AddTower()
    {
        print(hasTower);
        if (hasTower)
        {
            //tower upgrade option or sell it

            towerUpgradePanel.SetActive(true);
            towerSelectionPanel.SetActive(false);
            towerUpgradePanel.GetComponent<TowerUpgrader>().tower = tower;
        }
        else if(TowerProperties.isSold == true)
        {
            print("sold");
           // tower = null;
        }
        else
        {
            //spawn new tower
            towerSelectionPanel.SetActive(true);
            towerUpgradePanel.SetActive(false);

            towerSelectionPanel.GetComponent<TowerSelector>().towerLocation = gameObject;
        }
    }
    
}
