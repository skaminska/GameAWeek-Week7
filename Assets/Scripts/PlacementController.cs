using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : Singleton<PlacementController>
{
    [SerializeField] GameObject buildingShiluet;

    GameObject placement;
    GameObject bsInstance;

    void Start()
    {
        this.enabled = false;   

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(bsInstance);
            this.enabled = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (bsInstance.GetComponent<BuildingShiluetController>().BuildThis())
            {
                Destroy(bsInstance);
                placement.GetComponent<SpriteRenderer>().enabled = false;
                placement.SetActive(false);
                this.enabled = false;
            }
        }
    }

    public void SetPlacement(GameObject placement)
    {
        bsInstance = Instantiate(buildingShiluet, placement.transform.position, Quaternion.identity);
        this.placement = placement;        
    }
}
