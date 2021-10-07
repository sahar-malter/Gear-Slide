using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_lights : MonoBehaviour
{
    public List<GameObject> bars_list;
    // Start is called before the first frame update
    void Start()
    {
        //offlights();
    }

    // Update is called once per frame
    void Update()
    {
        lights();
    }
    //public void offlights()
    //{
    //    for (int i = 0; i < bars_list.Count; i++)
    //    {
    //        bars_list[i].DisableKeyword("_EMISSION");
    //    }
    //}
    public void lights()
    {
        if (GameManager.final_score / 50 > 0)
        {
          LeanTween.moveLocalX(bars_list[0].gameObject,-0.3f,0.1f);
        }
        if (GameManager.final_score / 50 > 0.15f)
        {
           LeanTween.moveLocalX(bars_list[1].gameObject, -0.5f, 0.1f);
        }
        if (GameManager.final_score / 50 > 0.3f)
        {
            LeanTween.moveLocalX(bars_list[2].gameObject, -0.5f, 0.1f);
        }
        if (GameManager.final_score / 50 > 0.45)
        {
            LeanTween.moveLocalX(bars_list[3].gameObject, -0.5f, 0.1f);
        }
        if (GameManager.final_score / 50 > 0.6)
        {
            LeanTween.moveLocalX(bars_list[4].gameObject, -0.5f, 0.1f);
        }
        if (GameManager.final_score / 50 > 0.75)
        {
            LeanTween.moveLocalX(bars_list[5].gameObject, -0.45f, 0.1f);
            LeanTween.moveLocalY(bars_list[5].gameObject, 5.6f, 0.1f);
            LeanTween.scale(bars_list[5].gameObject,new Vector3(-1.6f,1.6f,1.6f), 0.1f);
        }

    }
}
