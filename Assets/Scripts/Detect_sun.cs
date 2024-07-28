using UnityEngine;

public class Detect_sun : MonoBehaviour
{
    private Game_controller GC;

    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sun"))
        {
            GC.Die();
        }
    }
}
