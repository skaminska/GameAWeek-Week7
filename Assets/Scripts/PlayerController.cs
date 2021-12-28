using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] public int wood;
    [SerializeField] public int gold;
    [SerializeField] public int food;
    [SerializeField] List<WorkerController> workers;
    [SerializeField] GameObject workerPref;

    [SerializeField] public int newWorkerRequireGold;
    [SerializeField] public int newWorkerRequireFood;

    // Start is called before the first frame update
    void Start()
    {
        wood = 0;
        gold = 0;
        food = 0;
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
}
