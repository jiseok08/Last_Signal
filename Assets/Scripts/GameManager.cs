using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] WaveManager wave;
    [SerializeField] SpawnManager spawn;

    public WaveManager Wave => wave;
    public SpawnManager Spawn => spawn;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


}
