using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] int goldReqiured;
    [SerializeField] int woodRequired;

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
