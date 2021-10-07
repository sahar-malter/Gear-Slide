using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creat_branch : MonoBehaviour
{
    public Vector3 point_A;
    public Vector3 point_B;
    public float distance;

    void Start()
    {
        // Assuming this is run on a unit cube.
       
    }



    // Update is called once per frame
    void Update()
    {


    }

    public void branch()
    {
        distance = (point_B - point_A).magnitude;
        transform.GetChild(0).localScale = new Vector3(distance, transform.GetChild(0).localScale.y, transform.GetChild(0).localScale.z);
        transform.GetChild(0).position = point_A + ((point_B - point_A) / 2);
        transform.GetChild(0).LookAt(point_B);

    }
}
