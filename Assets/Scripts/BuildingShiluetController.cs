using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingShiluetController : MonoBehaviour
{
    [SerializeField] List<GameObject> buildingTypes;
    [SerializeField] List<Sprite> buildingSprite;
    [SerializeField] TextMeshProUGUI reqInfo;

    int currentBuilding;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentBuilding = 0;
        spriteRenderer.sprite = buildingSprite[currentBuilding];
        reqInfo = GetComponentInChildren<TextMeshProUGUI>();
    }

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
        reqInfo.text = "Wood: " + buildingTypes[currentBuilding].GetComponent<TowerController>().woodRequired + "\nGold: " + buildingTypes[currentBuilding].GetComponent<TowerController>().goldReqiured;

        if (!buildingTypes[currentBuilding].GetComponent<TowerController>().CheckIfRequirements())
        {
            spriteRenderer.color = Color.red;
        }
        else
            spriteRenderer.color = Color.white;
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
