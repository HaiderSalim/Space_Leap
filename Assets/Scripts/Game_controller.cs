using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Game_Controller_Data))]
public class Game_controller : MonoBehaviour
{
    [SerializeField]
    private Game_Controller_Data G_cont_data;
    [SerializeField]
    private GameObject Game_win_screen;
    [SerializeField]
    private GameObject Game_lose_screen;
    [SerializeField]
    private Image Fuel_bar_fill;
    [SerializeField]
    private Transform Sun;
    [SerializeField]
    private GameObject Player_rocket_tril; 

    public List<GameObject> Health_Bar;
    private List<Level_CheckPoint> CPs;
    private Attach_to_planet attach_To_Planet;
    private float fuel_slow_delay = 0f;
    private float fuel_Regen_og_amount;
    private bool fuel_is_Slowed = false;

    void Start()
    {
        CPs = G_cont_data.CheckPoints;//caching
        attach_To_Planet = GameObject.FindGameObjectWithTag("Player").GetComponent<Attach_to_planet>();
        fuel_Regen_og_amount = G_cont_data.fuel_Regen_Speed;
    }

    void Update()
    {
        manage_Fuel();
        move_Sun();

        if (fuel_slow_delay <= 0 && fuel_is_Slowed)
        {
            G_cont_data.fuel_Regen_Speed = fuel_Regen_og_amount;
            fuel_slow_delay = 0;
            fuel_is_Slowed = false;
        }
        else if (fuel_is_Slowed)
        {
            fuel_slow_delay -= Time.deltaTime;
        }
    }

    public void ManageCheckPoints()
    {
        if (CPs.Count > 0)
        {
            CPs[0].start_cam_mov = false;        
            CPs.RemoveAt(0);
            CPs[0].start_cam_mov = true;
        }
    }

    private void move_Sun()
    {
        Sun.Translate(new Vector3(0, G_cont_data.Sun_mov_speed * Time.deltaTime, 0), Space.Self);
    }

    public void manage_Fuel()
    {
        if (!attach_To_Planet.is_Attached && !G_cont_data.is_Game_ended)
            Fuel_bar_fill.fillAmount -= G_cont_data.fuel_Useage_Speed * Time.deltaTime;
        else if (!G_cont_data.is_Game_ended)
            Fuel_bar_fill.fillAmount += G_cont_data.fuel_Regen_Speed * Time.deltaTime;

        if (Fuel_bar_fill.fillAmount <= 0)
        {
            Player_rocket_tril.SetActive(false);
            Die();
        }
    }

    public void slow_fuel_regen(float amount, float time)
    {
        fuel_slow_delay = time;
        var newspeed = G_cont_data.fuel_Regen_Speed - amount;
        
        G_cont_data.fuel_Regen_Speed = newspeed;
        fuel_is_Slowed = true;
    }

    public void decrease_Health()
    {
        if (G_cont_data.player_Health > 0 && G_cont_data.player_Health <= Health_Bar.Count)
        {
            Health_Bar[G_cont_data.player_Health - 1].SetActive(false);
            G_cont_data.player_Health--;
        }
        if (G_cont_data.player_Health == 0)
        {
            Die();
        }
    }

    public void increase_Health()
    {
        if (G_cont_data.player_Health > 0 && G_cont_data.player_Health <= Health_Bar.Count)
        {
            Health_Bar[G_cont_data.player_Health - 1].SetActive(true);
            G_cont_data.player_Health++;
        }
    }

    public void GameWin()
    {
        G_cont_data.is_Game_Started = false;
        G_cont_data.is_Game_ended = true;
        Game_win_screen.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Die()
    {
        G_cont_data.is_Game_Started = false;
        G_cont_data.is_Game_ended = true;
        Game_lose_screen.SetActive(true);
    }
}
