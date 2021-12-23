using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInInventory
{
    string resourceType;
    public ResourceInInventory(string resourceType)
    {
        this.resourceType = resourceType;
    }

    public string GetResourceType()
    {
        return resourceType;
    }
}
