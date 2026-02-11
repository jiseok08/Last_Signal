using UnityEngine;
using Unity.Cinemachine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    
    CinemachineCamera cmCam;
    Animator animator;

    void Awake()
    {
        cmCam = GetComponent<CinemachineCamera>();   
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (cmCam == null) return;

        cmCam.Follow = target.transform;
    }

    public void GoField()
    {
        animator.SetTrigger("GoField");
    }

    public void GoLobby()
    {
        animator.SetTrigger("GoLobby");
    }
}
