using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public CanvasGroup canvasGroup; // 페이드 효과를 위한 CanvasGroup
    public float fadeDuration = 1.0f; // 페이드 지속 시간

    void Start()
    {
        StartCoroutine(FadeIn()); // 게임 시작 시 페이드인
    }

    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeIn()
    {
        canvasGroup.interactable = false; // 페이드아웃 중에는 UI 조작 방지
        canvasGroup.blocksRaycasts = false;

        float time = fadeDuration;
        while (time > 0)
        {
            time -= Time.deltaTime;
            canvasGroup.alpha = time / fadeDuration;
            yield return null;
        }

        canvasGroup.alpha = 0;
    }
    public void ReloadCurrentScene()
    {
        // 현재 씬 이름을 가져옵니다.
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // 현재 씬을 리로드합니다.
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }
}
