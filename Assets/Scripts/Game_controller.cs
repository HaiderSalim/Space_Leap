using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Game_Controller_Data))]
public class Game_controller : MonoBehaviour
{
    [SerializeField]
    private Game_Controller_Data G_cont_data;

    private List<Level_CheckPoint> CPs;

    void Start()
    {
        CPs = G_cont_data.CheckPoints;//caching
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
}
