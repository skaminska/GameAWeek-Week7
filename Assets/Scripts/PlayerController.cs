using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] public int wood;
    [SerializeField] public int gold;
    [SerializeField] List<WorkerController> workers;
    [SerializeField] GameObject workerPref;
    [SerializeField] int maxWorkersNumber;

    // Start is called before the first frame update
    void Start()
    {
        wood = 0;
        gold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && maxWorkersNumber>workers.Count)
        {
            GameObject worker = Instantiate(workerPref, transform.position, Quaternion.identity);
            worker.GetComponent<WorkerController>().owner = WorkerController.Owner.Player;
            worker.GetComponent<WorkerController>().fort = this.transform;
            workers.Add(worker.GetComponent<WorkerController>());
        }
    }

    public void SendToWork(Resource resource)
    {
        var worker = workers.Find(worker => worker.busy == false);
        if(worker!=null)
            worker.SetWork(resource);
    }

    public void AddResource(string type)
    {
        Debug.Log("oo this is " + type);
        if (type == "WOOD")
            wood++;
        else if (type == "GOLD")
            gold++;
        else
            Debug.Log("I have no idea what it is");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Worker")
        {
            Debug.Log("it's me");
            if (collision.gameObject.GetComponent<WorkerController>())
            {
                collision.gameObject.GetComponent<WorkerController>().LeftResource();
            }
        }
    }
}
