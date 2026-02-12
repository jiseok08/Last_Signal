using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public void MainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitScene()
    {
        SceneManager.LoadScene("Intro");
    }
}
