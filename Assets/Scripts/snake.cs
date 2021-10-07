using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour
{
    public float speed;
    private float speed2;
    // Start is called before the first frame update
    void Start()
    {
        speed2 = speed;
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    private void FixedUpdate()
    {
       if (speed2 > 0)
        {
            speed2 -= 0.3f;
        }
        else
        {
            speed2 = speed;
        }

        transform.position += Vector3.up* speed2 * Time.fixedDeltaTime;
    }
}
