using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public Rigidbody rb;
    public float maxspeed;
    public float minspeed;
    public float lx=-5f;
    public float rx=5f;
    private int dir = -1;


    public float waitDuration = 1.0f; 
    private bool isWaiting = false;
    private float waitTimer = 0f;

    void Start()
    {
        transform.position = new Vector3(rx, 0, 0);
       

    }


    void FixedUpdate()
    {
        float cex = (lx + rx) / 2f;
        float cux = transform.position.x;
        float distoce = Mathf.Abs(cex - cux);
        float sidetoce = Mathf.Abs(cex - lx);
        float cuspeed = Mathf.Max( maxspeed * (distoce / sidetoce),minspeed);

        rb.velocity = new Vector3(dir*cuspeed, 0, 0);
        


        if (isWaiting)
        {
            waitTimer += Time.fixedDeltaTime;
            rb.velocity = Vector3.zero;
            if (waitTimer >= waitDuration)
            {
                isWaiting = false;
                waitTimer = 0f;
            }
            return;
        }

        
        rb.velocity = new Vector3(dir * cuspeed, 0, 0);

        
        bool hitLeft = (dir == -1 && cux <= lx);
        bool hitRight = (dir == 1 && cux >= rx);
        if (hitLeft || hitRight)
        {
            isWaiting = true;
            waitTimer = 0f;
            rb.velocity = Vector3.zero;
        }


        if (dir == -1 && cux <= lx)
        {
            dir = 1;
        }
        if (dir == 1 && cux >= rx)
        {
            dir = -1;
        }
    }
}
