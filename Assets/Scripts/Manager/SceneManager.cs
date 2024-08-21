using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : Singleton<SceneManager>
{
    [SerializeField] Image fade;
    [SerializeField] Slider loadingBar;
    [SerializeField] float fadeTime;
    [SerializeField] TMP_Text loadingText;
    [SerializeField] TMP_Text loadingSubText;

    private BaseScene curScene;

    public Slider LoadingBar { get { return loadingBar; } }
    public TMP_Text LoadingText { get { return loadingText; } }

    public BaseScene GetCurScene()
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }
        return curScene;
    }

    public T GetCurScene<T>() where T : BaseScene
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }
        return curScene as T;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        fade.gameObject.SetActive(true);
        yield return FadeOut();

        Manager.Pool.ClearPool();
        Manager.UI.ClearPopUpUI();
        Manager.UI.ClearWindowUI();
        Manager.UI.CloseInGameUI();

        Time.timeScale = 0f;
        loadingBar.gameObject.SetActive(true);
        loadingText.text = "";
        loadingSubText.text = "";

        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        while (oper.isDone == false)
        {
            loadingBar.value = Mathf.Lerp(0, 0.3f, oper.progress);
            yield return null;
        }

        Manager.UI.EnsureEventSystem();

        curScene = GetCurScene();

        loadingBar.value = 0.4f;
        //권새롬 추가 --> 에셋번들 패치
        if (curScene as InGameScene != null)
        {
            yield return AssetBundleManager.Instance.ChapterAssetBundleLoad(curScene.ID,loadingText,loadingSubText);
        }
        loadingBar.value = 0.7f;

        //-------------------------------
        yield return curScene.LoadingRoutine();

        loadingBar.gameObject.SetActive(false);
        
        //예외처리 --> 미니게임에서 타임스케일 1이되면 문제가 됨.
        if(curScene as InGameScene != null || curScene as ChapterScene != null || curScene as LobbyScene != null)
            Time.timeScale = 1f;

        yield return FadeIn();
        fade.gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        float rate = 0;
        Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
        Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

        while (rate <= 1)
        {
            rate += Time.unscaledDeltaTime / fadeTime;
            fade.color = Color.Lerp(fadeInColor, fadeOutColor, rate);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float rate = 0;
        Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
        Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

        while (rate <= 1)
        {
            rate += Time.unscaledDeltaTime / fadeTime;
            fade.color = Color.Lerp(fadeOutColor, fadeInColor, rate);
            yield return null;
        }
    }
}
