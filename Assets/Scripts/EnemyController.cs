using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] List<Transform> path;
    [SerializeField] GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //TODO Lista zespalnowanych przeciwników 
            GameObject enemyCon = Instantiate(enemy, transform.position, Quaternion.identity);
            enemyCon.GetComponent<EnemyMovement>().SetPath(path);
        }
    }
}
