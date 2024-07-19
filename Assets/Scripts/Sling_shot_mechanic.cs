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
    private Vector3 startpos; // Vector3 to store the start position
    private float touch_dis;

    void Start()
    {
        RB.useGravity = false;
    }

    void FixedUpdate()
    {
        Sling();
    }

    void Sling()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            Vector3 endpos; 

            var ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit, 100, player_Layer))
                {
                    RB.gameObject.transform.SetParent(null);
                    RB.transform.rotation = Quaternion.identity;
                    RB.isKinematic = true;
                    startpos = hit.point; // Set start position to the hit point
                    is_Sling_able = true;
                }
            }
            if (touch.phase == TouchPhase.Moved && is_Sling_able)
            {
                endpos = ray.GetPoint(10); // Adjust the depth of the ray intersection
                touch_dis = Vector3.Distance(startpos, endpos);
                Vector3 shot_angle = startpos - endpos;

                //line_Renderer.SetPosition(0, startpos); // Start point of the line
                line_Renderer.SetPosition(1, shot_angle * touch_dis * shot_Power * 0.1f); // End point of the line
            }
            if (touch.phase == TouchPhase.Ended && is_Sling_able)
            {
                endpos = ray.GetPoint(10); // Adjust the depth of the ray intersection
                touch_dis = Vector3.Distance(startpos, endpos);
                Vector3 shot_angle = startpos - endpos;

                RB.isKinematic = false;
                RB.velocity = Vector3.zero;
                RB.AddForce(shot_angle * touch_dis * shot_Power * 0.1f, ForceMode.Impulse);

                is_Sling_able = false;
                line_Renderer.SetPosition(1, Vector3.zero); // Reset the line renderer
            }
        }
    }
}
