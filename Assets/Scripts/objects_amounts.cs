using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objects_amounts : MonoBehaviour
{
    public int[] Collect;
    public int[] Easy;
    public int[] Medium;


    public List <GameObject> Collect_only_objects;
    public List <GameObject> Easy_mix_objects;
    public List <GameObject> Medium_mix_objects;
    

 
    public List<GameObject> list_to_pass;


    // Start is called before the first frame update
    private void Awake()
    {
      creat_list();  
    }
    void Start()
    {
        
    }

    private void creat_list()
    {
        for (int i = 0; i < Collect[GameManager.player_level]; i++)
        {
            for (int k = 0; k < Collect_only_objects.Count; k++)
            {
             list_to_pass.Add(Collect_only_objects[k]);
            }
            
        }

        for (int i = 0; i < Easy[GameManager.player_level]; i++)
        {
            for (int k = 0; k < Easy_mix_objects.Count; k++)
            {
                list_to_pass.Add(Easy_mix_objects[k]);
            }

        }

        for (int i = 0; i < Medium[GameManager.player_level]; i++)
        {
            for (int k = 0; k < Medium_mix_objects.Count; k++)
            {
                list_to_pass.Add(Medium_mix_objects[k]);
            }

        }

        spowner.Objects_to_spowen = list_to_pass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
