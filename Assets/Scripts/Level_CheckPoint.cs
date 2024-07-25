using UnityEngine;

public class Level_CheckPoint : MonoBehaviour
{
    [SerializeField]
    private float cam_Movespeed = 0.1f;
    
    public bool start_cam_mov = false;
    private Vector3 cam_Stopping_pos;
    private Camera maincam;

    void Start()
    {
        maincam = Camera.main;
        cam_Stopping_pos = transform.position;
    }

    void Update()
    {
        if (start_cam_mov)
            Move_cam();
    }

    void  Move_cam()
    {
        maincam.transform.position = new Vector3(
            Mathf.Lerp(maincam.transform.position.x, cam_Stopping_pos.x, cam_Movespeed),
         Mathf.Lerp(maincam.transform.position.y, cam_Stopping_pos.y, cam_Movespeed),
         maincam.transform.position.z);
    }
}
