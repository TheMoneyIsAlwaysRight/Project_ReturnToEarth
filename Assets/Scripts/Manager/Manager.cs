using UnityEngine;

public static class Manager
{
    public static GameManager Game { get { return GameManager.Instance; } }
    public static DataManager Data { get { return DataManager.Instance; } }
    public static PoolManager Pool { get { return PoolManager.Instance; } }
    public static ResourceManager Resource { get { return ResourceManager.Instance; } }
    public static SceneManager Scene { get { return SceneManager.Instance; } }
    public static SoundManager Sound { get { return SoundManager.Instance; } }
    public static UIManager UI { get { return UIManager.Instance; } }
    public static BackendManager Backend { get { return BackendManager.Instance; } }
    public static TutorialManager Tutorial { get { return TutorialManager.Instance; } }
    public static ChapterManager Chapter { get { return ChapterManager.Instance; } }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        GameManager.ReleaseInstance();
        DataManager.ReleaseInstance();
        PoolManager.ReleaseInstance();
        ResourceManager.ReleaseInstance();
        SceneManager.ReleaseInstance();
        SoundManager.ReleaseInstance();
        UIManager.ReleaseInstance();
        JsonPatchManager.ReleaseInstance();
        BackendManager.ReleaseInstance();

        GameManager.CreateInstance();
        DataManager.CreateInstance();
        PoolManager.CreateInstance();
        ResourceManager.CreateInstance();
        SceneManager.CreateInstance();
        SoundManager.CreateInstance();
        UIManager.CreateInstance();
        JsonPatchManager.CreateInstance();
        BackendManager.CreateInstance();
        ChapterManager.CreateInstance(); //정민 추가
        TutorialManager.CreateInstance();
    }
}
