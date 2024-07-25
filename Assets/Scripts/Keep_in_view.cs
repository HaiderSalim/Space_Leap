using UnityEngine;

public class Keep_in_view : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Vector3 viewPos = transform.position;

        if (viewPos.x > screenBounds.x)
        {
            viewPos.x = -screenBounds.x;
        }
        else if (viewPos.x < -screenBounds.x)
        {
            viewPos.x = screenBounds.x;
        }

        // if (viewPos.y > screenBounds.y)
        // {
        //     viewPos.y = -screenBounds.y;
        // }
        // else if (viewPos.y < -screenBounds.y)
        // {
        //     viewPos.y = screenBounds.y;
        // }

        transform.position = viewPos;
    }

}
