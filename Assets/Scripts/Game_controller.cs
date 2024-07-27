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

    public List<GameObject> Health_Bar;
    private List<Level_CheckPoint> CPs;
    private Attach_to_planet attach_To_Planet;

    void Start()
    {
        CPs = G_cont_data.CheckPoints;//caching
        attach_To_Planet = GameObject.FindGameObjectWithTag("Player").GetComponent<Attach_to_planet>();
    }

    void Update()
    {
        decrease_Fuel();
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

    public void decrease_Fuel()
    {
        if (!attach_To_Planet.is_Attached && !G_cont_data.is_Game_ended)
            Fuel_bar_fill.fillAmount -= G_cont_data.fuel_Useage_Speed * Time.deltaTime;
        else if (!G_cont_data.is_Game_ended)
            Fuel_bar_fill.fillAmount += G_cont_data.fuel_Regen_Speed * Time.deltaTime;

        if (Fuel_bar_fill.fillAmount <= 0)
            Die();
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
