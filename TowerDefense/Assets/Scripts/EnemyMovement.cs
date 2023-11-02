using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject prev;
    private GameObject next;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float health = 10f;
    [SerializeField] private GameObject deathParticle;
    //[SerializeField] private GameObject deathNoise;
    private bool isDead = false;
 
    // Start is called before the first frame update
    void Start()
    {
        prev = PathGenerator.path[0];
        transform.position = prev.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Convert.ToInt32(prev.name) < PathGenerator.path.Count - 1)
        {


            next = PathGenerator.path[Convert.ToInt32(prev.name) + 1];
            transform.position += (next.transform.position - transform.position).normalized * Time.deltaTime * speed;
            Quaternion rot = Quaternion.LookRotation((next.transform.position - transform.position).normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 2f);
            if(Vector3.Distance(transform.position, next.transform.position) <= 0.01f)
                prev = next;
        }
        else
        {

            //reduce player health
            GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>().TakeDamage(damage);
            EnemySpawner.curEnemies--;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0 && !isDead)
        {
            Debug.Log("DEAD");
            gameObject.GetComponent<AudioSource>().Play();
            isDead = true;
            GameObject partPref = Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity);
            StartCoroutine("emptyObj", partPref);
            StartCoroutine("killObj", gameObject);

            EnemySpawner.curEnemies-=1;
            PlayerController.curMoney += 10;
        }
    }
    IEnumerator killObj(GameObject obj)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(obj);

    }
    IEnumerator emptyObj(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        Destroy(obj);

    }
}
