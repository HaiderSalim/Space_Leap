using UnityEngine;


[RequireComponent(typeof(Meteor_movement))]
public class Meteor_spawner : MonoBehaviour
{
    [SerializeField]
    private Vector3 Spawn_point;
    [SerializeField, Range(0f, 5f)]
    private float Spawn_delay = 0.5f;

    
    private Vector3 screenBounds;
    private float delay_temp;
    
    void Start()
    {
        Spawn_point = transform.position;
        delay_temp = Spawn_delay;
    }

    void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        Spawn_back();
    }

    void Spawn_back()
    {
        Vector3 viewPos = transform.position;
        var is_inscreen = true;

        if (viewPos.x > screenBounds.x)
        {
            is_inscreen = false;
        }
        else if (viewPos.x < -screenBounds.x)
        {
            is_inscreen = false;
        }

        if (viewPos.y > screenBounds.y)
        {
            is_inscreen = false;
        }
        else if (viewPos.y < -screenBounds.y)
        {
            is_inscreen = false;
        }

        if (!is_inscreen)
        {
            Back_to_spawn();
        }
    }

    public void Back_to_spawn()
    {
        if(Spawn_delay <= 0f)
        {
            gameObject.SetActive(true);
            transform.position = Spawn_point;
            Spawn_delay = delay_temp;
        }
        else{
            Spawn_delay -= Time.deltaTime;
        }
    }
}
