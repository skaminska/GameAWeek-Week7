using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick2D : Singleton<MouseClick2D>
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject menu;

    // Update is called once per frame
    void Update()
    {
        if (!menu.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckIfResources();
            }
            if (Input.GetMouseButtonDown(1))
            {
                CheckIfResourceToRemoveWorker();
            }
        }

    }

    private void CheckIfResourceToRemoveWorker()
    {
        Ray2D ray = new Ray2D(mainCamera.ScreenToWorldPoint(Input.mousePosition), transform.forward);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Resources")
            {
                PlayerController.Instance.RemoveFromWork(hit.collider.GetComponent<Resource>());
            }
        }
    }

    void CheckIfResources()
    {
        Ray2D ray = new Ray2D(mainCamera.ScreenToWorldPoint(Input.mousePosition), transform.forward);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Resources")
            {
                PlayerController.Instance.SendToWork(hit.collider.GetComponent<Resource>());
            }
            else if (hit.collider.gameObject.tag == "Placement" && !PlacementController.Instance.isActiveAndEnabled)
            {
                //hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                PlacementController.Instance.enabled = true;
                PlacementController.Instance.SetPlacement(hit.collider.gameObject);
            }
        }
    }
}
