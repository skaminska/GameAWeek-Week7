using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShiluetController : MonoBehaviour
{
    [SerializeField] List<GameObject> buildingTypes;
    [SerializeField] List<Sprite> buildingSprite;

    int currentBuilding;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentBuilding = 0;
        spriteRenderer.sprite = buildingSprite[currentBuilding];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentBuilding < buildingTypes.Count - 1)
                currentBuilding++;
            else
                currentBuilding = 0;
        }    
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            {
                if (currentBuilding ==0 )
                    currentBuilding = buildingTypes.Count-1;
                else
                    currentBuilding--;
            }
        }

        spriteRenderer.sprite = buildingSprite[currentBuilding];
    }

    public bool BuildThis()
    {
        if (buildingTypes[currentBuilding].GetComponent<TowerController>().CheckIfRequirements())
        {
            Instantiate(buildingTypes[currentBuilding], transform.position, Quaternion.identity);
            buildingTypes[currentBuilding].GetComponent<TowerController>().BuildThisTower();
            return true;
        }
        else
            Debug.Log("Brak ci czegoœ");
        return false;
    }
}
