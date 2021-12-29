using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>, IGetDamage
{
    [SerializeField] public int wood;
    [SerializeField] public int gold;
    [SerializeField] public int food;
    [SerializeField] List<WorkerController> workers;
    [SerializeField] public List<WarriorController> warriors;
    [SerializeField] GameObject workerPref;
    [SerializeField] GameObject warriorPref;
    [SerializeField] List<Transform> warriorsPath;
    [SerializeField] int healthPoints;

    [SerializeField] public int newWorkerRequireGold;
    [SerializeField] public int newWorkerRequireFood;

    [SerializeField] public int newWarriorRequireGold;
    [SerializeField] public int newWarriorRequireFood;

    // Start is called before the first frame update
    void Start()
    {
    }

    internal void RemoveFromWork(Resource resource)
    {
        var worker = workers.Find(worker => worker.GetWorkerCurrentResource()==resource);
        if (worker != null)
        {
            worker.RemoveFromWork();
            resource.RemoveWorker();
        }
    }

    public void SendToWork(Resource resource)
    {
        var worker = workers.Find(worker => worker.busy == false);
        if (worker != null)
        {
            worker.SetWork(resource);
            resource.AddWorker();
        }
    }

    public void AddResource(string type)
    {
        if (type == "WOOD")
            wood++;
        else if (type == "GOLD")
            gold++;
        else if (type == "FOOD")
            food++;
    }

    internal void BuyNewWarrior()
    {
        if (gold >= newWarriorRequireGold && food >= newWarriorRequireFood)
        {
            gold -= newWarriorRequireGold;
            food -= newWarriorRequireFood;


            GameObject warrior = Instantiate(warriorPref, transform.position, Quaternion.identity);
            warrior.GetComponent<WarriorController>().SetPath(warriorsPath);
            warriors.Add(warrior.GetComponent<WarriorController>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Worker")
        {
            if (collision.gameObject.GetComponent<WorkerController>() && !collision.gameObject.GetComponent<WorkerController>().unpackingResource)
            {
                collision.gameObject.GetComponent<WorkerController>().LeftResource();
            }
        }
    }

    public List<string> UpdateInfo()
    {
        return new List<string> { workers.Count.ToString(), warriors.Count.ToString(), gold.ToString(), food.ToString(), wood.ToString() };
    }

    public void BuyNewWorker()
    {
        if(gold >= newWorkerRequireGold && food >= newWorkerRequireFood)
        {
            gold -= newWorkerRequireGold;
            food -= newWorkerRequireFood;


            GameObject worker = Instantiate(workerPref, transform.position, Quaternion.identity);
            worker.GetComponent<WorkerController>().fort = this.transform;
            workers.Add(worker.GetComponent<WorkerController>());
        }
    }

    public void Attack()
    {
        foreach(var warrior in warriors)
        {
            warrior.StartAttack();
        }
    }

    public void GetDamage(int damage)
    {
        healthPoints -= damage;
    }
}
