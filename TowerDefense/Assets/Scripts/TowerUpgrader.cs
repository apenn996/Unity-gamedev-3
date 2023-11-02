using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class TowerUpgrader : MonoBehaviour
{
    //public GameObject towerLocation;
    public GameObject tower = null;
    [SerializeField] private Text upgradeText;
    [SerializeField] private Text missingFunds;
    //[SerializeField] private GameObject tower1Prefab;

    // Start is called before the first frame update
    private void Update()
    {
        upgradeText.text = "Upgrade: " + tower.GetComponent<TowerProperties>().upgradeCost ;
        if (tower.GetComponent<TowerProperties>().upgradeCost == 200)
            {
                upgradeText.text = "This tower is fully upgraded!";

            }
    }
    public void UpgradeTower()
    {
        //gameObject.GetComponent<Renderer>().material.color = Color.red;
        if (tower == null)
        {
            //no tower to upgrade
        }
        else if (PlayerController.curMoney < tower.GetComponent<TowerProperties>().upgradeCost)
        {
            StartCoroutine("missingF");
            //not enough money to upgrade the tower (panel)
            print("not enough money to upgrade tower");
        }
        else
        {
            // do upgrade
            //call a function
            print("trying to upgrade");
            int upReturn = tower.GetComponent<TowerProperties>().UpgradeTower();

            upgradeText.text = "Upgrade: " + tower.GetComponent<TowerProperties>().upgradeCost;
            if (tower.GetComponent<TowerProperties>().upgradeCost == 200)
            {
                upgradeText.text = "This tower is fully upgraded!";

            }
            if (upReturn == -1)
            {
                print(" upgrade failed");

            }
            else
            {
                print(tower.GetComponent<TowerProperties>().upgradeCost);
                print(tower.GetComponent<TowerProperties>().towerDamage);
                print(tower.GetComponent<TowerProperties>().towerRange);
                //PlayerController.curMoney -= tower.GetComponent<TowerProperties>().upgradeCost;

            }
        }
        gameObject.SetActive(false);
    }

    IEnumerator missingF()
    {
        missingFunds.enabled=true;
        yield return new WaitForSeconds(4);
        missingFunds.enabled = false;

    }
    public void SellTower()
    {
        //sell
        
        //tower.GetComponent<TowerSelector>().towerLocation.GetComponent<TowerGenerator>().hasTower = false;
        //print(tower.GetComponent<TowerSelector>().towerLocation.GetComponent<TowerGenerator>().hasTower);
        tower.GetComponent<TowerProperties>().sellTower();
        //Destroy(gameObject);
        //Destroy(tower);
        //gameObject.SetActive(false);
    }
}
