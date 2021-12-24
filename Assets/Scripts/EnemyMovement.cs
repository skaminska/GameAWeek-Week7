using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Transform> path;
    [SerializeField] float speed;

    public int pointAchive;


    private void Start()
    {
        pointAchive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, path[pointAchive].position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Path" && pointAchive < path.Count-1)
            pointAchive++;
    }

    public void SetPath(List<Transform> path)
    {
        this.path = path;
    }
}
