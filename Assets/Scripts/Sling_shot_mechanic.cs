using UnityEngine;

public class Sling_shot_mechanic : MonoBehaviour
{
    [Header("Sling Parameters")]
    [SerializeField]
    private float shot_Power = 5;
    [SerializeField]
    private Rigidbody RB;
    [SerializeField]
    private LayerMask player_Layer;
    [SerializeField]
    private LineRenderer line_Renderer;

    public bool is_Sling_able = false;
    private Vector2 shot_angle;
    private float touch_dis;

    void Start()
    {
    }

    void FixedUpdate()
    {
        Sling();
    }

    void Sling()
    {
        if (Input.touchCount != 0)
        {
            var touch = Input.GetTouch(0);
            Vector3 startpos = Vector3.zero;
            Vector3 endpos; 
            
            var ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray,out hit, 100, player_Layer))
                {
                    startpos = ray.GetPoint(0);
                    is_Sling_able = true;
                }
            }
            if (touch.phase == TouchPhase.Moved && is_Sling_able)
            {
                endpos = ray.GetPoint(0);
                touch_dis = Vector3.Distance((Vector2)startpos, (Vector2)endpos * 10);
                shot_angle = (Vector2)startpos - (Vector2)endpos;

                line_Renderer.SetPosition(1, shot_angle * touch_dis * shot_Power);
            }
            if (touch.phase == TouchPhase.Ended)
            {                
                RB.AddForce(shot_angle * touch_dis * shot_Power, ForceMode.Impulse);
                
                is_Sling_able = false;
                line_Renderer.SetPosition(1, Vector3.zero);
            }
        }
    }
}