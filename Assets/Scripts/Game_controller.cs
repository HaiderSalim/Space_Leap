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
    [SerializeField]
    private GameObject Play_menu;
    [SerializeField]
    private GameObject Level_menu;
    [SerializeField]
    private RectTransform Setting_button;
    [SerializeField, Range(0f, 1f)]
    private float setting_anime_speed;
    [SerializeField]
    private GameObject Volume_btn_on;
    [SerializeField]
    private GameObject Volume_btn_off;
    [SerializeField]
    private GameObject Audio_cont_obj;
    [SerializeField]
    private GameObject Pause_menu;

    private GameObject player;
    public List<GameObject> Health_Bar;
    private List<Level_CheckPoint> CPs;
    private Attach_to_planet attach_To_Planet;
    private float fuel_slow_delay = 0f;
    private float fuel_Regen_og_amount;
    private bool fuel_is_Slowed = false;
    private Sling_shot_mechanic sling_Shot_;
    private GameObject Current_menu_opend;
    private bool is_setting_on = false;
    private Vector2 setting_new_pos;
    private Vector2 setting_og_pos;
    private float setting_anime_delay = 0.5f;
    private bool exposion_once = false;
    private bool pause_once = false;


    void Start()
    {
        if (!(SceneManager.GetActiveScene().buildIndex == PlayerPrefs.GetInt("Current_Level")))
        {
            if (PlayerPrefs.GetInt("Current_Level") > 0)
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("Current_Level"));
            }
            else{
                PlayerPrefs.SetInt("Current_Level", 0);
            }
        }
        

        CPs = G_cont_data.CheckPoints;//caching
        sling_Shot_ = GetComponent<Sling_shot_mechanic>();
        attach_To_Planet = GameObject.FindGameObjectWithTag("Player").GetComponent<Attach_to_planet>();
        player = GameObject.FindGameObjectWithTag("Player");
        fuel_Regen_og_amount = G_cont_data.fuel_Regen_Speed;
        Current_menu_opend = Play_menu;

        setting_new_pos = new Vector2(Setting_button.localPosition.x, Setting_button.localPosition.y -92f);
        setting_og_pos = Setting_button.localPosition;
    }

    void Update()
    {
        manage_Fuel();
        if (G_cont_data.is_Game_Started)
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

        if (!is_setting_on)
        {
            if (setting_anime_delay > 0)
                Animate_setting_button_Backward();
            else
                Setting_button.gameObject.SetActive(false);

            setting_anime_delay-=Time.deltaTime;   
        }
        else{
            Animate_setting_button_Forward();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Open_pause_menu();
        }

        if (!Pause_menu.activeInHierarchy && !pause_once)
        {
            Time.timeScale = 1f;
            G_cont_data.is_paused = false;
        }
    }

    public void Start_Game()
    {
        G_cont_data.is_Game_Started = true;
        sling_Shot_.enabled = true;
        Play_menu.SetActive(false);
        Current_menu_opend = null;
    }

    public void Open_level_Menu()
    {
        Level_menu.SetActive(true);
        Current_menu_opend = Level_menu;
    }

    public void Close_current_menu()//A reusable back button function.
    {
        Current_menu_opend.SetActive(false);
        Current_menu_opend = null;
    }

    public void Open_Settings()
    {
        if (is_setting_on)
        {
            is_setting_on = false;
        }
        else
            is_setting_on = true;
    }

    public void Animate_setting_button_Forward()
    {
        Setting_button.gameObject.SetActive(true);
        var newscale = new Vector2(Setting_button.sizeDelta.x, 300);

        Setting_button.localPosition = Vector2.Lerp(Setting_button.localPosition, setting_new_pos, setting_anime_speed);
        Setting_button.sizeDelta = Vector2.Lerp(Setting_button.sizeDelta, newscale, setting_anime_speed);
        setting_anime_delay = 0.5f;
    }
    public void Animate_setting_button_Backward()
    {
        var newscale = new Vector2(Setting_button.sizeDelta.x, 120);

        Setting_button.localPosition = Vector2.Lerp(Setting_button.localPosition, setting_og_pos, setting_anime_speed);
        Setting_button.sizeDelta = Vector2.Lerp(Setting_button.sizeDelta, newscale, setting_anime_speed);
    }

    public void Mute_volume()
    {
        Volume_btn_on.SetActive(true);
        Volume_btn_off.SetActive(false);
        Audio_cont_obj.SetActive(false);
    }

    public void Unmute_volume()
    {
        Volume_btn_on.SetActive(false);
        Volume_btn_off.SetActive(true);
        Audio_cont_obj.SetActive(true);
    }

    public void Exit_button()
    {
        Application.Quit();
    }

    public void Open_pause_menu()
    {
        Current_menu_opend = Pause_menu;
        Time.timeScale = 0;
        G_cont_data.is_paused = true;
        Pause_menu.SetActive(true);
        pause_once = true;
    }

    public void Next_Level()
    {
        if (SceneManager.GetActiveScene().buildIndex == PlayerPrefs.GetInt("Current_Level"))
        {
            PlayerPrefs.SetInt("Current_Level", PlayerPrefs.GetInt("Current_Level") + 1);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("Current_Level"));
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
        if (!exposion_once)
        {
            Instantiate(G_cont_data.Explosion_effect, player.transform.position, Quaternion.identity);
            exposion_once = true;
        }
        player.SetActive(false);
    }
}
