using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingElementsMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;

    GameObject target;
    Vector3 targetPos;

    void Update()
    {
        if (target != null)
        {
            targetPos = target.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            float n = Mathf.Atan2((targetPos - transform.position).normalized.y, (targetPos - transform.position).normalized.x) * Mathf.Rad2Deg;
            if (n < 0)
                n += 360;
            transform.eulerAngles = new Vector3(0, 0, n);

            if(Vector3.Distance(transform.position,target.transform.position) < 0.2f)
            {
                if (target.GetComponent<WarriorController>() != null)
                    target.GetComponent<WarriorController>().GetDamage(damage);
                else if (target.GetComponent<PlayerController>() != null)
                    target.GetComponent<PlayerController>().GetDamage(damage);
                else if (target.GetComponent<EnemyController>() != null)
                    target.GetComponent<EnemyController>().GetDamage(damage);
                else if (target.GetComponent<EnemyMovement>() != null)
                    target.GetComponent<EnemyMovement>().GetDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPos) < 0.2f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
