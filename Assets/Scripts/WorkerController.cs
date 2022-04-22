using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    [SerializeField] string resourceInInventory;
    [SerializeField] public Transform fort;

    public bool busy;
    Resource currentResource;
    bool gatheringResources;
    bool removeFromWork;
    public bool unpackingResource;

    void Start()
    {
        removeFromWork = false;
        busy = false;
        gatheringResources = false;
        resourceInInventory = null;
        unpackingResource = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (busy && currentResource != null && resourceInInventory == null)
            transform.position = Vector2.MoveTowards(transform.position, currentResource.gameObject.transform.position, 3 * Time.deltaTime);
        else if (busy && currentResource != null && resourceInInventory != null)
            transform.position = Vector2.MoveTowards(transform.position, fort.transform.position, 3 * Time.deltaTime);
        else if (currentResource == null)
            busy = false;

        if (transform.position == fort.position && resourceInInventory != null)
            StartCoroutine(LeftResource());

        if(!busy)
            transform.position = Vector2.MoveTowards(transform.position, fort.transform.position, 3 * Time.deltaTime);

        if (currentResource!=null && transform.position == currentResource.transform.position && !gatheringResources)
        {
            gatheringResources = true;
            StartCoroutine(CollectResource());
        }
    }

    public IEnumerator LeftResource()
    {
        unpackingResource = true;
        yield return new WaitForSeconds(1);
        PlayerController.Instance.AddResource(resourceInInventory);
        resourceInInventory = null;
        if(removeFromWork)
        {
            RemoveFromWork();
        }
        unpackingResource = false;
    }

    public void RemoveFromWork()
    {
        if(resourceInInventory == null)
        {
            busy = false;
            currentResource = null;
            removeFromWork = false;
        }
        else
        {
            removeFromWork = true;
        }
    }

    public void SetWork(Resource resource)
    {
        busy = true;
        currentResource = resource;
    }

    private IEnumerator CollectResource()
    {
        yield return new WaitForSeconds(3);
        if(currentResource !=null)
        {
            resourceInInventory = currentResource.resourceType.ToString();
        }
        gatheringResources = false;
    }

    public Resource GetWorkerCurrentResource()
    {
        return currentResource;
    }
}
