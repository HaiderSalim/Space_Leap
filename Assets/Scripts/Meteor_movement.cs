using System;
using UnityEngine;


[RequireComponent(typeof(Meteor_spawner))]
public class Meteor_movement : MonoBehaviour
{
    private enum Direction {RToL, LToR}
    [SerializeField]
    private Direction DirectionType = Direction.RToL;
    [SerializeField, Range(0.01f, 0.5f)]
    private float m_Speed = 0;
    [SerializeField, Range(0.1f, 1f), Header("Direction Adjustment"), Tooltip("Lower the value; lesser the effect of the dimentions of x")]
    private float vertical_Adjust = 0.5f;
    [SerializeField, Range(0.1f, 1f), Tooltip("Lower the value; lesser the effect of the dimentions of y")]
    private float horizontal_Adjust = 0.1f;
    private float direction_Changer = 1f;
    private Game_controller GC;
    private Game_Controller_Data G_cont_data;
    private AudioSource Meteor_audio;
    private float Og_m_Speed;
    private float slowed_m_Speed;

    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
        G_cont_data = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_Controller_Data>();
        Meteor_audio = GameObject.FindGameObjectWithTag("Audio").GetComponentAtIndex<AudioSource>(5);

        Og_m_Speed = m_Speed;
        slowed_m_Speed = m_Speed * G_cont_data.Slow_mo_scale;

        if (DirectionType == Direction.RToL)
        {
            direction_Changer = -direction_Changer;
        }
        else
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void Update()
    {
        if (G_cont_data.is_slowmo_on)
        {
            m_Speed = slowed_m_Speed;
        }
        else 
        {
            m_Speed = Og_m_Speed;
        }

        if (G_cont_data.is_Game_Started && !G_cont_data.is_paused)
            Movement();
    }

    void Movement()
    {
        transform.Translate(new Vector3 (direction_Changer * m_Speed * horizontal_Adjust, -m_Speed * vertical_Adjust, 0), Space.Self);
    }

    void OnTriggerEnter(Collider collider)
    {
        Instantiate(G_cont_data.Meteor_explosion_effect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        if (collider.gameObject.CompareTag("Player"))
        {
            Meteor_audio.Play();
            GC.decrease_Health();
            //Animation and delay will be added
        }
        GetComponent<Meteor_spawner>().Back_to_spawn();
    }
}
