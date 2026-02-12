using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Loading 씬에서 사용되는 컨트롤러입니다.
/// `SceneLoader.TargetSceneName`에 설정된 씬을 비동기 로드하고 UI를 업데이트합니다.
///</summary>
public class LoadingController : MonoBehaviour
{
    [Tooltip("Optional UI Slider for progress (0..1)")]
    public Slider progressBar;

    [Tooltip("Optional text UI to show percent. Any Text component can be used by assigning it via inspector.")]
    public UnityEngine.UI.Text percentText;

    IEnumerator Start()
    {
        string target = SceneLoader.TargetSceneName;
        if (string.IsNullOrEmpty(target))
        {
            Debug.LogWarning("SceneLoader.TargetSceneName is empty. Falling back to 'Main'.");
            target = "Main";
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(target);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            float p = Mathf.Clamp01(op.progress / 0.9f);
            UpdateUI(p);
            yield return null;
        }

        UpdateUI(1f);
        // 잠깐 대기 후 씬 활성화
        yield return new WaitForSeconds(0.25f);
        op.allowSceneActivation = true;
    }

    void UpdateUI(float normalized)
    {
        if (progressBar != null)
            progressBar.value = normalized;

        if (percentText != null)
            percentText.text = Mathf.RoundToInt(normalized * 100f) + "%";
    }
}
