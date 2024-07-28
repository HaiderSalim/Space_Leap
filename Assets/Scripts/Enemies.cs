using UnityEngine;

public class Enemies : MonoBehaviour
{
    private enum Enemy_type {Fire, Slime};
    [SerializeField]
    private Enemy_type enemy_type = Enemy_type.Fire;
    [SerializeField, Range(0f, 0.5f)]
    private float Slow_regen_amount = 0.1f;
    [SerializeField, Range(1f, 10f)]
    private float Slow_regen_time = 1f;

    private Game_controller GC;

    void Start()
    {
        GC =  GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enemy_type == Enemy_type.Fire)
            {
                GC.decrease_Health();
            }
            else
            {
                GC.slow_fuel_regen(Slow_regen_amount, Slow_regen_time);
            }
            gameObject.SetActive(false);
        }
    }
}
