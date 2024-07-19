using UnityEngine;

public class Attach_to_planet : MonoBehaviour
{
    private Transform current_Attached_planet;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            current_Attached_planet = collision.gameObject.GetComponent<Transform>();
            transform.SetParent(current_Attached_planet, true);
            transform.rotation = Quaternion.identity;
        }
    }
}