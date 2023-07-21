using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class enemy : MonoBehaviour
{
    private GameObject playerObject; // the reference to the player GameObject
    public float Speed = 5; // Move speed of the enemy
    private Rigidbody enemyRb;
    public Component enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerObject = GameObject.Find("Player");
        enemyScript = GetComponent<enemy>();
       

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (playerObject.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection* Speed);
        if (transform.position.y<-0.8 ||transform.position.x>20||transform.position.x<-20||
            transform.position.z<-20||transform.position.z>20||transform.position.y>20 )
        {
            
            Component.Destroy(enemyScript);
            
        }
     
    }
   
}
    