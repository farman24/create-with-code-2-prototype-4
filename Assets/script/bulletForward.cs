using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletForward : MonoBehaviour
{
        private Transform target;
        public float speed = 10f;
        public float collisionforce;

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        private void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Optionally, you can add code to rotate the rocket to face its movement direction.
        // transform.rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.LookRotation(direction);
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 opp = collision.transform.position - transform.position;
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            enemyRB.AddForce(opp * collisionforce * Time.deltaTime, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
