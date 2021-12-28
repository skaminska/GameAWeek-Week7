using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resource : MonoBehaviour
{
    //public int resourceLeft;
    [SerializeField] TextMeshProUGUI currentWorkersText;
    int currentWorkers;

    private void Start()
    {
        currentWorkers = 0;
    }

    public enum ResourceType { WOOD, GOLD, FOOD};

    [SerializeField] public ResourceType resourceType;

    public void AddWorker()
    {
        currentWorkers++;
        currentWorkersText.text = currentWorkers.ToString();
    }

    public void RemoveWorker()
    {
        if (currentWorkers > 0)
        {
            currentWorkers--;
            currentWorkersText.text = currentWorkers.ToString();
        }
    }

}
