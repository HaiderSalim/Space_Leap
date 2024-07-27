using UnityEngine;


[RequireComponent(typeof(Meteor_movement))]
public class Meteor_spawner : MonoBehaviour
{
    [SerializeField]
    private Vector3 Spawn_point;
    
    private Vector3 screenBounds;
    
    void Start()
    {
        Spawn_point = transform.position;
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
        transform.position = Spawn_point;
    }
}
