using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TOWER_TYPE { BOMB, LASER, GUN, TOWER_COUNT} ;
public class TowerProperties : MonoBehaviour
{
    public TOWER_TYPE towerType = TOWER_TYPE.LASER;
    public float shootRate = 50f;
    public int purchaseCost = 50;
    public float towerDamage = 10f;
    public float towerRange = 5f;
    public int upgradeCost = 25;
    public int sellPrice ;
    public int totalUpgrades = 0;
    public static bool isSold = false;
    private Material SphereMaterial;
    [SerializeField] public GameObject anewSphere;

    private GameObject mySphere;

    private void Start()
    {
        if(towerType == TOWER_TYPE.BOMB)
        {
            mySphere = Instantiate(anewSphere, gameObject.transform);
            mySphere.transform.localScale = new Vector3(8, 8, 8);
        }
        else
        mySphere = Instantiate(anewSphere, gameObject.transform);
       
    }
    private void Update()
    {
        sellPrice = upgradeCost / 2;
    }
    public int UpgradeTower()
    {
        
        

        gameObject.GetComponent<Renderer>().material.color = Color.red;
        print("TOtal upgrades are: " + totalUpgrades);
        if (totalUpgrades < 3)
        {
            PlayerController.curMoney -= gameObject.GetComponent<TowerProperties>().upgradeCost;

            if (totalUpgrades == 0)
            {
               
                mySphere.transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                mySphere.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
            else if (totalUpgrades == 1) {
                mySphere.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (totalUpgrades == 2) {
                mySphere.GetComponent<Renderer>().material.color = Color.yellow;
             }
            upgradeCost *= 2;
            if(towerType == TOWER_TYPE.BOMB)
            {
                towerDamage += 2;
                towerRange += .3f;
            }
            if(towerType == TOWER_TYPE.GUN)
            {
                shootRate += 2f;
                towerDamage += .01f;
            }
            if (towerType == TOWER_TYPE.LASER)
            {
                towerRange += .5f;
                shootRate += 1f;
            }
            totalUpgrades++;

            //do tower upgrade;
            
            
            towerRange += 1;
            return 0;
        }
        else
        {
            
            print("already fully upgraded");
            return -1;
        }
        //max 2 upgrades?
    }

    public void sellTower()
    {
        isSold = true;
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
