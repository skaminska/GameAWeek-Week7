using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Singleton<EnemyController>, IGetDamage
{
    [SerializeField] List<Transform> path;
    [SerializeField] GameObject enemy;
    [SerializeField] int healthPoints;

    [SerializeField] public List<GameObject> enemiesList;

    [SerializeField] float timeToNewWave;
    int enemyInWave;

    private void Start()
    {
        enemyInWave = 3;
    }


    // Update is called once per frame
    void Update()
    {
        timeToNewWave -= Time.deltaTime;
        if(timeToNewWave <= 0)
        {
            timeToNewWave = 30;
            StartCoroutine(SpawnEnemy());
        }

        if (healthPoints < 0)
        {
            Debug.Log("You Win");
            Destroy(this);
        }

    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < enemyInWave; i++)
        {
            yield return new WaitForSeconds(0.5f);

            GameObject enemyCon = Instantiate(enemy, transform.position, Quaternion.identity);
            enemyCon.GetComponent<EnemyMovement>().SetPath(path);

            enemiesList.Add(enemyCon);
        }
        enemyInWave += 2;
    }

    public void GetDamage(int damage)
    {
        healthPoints -= damage;
    }
}
