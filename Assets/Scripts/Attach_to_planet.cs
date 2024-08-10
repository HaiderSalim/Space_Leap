using UnityEngine;

public class Attach_to_planet : MonoBehaviour
{
    public GameObject rockettrail;
    public float loc_adject_speed = 0.01f;
    public float unattachable_Delay = 0.1f;
    public bool unattachable = true;//indecates if the player can attach to a planet.
    public bool is_Attached = false;//indecates if the player is attached to a planet.
    private Transform current_Attached_planet;
    private float unattachable_Delay_temp;
    private Game_Controller_Data G_cont_data;

    [Header("Animation Parameters"), SerializeField, Range(0.001f, 0.1f)]
    private float idel_Anime_speed;
    [SerializeField, Range(0.1f, 1f)]
    private float Anime_direction_change_time = 0.5f;
    private bool is_Anime_move_up = true;
    private float Anime_direction_change_time_temp;

    void Start()
    {
        G_cont_data = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Controller_Data>();
        unattachable_Delay_temp = unattachable_Delay;
        rockettrail = transform.GetChild(2).gameObject;
        Anime_direction_change_time_temp = Anime_direction_change_time;
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

        if (transform.parent != null)
        {
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            GetComponent<Keep_in_view>().enabled = false;
            rockettrail.SetActive(false);

            if (!G_cont_data.is_slowmo_on)
                Player_idel_anime();
        }
        else
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            GetComponent<Keep_in_view>().enabled = true;
            rockettrail.SetActive(true);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet") && unattachable)
        {
            current_Attached_planet = collision.gameObject.GetComponent<Transform>();
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.SetParent(current_Attached_planet, true);
            is_Attached = true;
        }
    }

    void Player_idel_anime()
    {
        if(Anime_direction_change_time >= 0 && is_Anime_move_up)
        {
            transform.Translate(new Vector3(0, idel_Anime_speed, 0), Space.Self);
        }
        else
        {
            is_Anime_move_up = false;
        }
        if(Anime_direction_change_time <= Anime_direction_change_time_temp && !is_Anime_move_up){
            transform.Translate(new Vector3(0, -idel_Anime_speed, 0), Space.Self);
        }
        else
        {
            is_Anime_move_up = true;
        }
        if(is_Anime_move_up)
        {
            Anime_direction_change_time -= Time.deltaTime;
        }
        else
        {
            Anime_direction_change_time += Time.deltaTime;
        }
    }
}