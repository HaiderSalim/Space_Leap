using System.Collections.Generic;
using UnityEngine;

public class Game_Controller_Data : MonoBehaviour
{
    [TextArea]
    public string Notes = "These data members control the game flow and other progression relates events of the game";

    [Header("Data members of the game"), Tooltip("Tell has the game started")]
    public bool is_Game_Started = false;
    public bool is_Game_ended = false;
    public List<Level_CheckPoint> CheckPoints;
    [Range(3, 5)]
    public int player_Health = 3;
    [Range(0.1f, 1f)]
    public float fuel_Useage_Speed = 0.5f;
    [Range(0.1f, 1f)]
    public float fuel_Regen_Speed = 0.5f;
    [Range(0.1f, 1f)]
    public float Sun_mov_speed;
    public bool is_slowmo_on = false;

    void Start()
    {
        player_Health = GetComponent<Game_controller>().Health_Bar.Count;
    }
}
