using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;
    private bool ispressed = false;
    private bool ispressed1 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotationSpeed * HorizontalInput * Time.deltaTime);
        if (ispressed)
        {
            transform.Rotate(Vector3.up * rotationSpeed * 1 * Time.deltaTime);
        }
        if (ispressed1)
        {
            transform.Rotate(Vector3.up * rotationSpeed * -1 * Time.deltaTime);
        }
    }
    public void leftButtonDown()
    {
        ispressed = true;
    }
    public void leftButtonUp()
    {
        ispressed = false;
    }
    public void rightButtonDown()
    {
        ispressed1 = true;
    }
    public void rightButtonUp()
    {
        ispressed1 = false;
    }
}
