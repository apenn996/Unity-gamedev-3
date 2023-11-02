using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    public GameObject towerLocation;
    //[SerializeField] private GameObject tower1Prefab;
    
    // Start is called before the first frame update
    
    public void AddTower(GameObject towerPrefab)
    {
        if (PlayerController.curMoney < towerPrefab.GetComponent<TowerProperties>().purchaseCost)
        {
            //not enough money to buy the tower (panel)
            print("doesnt have money");
            print(PlayerController.curMoney);
        }
        else
        {
            print("hasmoney");
            print(PlayerController.curMoney);

            towerLocation.GetComponent<TowerGenerator>().tower = Instantiate(towerPrefab, towerLocation.transform.position, Quaternion.identity);
            towerLocation.GetComponent<TowerGenerator>().hasTower = true;
            PlayerController.curMoney -= towerPrefab.GetComponent<TowerProperties>().purchaseCost;
        }
        gameObject.SetActive(false);
    }

    


    // Update is called once per frame

}
