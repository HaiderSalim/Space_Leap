using UnityEngine;

public class Move_cam : MonoBehaviour
{
    private Game_controller GC;

    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GC.ManageCheckPoints();
            gameObject.SetActive(false);
        }
    }
}
