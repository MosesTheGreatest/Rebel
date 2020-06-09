using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject strength;
    private bool isHeld = false;
    private Vector3 mousePosCurrent;
    private Vector3 mousePosDown;
    private Vector3 mousePosUp;
    private Vector3 scale;
    private Vector3 scaleLimit;
    private float direction;
    private Vector3 force;
    private Vector3 forceLimit;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        strength.gameObject.SetActive(false);
    } 

    void FixedUpdate()
    {
        mousePosCurrent = Input.mousePosition;
        mousePosCurrent = Camera.main.ScreenToWorldPoint(mousePosCurrent);

        if (isHeld)
        {
            strength.gameObject.SetActive(true);
            Vector3 scaleLimit = mousePosCurrent - mousePosDown;
            if  (scaleLimit.magnitude < 2.1f)
            {
                scale = scaleLimit;
            } else
            {
                scale = scaleLimit.normalized * 2.1f;
            }
            strength.transform.localScale = new Vector3(1.5f, -scale.magnitude * 2, 1);
            strength.transform.up = (scale);
        }
        
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isHeld = true;
            mousePosDown = Input.mousePosition;
            mousePosDown = Camera.main.ScreenToWorldPoint(mousePosDown);
        }
        
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            strength.gameObject.SetActive(false);
            mousePosUp = Input.mousePosition;
            mousePosUp = Camera.main.ScreenToWorldPoint(mousePosUp);

            Vector3 direction = mousePosDown - mousePosUp;
            float distance = direction.magnitude;

            float forceMagnitude = distance * 2;
            forceLimit = (direction * 2) * forceMagnitude;
            rb.constraints = RigidbodyConstraints2D.None;
            Launch(rb);
            isHeld = false;
        }
    }

    void Launch(Rigidbody2D rb)
    {
        if (forceLimit.magnitude < 14.0f)
        {
            force = forceLimit;
        }
        else
        {
            force = forceLimit.normalized * 14.0f;
        }
        rb.AddForce(force * 2);
        
    }

}
