using UnityEngine;

public class Attach_to_planet : MonoBehaviour
{
    public float loc_adject_speed = 0.01f;
    public float unattachable_Delay = 0.1f;
    public bool unattachable = true;
    private Transform current_Attached_planet;
    private float unattachable_Delay_temp;

    [SerializeField]
    private Game_Controller_Data G_control_d;

    void Start()
    {
        unattachable_Delay_temp = unattachable_Delay;
        G_control_d = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Controller_Data>();
    }
    void Update()
    {
        if (!unattachable && unattachable_Delay > 0)
        {
            unattachable_Delay -= Time.deltaTime;
        }
        else
        {
            unattachable = true;
            unattachable_Delay = unattachable_Delay_temp;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet") && unattachable)
        {
            current_Attached_planet = collision.gameObject.GetComponent<Transform>();
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.SetParent(current_Attached_planet, true);
        }
    }
}