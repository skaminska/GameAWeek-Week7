using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour, IGetDamage
{
    [SerializeField] List<Transform> path;
    [SerializeField] float speed;
    [SerializeField] int healthPoint;
    [SerializeField] float range;
    [SerializeField] GameObject arrow;

    int pointAchive;
    bool attack;
    float timeToAttack;

    GameObject target;
    GameObject fort;

    // Start is called before the first frame update
    void Start()
    {
        range = 3f;
        attack = false;
        pointAchive = 0;
        fort = FindObjectOfType<EnemyController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            if (timeToAttack > 0)
                timeToAttack -= Time.deltaTime;

            if (target == null)
            {
                transform.position = Vector2.MoveTowards(transform.position, path[pointAchive].position, speed * Time.deltaTime);
                if (EnemyController.Instance.enemiesList.Count != 0)
                {
                    foreach (var enemy in EnemyController.Instance.enemiesList)
                    {
                        if (Vector3.Distance(enemy.transform.position, transform.position) < range)
                            target = enemy.gameObject;
                    }
                }

                if (Vector3.Distance(fort.transform.position, transform.position) < range+1)
                    target = fort;


            }

            else if(target!= null && timeToAttack <= 0)
            {
                Attack();
                timeToAttack = 1;
            }


            if (healthPoint <= 0)
            {
                PlayerController.Instance.warriors.Remove(this);
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Path" && pointAchive < path.Count - 1)
            pointAchive++;
    }

    public void SetPath(List<Transform> path)
    {
        this.path = path;
    }

    public void StartAttack()
    {
        attack = true;
    }

    void Attack()
    {
        GameObject attProj = Instantiate(arrow, transform.position, Quaternion.identity);
        attProj.GetComponent<AttackingElementsMovement>().SetTarget(target);
    }

    public void GetDamage(int damage)
    {
        healthPoint -= damage;
    }
}
