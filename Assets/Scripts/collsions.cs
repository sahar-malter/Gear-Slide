using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collsions : MonoBehaviour
{
    private bool have_shild;
    [HideInInspector]
    public float Shield_time;
    public GameObject shield_particle;
    public GameObject acorn_pf;
    public GameObject score_location;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (have_shild)
        {
            shield_particle.SetActive(true);
        }
        else
        {
            shield_particle.SetActive(false);
        }
       
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shield"))
        {
            other.GetComponent<AudioSource>().Play();
            StopCoroutine(shield());
            StartCoroutine(shield());
            Destroy(other.gameObject,0.1f);
        }
        if (other.CompareTag("acorn"))
        {
            other.GetComponent<AudioSource>().Play();
            other.GetComponent<Collider>().enabled = false;
            GameManager.score++;
            LeanTween.rotateAround(other.gameObject, new Vector3(0,0,1),360, 0.5f);
            LeanTween.scale(other.gameObject, new Vector3(0, 0, 0), 0.6f);
            LeanTween.move(other.gameObject,new Vector3(score_location.transform.position.x, score_location.transform.position.y-1, other.gameObject.transform.position.z),0.5f);
            Destroy(other.gameObject,0.6f);
        }
        if (other.CompareTag("golden acorn"))
        {
            other.GetComponent<AudioSource>().Play();
            other.GetComponent<Collider>().enabled = false;
            GameManager.score++;
            GameManager.score++;
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            LeanTween.rotateAround(other.gameObject, new Vector3(0, 0, 1), 360, 0.5f);
            LeanTween.scale(other.gameObject, new Vector3(0, 0, 0), 0.6f);
            LeanTween.move(other.gameObject, new Vector3(score_location.transform.position.x, score_location.transform.position.y - 1, other.gameObject.transform.position.z), 0.5f);
            Destroy(other.gameObject,0.6f);
        }
        if (other.CompareTag("trap"))
        {
            if (!have_shild)
            {
                StartCoroutine(player_death());
            }
            else
            {
                other.GetComponent<AudioSource>().Play();
                other.GetComponent<Animator>().SetTrigger("destroy");
                StartCoroutine(hit_object(other.gameObject));

            }


        }
        if (other.CompareTag("caterpiller"))
        {
            if (!have_shild)
            {
                StartCoroutine(player_death());
            }
            else
            {
                other.GetComponent<ParticleSystem>().Play();
                Destroy(other.transform.parent.gameObject);
            }

        }
        if (other.CompareTag("web"))
        {
            other.GetComponent<AudioSource>().Play();
            StartCoroutine(web_pull());
            StartCoroutine(hit_object(other.gameObject));


        }
        if (other.CompareTag("transformer"))
        {
            other.GetComponent<AudioSource>().Play();
            other.GetComponent<Animator>().enabled = false;
            StartCoroutine(transformer());
            StartCoroutine(hit_object(other.gameObject));


        }
        if (other.CompareTag("Finish"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(finish(other.transform.parent.transform.GetChild(1).gameObject));
        }
        if (other.CompareTag("top"))
        {
           
                StartCoroutine(player_death());

        }
    }

    public IEnumerator hit_object(GameObject item)
    {
        
        item.GetComponent<Collider>().enabled = false;
        item.GetComponent<MeshRenderer>().enabled = false;
        item.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(0.5f);
        Destroy(item.gameObject);
    }

    public IEnumerator finish(GameObject ground)
    {
     GetComponent<Collider>().enabled = false;
     FindObjectOfType<Line>().enabled = false;
     yield return new WaitForSeconds(0.5f);
      GetComponent<Animator>().SetTrigger("win");
     LeanTween.move (gameObject,new Vector3 (ground.transform.position.x-1, ground.transform.position.y + 0.25f, ground.transform.position.z+3f),1f);
     LeanTween.move (Camera.main.gameObject,new Vector3 (Camera.main.transform.position.x, ground.transform.position.y + 0.5f, Camera.main.transform.position.z),1f);
        GameManager.won = true;
     StartCoroutine(GameManager.next_level());
    }

    [System.Obsolete]
    public IEnumerator shield()
    {

        //particles
        have_shild = true;
        yield return new WaitForSeconds(Shield_time);
        have_shild = false;
    }

    public IEnumerator web_pull()
    {
        yield return new WaitForSeconds(0.5f);
        Collider[] first_hit = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider hitCollider in first_hit)
        {
            if (hitCollider.CompareTag("acorn"))
            {

                LeanTween.move(hitCollider.gameObject, transform.position, 0.3f);

            }
        }
       

    }
    public IEnumerator transformer()
    {
        yield return new WaitForSeconds(0f);
        Collider[] first_hit = Physics.OverlapSphere(transform.position, 6f);
        foreach (Collider hitCollider in first_hit)
        {
            if (hitCollider.CompareTag("trap"))
            {
                StartCoroutine(hit_object(hitCollider.gameObject));
                Instantiate(acorn_pf, hitCollider.transform.position, hitCollider.transform.rotation);
                hitCollider.gameObject.GetComponent<Animator>().SetTrigger("destroy");

            }
        }
    }

    public IEnumerator player_death()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<Collider>().enabled = false;
        FindObjectOfType<Line>().enabled = false;
        LeanTween.move(gameObject, (transform.position + new Vector3(0, 2f, 1f)), 0.5f);
        transform.LookAt(Camera.main.transform);
        GetComponent<Animator>().SetTrigger("death");
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(2f);
        GameManager.reset_level();

    }
}
