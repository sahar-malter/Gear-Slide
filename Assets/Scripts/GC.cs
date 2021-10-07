using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GC : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<collsions>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).transform.position.y > player.transform.position.y + 15 && !transform.GetChild(i).CompareTag("line") )
            {
                Destroy(transform.GetChild(i).gameObject);
            }

        }
    }
}
