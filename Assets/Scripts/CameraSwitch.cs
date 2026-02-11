using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] MainCamera mainCamera;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.GetComponent<MainCamera>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            mainCamera.GoField();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            mainCamera.GoLobby();
        }
    }
}
