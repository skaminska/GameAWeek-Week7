using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    public bool busy;
    Resource currentResource;
    public enum Owner { Player, Enemy}
    bool gatheringResources;

    [SerializeField] string resourceInInventory;
    //public ResourceInInventory resourceInInventory;

    [SerializeField] public Owner owner;
    [SerializeField] public Transform fort;


    // Start is called before the first frame update
    void Start()
    {
        busy = false;
        gatheringResources = false;
        resourceInInventory = null;
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

        if (currentResource!=null && transform.position == currentResource.transform.position && !gatheringResources)
        {
            gatheringResources = true;
            StartCoroutine(CollectResource());
        }
           
    }

    public IEnumerator LeftResource()
    {
        yield return new WaitForSeconds(1);
        PlayerController.Instance.AddResource(resourceInInventory);
        resourceInInventory = null;
    }

    public void SetWork(Resource resource)
    {
        busy = true;
        currentResource = resource;
        Debug.Log("i am going to collect some " + resource.gameObject.name);
    }

    private IEnumerator CollectResource()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(currentResource.resourceType.ToString());
        if(currentResource.resourceLeft > 0)
        {
            currentResource.resourceLeft--;
            resourceInInventory = currentResource.resourceType.ToString();
            
        }
        gatheringResources = false;
    }

}
