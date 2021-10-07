using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Line : MonoBehaviour
{
    LineRenderer lineRenderer;
    LineRenderer lineRenderer2;
    public  Vector3 wall_right;
    public  Vector3 wall_left;
    public  Vector3 previos_point;
    public  Vector3[] positions;
    public  Vector3[] positions2;
    public Material mat;
    public Material mat2;

    public GameObject branch_pf;
    public GameObject Axle_pf;
    public GameObject Wall_right;
    public GameObject Wall_left;
    private bool right_turn;
    private bool can_release;
    private bool finish_roll;
    


    public GameObject player;
    [HideInInspector]
    public float move_speed;
    private float x;
    public GameObject holder;
    public Animator anim_player;

    private Camera _camera;

    public static bool player_moved;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
         finish_roll = true;
        can_release = false;
        right_turn = false;
        positions = new Vector3[2];
        positions2 = new Vector3[2];
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer2 = transform.GetChild(0).GetComponent<LineRenderer>();
        wall_right = new Vector3 (0,1,0);
        wall_right = new Vector3 (0,0,0);
        previos_point = wall_right;
        x = 1;

        player_moved = false;
    }

    // Update is called once per frame

    [Obsolete]
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            if (finish_roll && Input.mousePosition.y < _camera.WorldToScreenPoint(player.transform.position).y - 200f)
            { 
            if (right_turn)
            {
                    if (Input.mousePosition.x > Screen.width / 2 + (Screen.width / 20))
                    { 
                    wall_right = new Vector3(Wall_right.transform.position.x, _camera.ScreenToWorldPoint(Input.mousePosition).y, Wall_right.transform.position.z);
                    can_release = true;
                    creatline(wall_right);
                    }
            }
            else
            {
                    if (Input.mousePosition.x < Screen.width / 2 - (Screen.width / 20))
                    { 
                    wall_left = new Vector3(Wall_left.transform.position.x, _camera.ScreenToWorldPoint(Input.mousePosition).y, Wall_left.transform.position.z);
                    can_release = true;
                    creatline(wall_left);
                    }

            }

            }
        }
        if (Input.GetMouseButtonUp(0)&& can_release)
        {
            if (right_turn)
            {
                creat_branch(wall_right);
                right_turn = false;
            }
            else
            {
                creat_branch(wall_left);
                right_turn = true;
            }
            finish_roll = false;
            can_release = false;
        }



       
    }

    [Obsolete]
    public void creatline(Vector3 next_point)
    {
        lineRenderer.enabled = true;
        lineRenderer.material = mat;
        lineRenderer2.enabled = true;
        lineRenderer2.material = mat2;
       
       
        positions[0] = wall_right;
        positions[1] = wall_left;
       
        lineRenderer.SetPositions(positions);

        positions2[0] = new Vector3(player.transform.position.x, player.transform.position.y+0.3f, player.transform.position.z); 
        positions2[1] = new Vector3((next_point.x - 0.2f * x), next_point.y + ((player.transform.position - next_point).magnitude / (20 + ((player.transform.position - next_point).magnitude / 10))) + 0.3f, next_point.z+1f);  
      
        lineRenderer2.SetPositions(positions2);
        lineRenderer2.alignment = LineAlignment.Local;
    }

    public void creat_branch(Vector3 point_A)
    {
       
        anim_player.SetTrigger("roll");
        lineRenderer.enabled = false;
        lineRenderer2.enabled = false;
        Vector3 point_B = previos_point;
        float distance;
     

        distance = (point_B - point_A).magnitude;

        GameObject branch = Instantiate(branch_pf, transform);
        
        branch.transform.localScale = new Vector3(.20f,.20f, distance);
        branch.transform.position = point_A + ((point_B - point_A) / 2);
        branch.transform.LookAt(point_B);
        previos_point = point_A;
        StartCoroutine(roll(point_A,distance));

        GameObject Axle =  Instantiate(Axle_pf,  point_A + new Vector3((x/5), 0, 0.7f), Axle_pf.transform.rotation);
        Axle.transform.parent = transform;

        player_moved = true;// this is only so game will start after the player moved for the first time.
    }
    public IEnumerator roll(Vector3 point_A, float distance)
    {
        yield return new WaitForSeconds(0.1f);
        LeanTween.move(player, point_A + new Vector3((x/5), distance / (20 + (distance / 10)), 0.9f), move_speed);
        x *= -1;


        yield return new WaitForSeconds(move_speed);
        anim_player.SetTrigger("end_roll");
      
        player.transform.localScale = new Vector3(player.transform.localScale.x * -1, player.transform.localScale.y, player.transform.localScale.z);
        yield return new WaitForSeconds(0.5f);
        finish_roll = true;

        
    }
   

}
