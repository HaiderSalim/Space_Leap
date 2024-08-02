using UnityEngine;

public class Sling_shot_mechanic : MonoBehaviour
{
    [Header("Sling Parameters")]
    [SerializeField]
    private float shot_Power = 5;
    [SerializeField]
    private Rigidbody RB;
    [SerializeField]
    private LineRenderer line_Renderer_F;
    [SerializeField]
    private LineRenderer line_Renderer_B;
    [SerializeField]
    private Game_Controller_Data G_control_d;
    [SerializeField]
    private Outline Player_outline;

    public bool is_Sling_able = false;
    private Vector3 startpos; // Vector3 to store the start position
    private float touch_dis;
    private Attach_to_planet attach_To_Planet;
    private bool Apply_slowmo_once = false;

    void Start()
    {
        RB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        RB.useGravity = false;
        attach_To_Planet = RB.gameObject.GetComponent<Attach_to_planet>();
    }

    void Update()
    {
        if (!attach_To_Planet.unattachable)
        {
            is_Sling_able = false;
        }
    }

    void FixedUpdate()
    {
        Sling();
    }

    void Sling()
    {
        if (Input.touchCount > 0 && attach_To_Planet.is_Attached && !G_control_d.is_Game_ended && G_control_d.is_Game_Started)
        {
            var touch = Input.GetTouch(0);
            Vector3 endpos;

            if (touch.phase == TouchPhase.Began &&  RB.transform.parent != null)
            {
                RB.transform.rotation = Quaternion.identity;
                RB.isKinematic = true;
                startpos = Camera.main.ScreenToWorldPoint(touch.position); // Set start position to the hit point
                is_Sling_able = true;
                Player_outline.enabled = true;

                if (!Apply_slowmo_once)
                {
                    foreach (var obj in G_control_d.Planets_rotation_comp)
                    {
                        obj.rotationSpeed *= G_control_d.Slow_mo_scale;
                    }
                    Apply_slowmo_once = true;
                }

                G_control_d.is_slowmo_on = true;
            }
            if (touch.phase == TouchPhase.Moved && is_Sling_able)
            {
                endpos = Camera.main.ScreenToWorldPoint(touch.position); 
                touch_dis = Vector3.Distance(startpos, endpos);
                Vector3 shot_angle = startpos - endpos;
                shot_angle.z = 0;

                var Total_force_F =  0.1f * touch_dis * shot_Power * shot_angle;
                Total_force_F.y = Mathf.Clamp(Total_force_F.y, 0, 4);
                Total_force_F.x = Mathf.Clamp(Total_force_F.x, -4f, 4f);
                var Total_force_B =  0.1f * touch_dis * shot_Power * -shot_angle;
                Total_force_B.y = Mathf.Clamp(Total_force_B.y, -4f, 0f);
                Total_force_B.x = Mathf.Clamp(Total_force_B.x, -4f, 4f);
                //Debug.Log(Total_force_F + " " + Total_force_B);

                line_Renderer_F.SetPosition(1, Total_force_F); // End point of the line
                line_Renderer_B.SetPosition(1, Total_force_B);
            }
            if (touch.phase == TouchPhase.Ended && is_Sling_able)
            {
                endpos = Camera.main.ScreenToWorldPoint(touch.position);
                touch_dis = Vector3.Distance(startpos, endpos);
                Vector3 shot_angle = startpos - endpos;

                Player_outline.enabled = false;
                RB.isKinematic = false;
                RB.velocity = Vector3.zero;
                attach_To_Planet.unattachable = false;

                var Total_force = 0.1f * shot_Power * touch_dis * shot_angle;
                Total_force.y = Mathf.Clamp(Total_force.y, 0, 4f);//locking the force to a limit and also so preventing the player to sling back (There is only one way to go UP!!)
                Total_force.x = Mathf.Clamp(Total_force.x, -4f, 4f);

                foreach (var obj in G_control_d.Planets_rotation_comp)
                {
                    obj.rotationSpeed = obj.OG_rotationSpeed;
                }

                if (Total_force.y > 0)//makes sure that force only applyes when their is an effect to sling so, accindental swips don't registor.
                    RB.AddForce(Total_force, ForceMode.Impulse);
                
                RB.gameObject.transform.SetParent(null);
                is_Sling_able = false;
                line_Renderer_F.SetPosition(1, Vector3.zero); // Reset the line renderer
                line_Renderer_B.SetPosition(1, Vector3.zero);
                attach_To_Planet.is_Attached = false;
                Time.timeScale = 1f;
                G_control_d.is_slowmo_on = false;
                Apply_slowmo_once = false;
            }
        }
        else{
            Player_outline.enabled = false;
            is_Sling_able = false;
            line_Renderer_F.SetPosition(1, Vector3.zero); // Reset the line renderer
            line_Renderer_B.SetPosition(1, Vector3.zero);
            Time.timeScale = 1f;
            G_control_d.is_slowmo_on = false;
        }
    }
}
