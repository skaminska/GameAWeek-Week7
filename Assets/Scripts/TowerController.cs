using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] int goldReqiured;
    [SerializeField] int woodRequired;
    [SerializeField] GameObject attackProjecttile;
    [SerializeField] float range;

    GameObject currentEnemy;
    float timeToAttack;

    private void Update()
    {
        if (timeToAttack > 0)
            timeToAttack -= Time.deltaTime;
        if (currentEnemy != null)
        {
            if (Vector3.Distance(transform.position, currentEnemy.transform.position) > range)
                currentEnemy = null;
            if (timeToAttack <= 0)
            {
                Attack();
                timeToAttack = 1;
            }

        }
        else
        {
            foreach(var enemy in EnemyController.Instance.enemiesList)
            {
                if(Vector3.Distance(transform.position, enemy.transform.position) <= range)
                {
                    currentEnemy = enemy;
                }
            }
        }
            
    }



    void Attack()
    {
        GameObject attProj = Instantiate(attackProjecttile, transform.position, Quaternion.identity);
        attProj.GetComponent<AttackingElementsMovement>().SetTarget(currentEnemy);
    }

    public bool CheckIfRequirements()
    {
        if (goldReqiured <= PlayerController.Instance.gold && woodRequired <= PlayerController.Instance.wood)
            return true;
        else
            return false;
    }

    public void BuildThisTower()
    {
        PlayerController.Instance.gold -= goldReqiured;
        PlayerController.Instance.wood -= woodRequired;
    }
}
