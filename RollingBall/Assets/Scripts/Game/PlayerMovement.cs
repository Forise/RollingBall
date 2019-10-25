using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Fields
    public float speed = 1f;
    public Rigidbody rb;
    public float wallPos;

    private bool firstTouch = true;
    private Vector3 currentMousePos;
    private Vector3 oldMousePos;
    private bool enable = true;
    #endregion Fields

    #region Mono Methods
    private void Start()
    {
        if (!rb)
            rb = GetComponent<Rigidbody>();
        GameController.Instance.gameLost += () => { enable = false; };
        GameController.Instance.gameWon += () => { enable = false; };
        GameController.Instance.gameStarted += () => { enable = true; };
    }

    private void FixedUpdate()
    {
        if (enable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (firstTouch)
                {
                    rb.velocity = Vector3.forward * speed;
                    firstTouch = false;
                }
                currentMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                oldMousePos = currentMousePos;
                currentMousePos = Input.mousePosition;

                Vector3 force = new Vector3(currentMousePos.x - oldMousePos.x, 0f, currentMousePos.y - oldMousePos.y);
                rb.AddForce(new Vector3(force.x, 0f, force.z > 0 ? force.z : 0f), ForceMode.Acceleration);
            }

            if (Input.GetMouseButtonUp(0))
            {
                oldMousePos = Vector3.zero;
                currentMousePos = Vector3.zero;

                StopAllCoroutines();
                StartCoroutine(SlowingCoroutine());
            }

            if (transform.position.x > wallPos)
            {
                transform.position = new Vector3(wallPos, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -wallPos)
            {
                transform.position = new Vector3(-wallPos, transform.position.y, transform.position.z);
            }
        }
    }
    #endregion MonoMethods

    #region Methods
    private IEnumerator SlowingCoroutine()
    {
        while (rb.velocity.z > Vector3.forward.z * speed)
        {
            rb.velocity -= Vector3.forward;
            yield return null;
        }
        rb.velocity = Vector3.forward * speed;
    }
    #endregion Methods
}
