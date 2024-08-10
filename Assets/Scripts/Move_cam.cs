using UnityEngine;

public class Move_cam : MonoBehaviour
{
    [SerializeField]
    private bool is_Win_trigger = false;
    private Game_controller GC;

    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (is_Win_trigger)
            {
                GC.GameWin();
                collider.gameObject.SetActive(false);
            }
            else
            {
                GC.ManageCheckPoints();
                gameObject.SetActive(false);
            }
        }
    }
}
