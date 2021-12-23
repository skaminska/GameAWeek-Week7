using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] int borderX;
    [SerializeField] int borderY;
    [SerializeField] float speed;
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        if (transform.position.x < -borderX && horizontalMovement < 0)
            horizontalMovement = 0;
        if (transform.position.x > borderX && horizontalMovement > 0)
            horizontalMovement = 0;

        if (transform.position.y < -borderY && verticalMovement < 0)
            verticalMovement = 0;
        if (transform.position.y > borderY && verticalMovement > 0)
            verticalMovement = 0;

        transform.position += new Vector3(horizontalMovement, verticalMovement, 0);
    }
}
