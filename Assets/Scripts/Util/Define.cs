using System.Collections.Generic;

public class Define
{
    public const int JUMP_GAME_SCENE_KEY = 108003; // 점프점프 게임씬 키
    public const int UPDOWN_GAME_SCENE_KEY = 108006; // 업다운 게임씬 키
    public const int PUSH_GAME_SCENE_KEY = 108009; // 눌러눌러 게임씬 키
    public const int RUN_GAME_SCENE_KEY = 108012;   // 달려달려 게임씬 키
    public const int SHOOT_GAME_SCENE_KEY = 108015; // 슈팅게임 게임씬 키
    public const int AHPUH_GAME_SCENE_KEY = 108016; // 어푸어푸 게임씬 키
    public const int HOMERUN_GAME_SCENE_KEY = 108018; // 홈런 게임씬 키

    public const float INSPECTOR_SPACE = 20f;
    public const int CLEAR_UI_NUM = 104016;
    public const int HIDDEN_CHAPTER = 108017;
    public const float MAX_TIMER = 10 * 60f;

    public const int START_CHAPTER = 108001;
    public const int LAST_CHAPTER = 108018; //마지막 스테이지 key

    //텍스트 KEY값 모음
    public const int START_CLEAR_STORY_TEXT_KEY = 102467;

    public static Dictionary<int, string> MiniGames { get { return miniGames; } }
    private static Dictionary<int, string> miniGames = new Dictionary<int, string>()
        {
            { JUMP_GAME_SCENE_KEY, "JumpGameScene"},
            { UPDOWN_GAME_SCENE_KEY,"UpDownGameScene"},
            { PUSH_GAME_SCENE_KEY,"PushPushGameScene"},
            {RUN_GAME_SCENE_KEY, "RunRunGameScene" },
            {SHOOT_GAME_SCENE_KEY, "ShootShootGameScene"},
            {AHPUH_GAME_SCENE_KEY, "AhpuhAhpuhGameScene"},
            {HOMERUN_GAME_SCENE_KEY, "HomeRunGameScene" }
        };

    // 언어설정
    public enum LanguageSet
    {
        KR, EN, SP, FR
    }

    public enum JsonFile
    {
        ChapterTable,
        IDTable,
        ResourceTable,
        TextTable,
        TextBundleTable,
        UITable,
        PuzzleTable,
        ItemTable,
        InteractionObjectTable,
        StageTable,
        StageProcessTable,
        InventoryTable,
        UserInfoTable,
        UserItemTable,
        UserLogTable
    }

    //퍼즐 정답을 알 수 있는 key -> 정답은 Puzzle매니저에 저장되어있음. (key로 접근가능)
    public enum PuzzleAnswerKey
    {
        Chap02_1_USB,
        Chap02_1_Pattern,
        Chap03_2_Power,
        Hose = 107030,
        Dial = 107019,
        Chap05_1_Note = 100000,
        Chap04_2_Battery = 107192
    }

    public enum Chapter
    {
        UIs,
        Chap01_1,
        Chap01_2,
        Chap02_1,
        Chap02_2,
        Chap03_1,
        Chap03_2,
        Chap04_1,
        Chap04_2,
        Chap05_1,
        Chap05_2,
        Hidden
    }

    public enum HoseColor
    {
        Blue,
        Pupple,
        Red,
        Yellow
    }

    public enum Dial
    {
        Hour12,
        Hour8,
        Hour4
    }
}
