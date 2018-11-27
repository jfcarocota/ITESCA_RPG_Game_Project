using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameCore.SystemControls;
using UnityEngine.UI;
using GameCore.MemorySystem;
using GameCore.GameTime;


public abstract class MenuController : MonoBehaviour {

    [SerializeField]
    protected string gameScene;
    [SerializeField]
    protected string titleScene;
    [SerializeField]
    protected GameObject pauseMenu;
    [SerializeField]
    protected GameObject deadScreenGO;
    public static bool isPaused;
    

    [SerializeField]
    protected Button[] menuOptions;

    protected bool canPause;

    public static bool gameCleared;
    public static bool deadScreen;
    private TimeManager tm;
    private bool initialDeadCount;
    private bool deadScreenCount;
    private bool finalDeadCount;

    [SerializeField]
    protected float fadeInTime;
    [SerializeField]
    protected float deadTextTime;
    [SerializeField]
    protected float fadeOutTime;

    private Image blackScreen;
    private Text redText;
    private Text winText;
    private Outline outlineText;
    private Outline outlineTextWin;

    [SerializeField]
    GameObject beforeRobot;
    [SerializeField]
    GameObject afterRobot;


    // Use this for initialization
    void Awake () {
        setPause();
        deadScreen = false;
        tm = new TimeManager(0f);
        initialDeadCount = false;
        finalDeadCount = false;
        deadScreenCount = false;
        blackScreen = deadScreenGO.GetComponent<Image>();
        redText = deadScreenGO.GetComponentsInChildren<Text>()[0];
        outlineText = deadScreenGO.GetComponentsInChildren<Outline>()[0];
        winText = deadScreenGO.GetComponentsInChildren<Text>()[1];
        outlineTextWin = deadScreenGO.GetComponentsInChildren<Outline>()[1];

    }

    private void Start()
    {
        if (!canPause)
        {
            for(int i = 0; i < menuOptions.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        menuOptions[i].onClick.AddListener(NewGame);
                        break;
                    case 1:
                        menuOptions[i].onClick.AddListener(LoadGame);
                        break;
                    case 2:
                        menuOptions[i].onClick.AddListener(ExitGame);
                        break;

                }
            }
        }
        else
        {
            for (int i = 0; i < menuOptions.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        menuOptions[i].onClick.AddListener(ResumeGame);
                        break;
                    case 1:
                        menuOptions[i].onClick.AddListener(SaveGame);
                        break;
                    case 2:
                        menuOptions[i].onClick.AddListener(ReturnToTitle);
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonUp("Fire4") & canPause)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
       
        if (deadScreen || initialDeadCount || finalDeadCount || deadScreenCount)
        {
            if (deadScreen)
            {
                deadScreen = false;
                initialDeadCount = true;
                tm.StartTime(fadeInTime);
                deadScreenGO.SetActive(true);
                blackScreen.color = new Color(1f, 1f, 1f, 0f);
                if (!gameCleared)
                {
                    winText.gameObject.SetActive(false);
                    redText.color = new Color(170f / 255f, 0f, 0f, 0f);
                }
                else
                {
                    redText.gameObject.SetActive(false);
                    winText.color = new Color(230f / 255f, 240f / 255f, 75f / 255f, 0f);
                    deadTextTime = deadTextTime * 4;
                }
                    

            }
            else if (initialDeadCount)
            {
                //advance deadScreen start
                FadeIn();

                if (tm.IsTimeOverNoUpdate())
                {
                    initialDeadCount = false;
                    deadScreenCount = true;
                    tm.StartTime(deadTextTime);
                }
            }
            else if (deadScreenCount)
            {
                if (tm.IsTimeOverNoUpdate())
                {
                    deadScreenCount = false;
                    finalDeadCount = true;
                    tm.StartTime(fadeOutTime);
                }
            }
            else if (finalDeadCount)
            {
                // advance deadScreen end
                FadeOut();

                if (tm.IsTimeOverNoUpdate())
                {
                    finalDeadCount = false;
                    SceneManager.LoadScene(titleScene);
                }
            }

            tm.AdvanceTime();
        }

    }
    
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToTitle()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(titleScene);
    }

    public void NewGame()
    {
        print("NewGamer");
        SceneManager.LoadScene(gameScene);
        print("NewGamer2");
    }

    public void LoadGame()
    {
        print("LoadGamer");
        MemorySystem.loadGame = true;
        SceneManager.LoadScene(gameScene);
    }

    public void SaveGame()
    {
        //get the current position of the player, later
        Queue<GameObject> parties = PartyManager.partyMembers;
        Vector3[] partyPos = new Vector3[4];
        for (int i = 0; i < PartyManager.members.Length; i++)
        {
            partyPos[i] = PartyManager.members[i].transform.position;
        }

        MemorySystem.Save(new GameData(partyPos,beforeRobot.activeSelf,afterRobot.activeSelf), "party.data");
        Debug.Log("Saved");
    }

    public void ExitGame()
    {
        print("_ExitGamer");
        Application.Quit();
    }

    protected virtual void setPause()
    {

    }

    private void FadeIn()
    {
        blackScreen.color = new Color(0f,0f,0f,
            blackScreen.color.a + (Time.deltaTime / fadeInTime) * 100f/255f);
        if (!gameCleared)
        {
            redText.color = new Color(170f / 255f, 0f, 0f,
                redText.color.a + (Time.deltaTime / fadeInTime));
            outlineText.effectColor = new Color(1f, 1f, 1f,
                outlineText.effectColor.a + (Time.deltaTime / fadeInTime));
        }
        else
        {
            winText.color = new Color(230f / 255f, 240f / 255f, 75f / 255f,
                winText.color.a + (Time.deltaTime / fadeInTime));
            outlineText.effectColor = new Color(1f, 1f, 1f,
                outlineText.effectColor.a + (Time.deltaTime / fadeInTime));
        }
        
    }

    private void FadeOut()
    {
        blackScreen.color = new Color(0f, 0f, 0f,
           blackScreen.color.a + (Time.deltaTime / fadeInTime) * 155f/255f);
        if (!gameCleared)
        {
            redText.color = new Color(redText.color.r - (Time.deltaTime / fadeInTime) * 170f / 255f
                , 0f, 0f);
            outlineText.effectColor = new Color(outlineText.effectColor.r - (Time.deltaTime / fadeInTime),
                outlineText.effectColor.r - (Time.deltaTime / fadeInTime),
                outlineText.effectColor.r - (Time.deltaTime / fadeInTime));
        }
        else 
        {
            redText.color = new Color(redText.color.r - (Time.deltaTime / fadeInTime) * 230f / 255f,
                redText.color.g - (Time.deltaTime / fadeInTime) * 240f / 255f,
                redText.color.b - (Time.deltaTime / fadeInTime) * 75f / 255f);
            outlineText.effectColor = new Color(outlineText.effectColor.r - (Time.deltaTime / fadeInTime),
                outlineText.effectColor.r - (Time.deltaTime / fadeInTime),
                outlineText.effectColor.r - (Time.deltaTime / fadeInTime));
        }
    }

}
