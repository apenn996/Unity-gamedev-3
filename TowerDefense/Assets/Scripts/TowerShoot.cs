using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(TowerProperties))]

    public class TowerShoot : MonoBehaviour
    {
        [SerializeField] private LineRenderer laser;
        [SerializeField] private Transform fireLaserLocation;
        [SerializeField] private Transform towerHead;
        [SerializeField] private GameObject bombExplosion;
         [SerializeField] private GameObject machinegunFire;
        private TowerProperties tower;
    private bool audioOn = false;
        private GameObject target;
        private float nextShootTime = 0;
        // Start is called before the first frame update
        void Start()
        {
       //     Debug.Log("dd");
            tower = GetComponent<TowerProperties>(); Debug.Log("dd");
            InvokeRepeating("SelectTarget", 0, 0.5f); Debug.Log("dd");
        }

        // Update is called once per frame
        private void Update()
        {
     //   print("update");
        if (target == null)
            {
                laser.enabled = false;
                return;
            }
        
        LockOnTarget();

      //  print("update2");
        ShootTarget();
        }
        private void ShootTarget()
        {
            //Debug.Log("in shoot target");
            if (Time.time > nextShootTime)
            {
               // Debug.Log("time > nextShoot");
                switch (tower.towerType)
                {
                    case TOWER_TYPE.LASER:
                       // Debug.Log("laser case");
                        ShootLaser();
                        break;
                    case TOWER_TYPE.BOMB:
                        laser.enabled = false;
                        ShootBomb();
                        break;
                    case TOWER_TYPE.GUN:
                        laser.enabled = false;
                        ShootGun();
                        break;
                    default:
                        break;
                }
                nextShootTime = Time.time + 1 / tower.shootRate;
            }
        }
        private void SelectTarget()
        {
            //Debug.Log("in selected target");
            float minDist = Mathf.Infinity;
            int selectedEnemey = -1;
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < allEnemies.Length; i++)
            {
                //Debug.Log("st for");

                float distance = Vector3.Distance(transform.position, allEnemies[i].transform.position);
                if (distance < minDist)
                {
                    minDist = distance;
                    selectedEnemey = i;
                }
            }
            ////Debug.Log("st before if");

            if (selectedEnemey != -1 && minDist <= tower.towerRange)
                target = allEnemies[selectedEnemey];
            else
                target = null;
            //Debug.Log("done");

        }

        private void LockOnTarget()
        {
        if (tower.towerType == TOWER_TYPE.BOMB)
        {
            towerHead.transform.rotation = Quaternion.LookRotation(target.transform.position - fireLaserLocation.position);
            towerHead.transform.eulerAngles = new Vector3(0, towerHead.eulerAngles.y, 0);
        }
        if (tower.towerType == TOWER_TYPE.GUN)
            towerHead.transform.rotation = Quaternion.LookRotation(target.transform.position - fireLaserLocation.position);
    }
        private void ShootGun()
        {
        if (!audioOn)
            StartCoroutine("audioPlay");
        GameObject partPref =    Instantiate(machinegunFire, towerHead.transform.position, towerHead.transform.rotation.normalized);
            target.GetComponent<EnemyMovement>().TakeDamage(tower.towerDamage);
            StartCoroutine("emptyObj", partPref);

        }
    IEnumerator emptyObj(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        Destroy(obj);
        
    }
    private void ShootLaser()
        {
        if(!audioOn)
        StartCoroutine("audioPlay");
        //Debug.Log("firing laser");
            laser.enabled = true;
            laser.SetPosition(0, fireLaserLocation.position);
            laser.SetPosition(1, target.transform.position);
            target.GetComponent<EnemyMovement>().TakeDamage(tower.towerDamage);
        }
    IEnumerator audioPlay()
    {
        audioOn = true;
        tower.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3);
        tower.GetComponent<AudioSource>().Stop();

        audioOn = false;
    }
    private void ShootBomb()
        {
            tower.GetComponent<AudioSource>().Play();
            GameObject partPref = Instantiate(bombExplosion, target.transform.position, Quaternion.identity);
            target.GetComponent<EnemyMovement>().TakeDamage(tower.towerDamage);
            StartCoroutine("emptyObj", partPref);
    }
}
