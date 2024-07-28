using UnityEngine;

public class Attach_to_planet : MonoBehaviour
{
    public float loc_adject_speed = 0.01f;
    public float unattachable_Delay = 0.1f;
    public bool unattachable = true;//indecates if the player can attach to a planet.
    public bool is_Attached = false;//indecates if the player is attached to a planet.
    private Transform current_Attached_planet;
    private float unattachable_Delay_temp;

    void Start()
    {
        unattachable_Delay_temp = unattachable_Delay;
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
            transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
        else
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
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
}