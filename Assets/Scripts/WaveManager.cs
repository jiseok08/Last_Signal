using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] public int wave = 0;

    public void WaveUp()
    {
        wave++;
    }
}
