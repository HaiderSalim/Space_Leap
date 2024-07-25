using System.Collections.Generic;
using UnityEngine;

public class Game_Controller_Data : MonoBehaviour
{
    [TextArea]
    public string Notes = "These data members control the game flow and other progression relates events of the game";

    [Header("Data members of the game"), Tooltip("Tell has the game started")]
    public bool is_Game_Started = false;
    public List<Level_CheckPoint> CheckPoints;
}
