using UnityEngine;

/// <summary>
/// Intro 씬에서 버튼에 연결해서 사용합니다.
/// 예: UI Button OnClick에 `IntroStarter.OnStartButton`을 연결하세요.
///</summary>
public class IntroStarter : MonoBehaviour
{
    [Tooltip("로딩화면을 통해 열 씬 이름 (예: Main)")]
    public string sceneToOpen = "Main";

    public void OnStartButton()
    {
        SceneLoader.LoadWithLoadingScreen(sceneToOpen);
    }
}
