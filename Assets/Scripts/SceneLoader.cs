using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 간단한 씬 로드 트리거입니다.
/// 사용법: SceneLoader.LoadWithLoadingScreen("Main");
/// 이 메서드는 타겟 씬 이름을 저장한 뒤 `Loading` 씬을 로드합니다.
/// `Loading` 씬의 `LoadingController`가 실제로 타겟 씬을 비동기로 로딩합니다.
/// </summary>
public static class SceneLoader
{
    // 다음에 로드할 씬 이름을 저장합니다.
    public static string TargetSceneName;

    public static void LoadWithLoadingScreen(string sceneName)
    {
        TargetSceneName = sceneName;
        SceneManager.LoadScene("Loading");
    }
}
