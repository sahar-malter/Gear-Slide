using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spowner : MonoBehaviour
{
    public static List<GameObject> Objects_to_spowen;
    private int level;
    private float level_lengh;

    public float time_between_spowns;
    public List<GameObject> spowners;
    public GameObject spowners_parent;

    public GameObject[] enemies_pf;
    //public int[] caterpiller_chance;
    private bool activated_spowners;


    // Start is called before the first frame update

    void Start()
    {
        level_lengh = FindObjectOfType<GameManager>().Level_Lengh[GameManager.player_level];
        time_between_spowns = (10 - (FindObjectOfType<GameManager>().holder_speed[level]*3f));
        level = GameManager.player_level;
        
        activated_spowners = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Line.player_moved == true && activated_spowners == false)
        {
            activate_spowners();
            activated_spowners = true;
        }

        
    }
  
    public IEnumerator spown()
    {
         if (transform.position.y >= (Vector3.down * level_lengh).y+10)
        { 
          GameObject item =  Instantiate(Objects_to_spowen[randomizer(Objects_to_spowen.Count)], spowners[0].transform.position, spowners[0].transform.rotation);
          item.transform.parent = spowners_parent.transform;
          yield return new WaitForSeconds((time_between_spowns));
          StartCoroutine(spown());
         }

    }
    public IEnumerator enemy_spown()
    {
       
            if (transform.position.y >= (Vector3.down * level_lengh).y+10)
            {
                //int temp = Random.Range(0, 10 + caterpiller_chance[level]);
                //if (temp > 10)
                //{
                //    GameObject item = Instantiate(caterpiller_pf, spowners[randomizer(spowners.Count)].transform.position, spowners[randomizer(spowners.Count)].transform.rotation);
                //    item.transform.parent = spowners_parent.transform;
                //}

                GameObject item = Instantiate(enemies_pf[randomizer(enemies_pf.Length)], spowners[0].transform.position, spowners[0].transform.rotation);
                item.transform.parent = spowners_parent.transform;




             yield return new WaitForSeconds(timer_randomizer(time_between_spowns * 6));
             StartCoroutine(enemy_spown());
            }
    }
    public int randomizer(int limit)
    {
        int num = Random.Range(0, limit);
        return num;
    }
    public float timer_randomizer(float limit)
    {
        float num = Random.Range(0.5f, limit);
        return num;
    }

    public void activate_spowners()
    {
        StartCoroutine(spown());
        StartCoroutine(enemy_spown());
    }

}
