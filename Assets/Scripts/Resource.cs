using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int resourceLeft;
    public enum ResourceType { WOOD, GOLD};

    [SerializeField] public ResourceType resourceType;

    private void Update()
    {
        if (resourceLeft == 0)
            Destroy(gameObject);
    }
}
