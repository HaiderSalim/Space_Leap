using Unity.VisualScripting;
using UnityEngine;

public class Black_hole : MonoBehaviour
{
    [SerializeField, Header("Black Hole parameters"), Range(0.1f, 1)]
    private float radius = 0.7f;
    [SerializeField, Tooltip("The on which bases the force will be calculated"), Range(0.1f, 10)]
    private float Gravitation_const = 9.8f;
    [SerializeField, Range(1, 10)]
    private float Blackhole_mass = 10f;
    [SerializeField]
    private LayerMask player_mask;

    private Transform Radius_ring;
    private float Player_mass = 0f;
    private float Pull_force = 0f;
    private Vector3 Pull_direction;
    private Game_controller GC;
    private AudioSource Black_h_audio;

    private bool once = false;

    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
        Black_h_audio = GameObject.FindGameObjectWithTag("Audio").GetComponentAtIndex<AudioSource>(5);
        Radius_ring = transform.GetChild(0);
        Radius_ring.localScale = new Vector3(radius + 0.15f, radius + 0.15f, 1);
    }

    void FixedUpdate()
    {
        var colliders = Physics.OverlapSphere(transform.position, radius, player_mask);
        if (colliders.Length > 0 && !once)
        {
            Black_h_audio.Play();
            once = true;
        }
        foreach (var collider in colliders)
        {
            var RB = collider.gameObject.GetComponent<Rigidbody>();
            Pull_direction = collider.transform.position - transform.position;
            Player_mass = RB.mass;

            Pull_force = Gravitation_const * (Player_mass * Blackhole_mass / radius*radius);

            RB.AddForce(-Pull_force * Pull_direction, ForceMode.Force);
        }
        if (colliders.Length <= 0 && once)
        {
            Black_h_audio.Stop();
            once = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GC.Die();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
