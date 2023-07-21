using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -19 || transform.position.x > 40 || transform.position.x < -40 ||
         transform.position.z < -40 || transform.position.z > 40 || transform.position.y > 40)
        {

            Destroy(gameObject);

        }
    }
}
