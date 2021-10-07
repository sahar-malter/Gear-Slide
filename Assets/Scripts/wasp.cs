using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasp : MonoBehaviour
{
    public Transform right_side;
    public Transform left_side;
    private float up;

    // Start is called before the first frame update
    void Start()
    {
        up = 0.5f;
        StartCoroutine (move_left());
    }
    
    private IEnumerator move_left()
    {
        yield return new WaitForSeconds(0.5f);
        LeanTween.move(gameObject, new Vector3 (left_side.position.x,transform.position.y+up, left_side.position.z+1f), 4);
        yield return new WaitForSeconds(4f);
        LeanTween.rotateAround(gameObject, new Vector3(0, 1, 0), 180f, 1f);
        LeanTween.move(gameObject, new Vector3 (transform.position.x+0.5f,transform.position.y, transform.position.z), 1f);

        up += 0.5f;
        StartCoroutine(move_right());
    }

    private IEnumerator move_right()
    {
        yield return new WaitForSeconds(0.5f);
        LeanTween.move(gameObject, new Vector3(right_side.position.x, transform.position.y+up, right_side.position.z + 1f), 4);
        yield return new WaitForSeconds(4f);
        LeanTween.rotateAround(gameObject, new Vector3(0, 1, 0), 180f, 1f);
        LeanTween.move(gameObject, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), 1f);

        up += 0.5f;


        StartCoroutine(move_left());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
