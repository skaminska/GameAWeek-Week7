using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Transform> path;
    [SerializeField] float speed;
    [SerializeField] int healthPoint;
    [SerializeField] GameObject arrow;

    [SerializeField] float range;
    GameObject target;

    GameObject fort;

    float timeToAttack;

    public int pointAchive;


    private void Start()
    {
        timeToAttack = 1f;
        range = 3f;
        pointAchive = 0;
        fort = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToAttack > 0)
            timeToAttack -= Time.deltaTime;

        if (target == null)
        { 
            transform.position = Vector2.MoveTowards(transform.position, path[pointAchive].position, speed * Time.deltaTime);
            if(PlayerController.Instance.warriors.Count != 0)
            {
                foreach (var warrior in PlayerController.Instance.warriors)
                {
                    if (Vector3.Distance(warrior.transform.position, transform.position) < range)
                        target = warrior.gameObject;
                }
            }

            if (Vector3.Distance(fort.transform.position, transform.position) < range+2)
                target = fort;


        }

        else if (target != null && timeToAttack <= 0)
        {
            Attack();
            timeToAttack = 1f;
        }


        if (healthPoint <= 0)
        {
            EnemyController.Instance.enemiesList.Remove(this.gameObject);
            if (EnemyController.Instance.enemiesList.Count == 0)
                EnemyController.Instance.WaveDefete();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Path" && pointAchive < path.Count-1)
            pointAchive++;
    }

    public void SetPath(List<Transform> path)
    {
        this.path = path;
    }
    
    public void GetDamage(int damage)
    {
        healthPoint -= damage;
    }

    void Attack()
    {
        GameObject attProj = Instantiate(arrow, transform.position, Quaternion.identity);
        attProj.GetComponent<AttackingElementsMovement>().SetTarget(target);
    }
}
