using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : Singleton<EnemyController>, IGetDamage
{
    [SerializeField] List<Transform> path;
    [SerializeField] GameObject enemy;
    [SerializeField] int healthPoints;
    [SerializeField] Slider timeSlider;

    [SerializeField] Slider hpSlider;
    [SerializeField] public List<GameObject> enemiesList;

    [SerializeField] float timeToNewWave;

    int enemyInWave;
    public bool wave;

    private void Start()
    {
        wave = false;
        enemyInWave = 3;
    }

    void Update()
    {
        timeToNewWave -= Time.deltaTime;
        timeSlider.value = timeToNewWave;

        if(timeToNewWave <= 0 && !wave)
        {
            wave = true;
            StartCoroutine(SpawnEnemy());
        }

        if (healthPoints < 0)
        {
            SceneManager.LoadScene(1);
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

    public void WaveDefete()
    {
        timeToNewWave = 30;
        timeSlider.value = timeToNewWave;
        wave = false;
    }

    public void GetDamage(int damage)
    {
        healthPoints -= damage;
        hpSlider.value = healthPoints;
    }
}
