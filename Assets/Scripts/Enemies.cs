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
    private AudioSource Fire_audio;
    private AudioSource Slime_audio;

    void Start()
    {
        GC =  GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
        Fire_audio = GameObject.FindGameObjectWithTag("Audio").GetComponentAtIndex<AudioSource>(2);
        Slime_audio = GameObject.FindGameObjectWithTag("Audio").GetComponentAtIndex<AudioSource>(3);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enemy_type == Enemy_type.Fire)
            {
                Fire_audio.Play();
                GC.decrease_Health();
            }
            else
            {
                Slime_audio.Play();
                GC.slow_fuel_regen(Slow_regen_amount, Slow_regen_time);
            }
            gameObject.SetActive(false);
        }
    }
}
