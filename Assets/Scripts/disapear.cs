using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disapear : MonoBehaviour
{
    private Color col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Renderer>().material.color;
        col.a = Mathf.Clamp(col.a, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        col.a = Mathf.Clamp(col.a, 0, 1);
        col.a -=(Time.deltaTime/5);
        GetComponent<Renderer>().material.color = col;
    }
}
