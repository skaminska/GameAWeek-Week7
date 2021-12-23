using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick2D : MonoBehaviour
{
    [SerializeField] Camera mainCamera;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckIfResources();
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

        }
    }
}
