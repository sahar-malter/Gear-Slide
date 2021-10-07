using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int player_level;
    public GameObject holder;
    public GameObject END_LAND;

    public float[] holder_speed;
    public float[] Level_Lengh;
    public float player_shild_time;
    public float[] player_speed;
    public Slider level_progress;

    public TextMeshProUGUI level_text;
    public TextMeshProUGUI score_text;
    public static int score;
    public GameObject player;


    public static bool won;
    public Image finish_acorn_image;
    public static float final_score;
    public GameObject basket;
    public ParticleSystem finish_particles;
    // Start is called before the first frame update
  

    void Start()
    {
        
        if (PlayerPrefs.HasKey("player_level"))
        {
            player_level = PlayerPrefs.GetInt("player_level");
        }
        else
        {
            PlayerPrefs.SetInt("player_level", 0);
            player_level = 0;
        }
        FindObjectOfType<collsions>().Shield_time = player_shild_time;
        FindObjectOfType<Line>().move_speed = player_speed[player_level];
        creat_finish();

     level_text.text = "Level " +(player_level + 1);
        score = 0;
        final_score = 0;
        won = false;
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = score.ToString();
        level_progress.value = (player.transform.position.y / ((Vector3.down * Level_Lengh[player_level]).y-10f));
        if (won)
        {
            won = false;
            StartCoroutine(Finish());
        }
        finish_acorn_image.fillAmount = (final_score / 50);
    }

    private void FixedUpdate()
    {
        if (Line.player_moved == true)
        {
            if (holder.transform.position.y >= ((Vector3.down * Level_Lengh[player_level]).y)-2f)
            {

                holder.transform.position += (Vector3.down * holder_speed[player_level]) * Time.fixedDeltaTime;
            }
        }
    }

    public void creat_finish()
    {
        END_LAND.transform.position = new Vector3(END_LAND.transform.position.x, (-1 * Level_Lengh[player_level])-10, END_LAND.transform.position.z);
    }

    public static void reset_level()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void reset_level_button()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public static IEnumerator next_level()
    {
        PlayerPrefs.SetInt("player_level", player_level + 1);
        yield return new WaitForSeconds(12f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    [ContextMenu("reset_pref")]
    public void reset_pref()
    {
        PlayerPrefs.DeleteAll();
        reset_level();
    }

    public IEnumerator Finish()
    {
        basket.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        var main = finish_particles.main;
        main.duration = (0.4f * (score / 10f));
        finish_particles.Play();
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < score; i++)
        {
            final_score++;
            yield return new WaitForSeconds(0.15f);
        }

    }


}
