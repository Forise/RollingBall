using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody rb;

    private Vector3 currentMousePos;
    private Vector3 oldMousePos;

    private void Start()
    {
        if (!rb)
            rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * speed;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentMousePos = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {            
            oldMousePos = currentMousePos;
            currentMousePos = Input.mousePosition;

            rb.AddForce(new Vector3(currentMousePos.x - oldMousePos.x, 0f, currentMousePos.y - oldMousePos.y), ForceMode.Acceleration);
        }

        if(Input.GetMouseButtonUp(0))
        {
            oldMousePos = Vector3.zero;
            currentMousePos = Vector3.zero;
        }
    }
}
