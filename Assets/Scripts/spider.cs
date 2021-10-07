using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : MonoBehaviour
{
    public Transform right_side;
    public Transform left_side;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (move_left());  
    }

    private IEnumerator move_left()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.move(gameObject, new Vector3 (left_side.position.x,transform.position.y, left_side.position.z+0.5f), 5f);
        yield return new WaitForSeconds(5f);
        LeanTween.rotateAround(gameObject, new Vector3(0, 0, 1), 180f, 1f);
        StartCoroutine(move_right());
    }

    private IEnumerator move_right()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.move(gameObject, new Vector3(right_side.position.x, transform.position.y, right_side.position.z + 0.5f), 5f);
        yield return new WaitForSeconds(5f);
        LeanTween.rotateAround(gameObject, new Vector3(0, 0, 1), 180f, 1f);

        StartCoroutine(move_left());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
