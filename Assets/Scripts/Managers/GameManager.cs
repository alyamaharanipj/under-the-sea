using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Utilities
{
    public enum State { Menu, Preparing, Playing, Paused };  //variable asing 
    
    public State currentState;                  //variabel keadaan saat ini          
    public float elapsedTime = 0.0f;            //variabel waktu
    public float playTime = 0.0f;               //
    public int currentScore = 0;                // variabel score
    public int currentHighScore = 0;            //variable highscore
    public int eatenFood=1;
    public int camSize=5;
    public int foodEat;
    public string userHighScore;
    public GameObject playerPrefab;
    

    public string backgroundMusic = "BackgroundMusic"; //background music

    private PlayerController playerController;
    public InputField user;
    private AudioManager audioManager;                  //audio
    private Camera mainCamera;                            //camera
    private Level level;                                //level

    // Awake is always called before any Start functions
    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Print("No AudioManager found!", "error");
        }
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Print("No Camera found!", "error");
        }
        level = FindObjectOfType<Level>();
        if (level == null)
        {
            Print("No Level found!", "error");
        }
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time;

        if (currentState == State.Playing)
        {
            camController();
            playTime += Time.deltaTime;

            if (currentScore > currentHighScore)
            {
                currentHighScore = currentScore;
                Save();
                Save2();
            }
        }
    }

    /// <summary>
    /// Change the current game state.
    /// </summary>
    public void ChangeState(State state)
    {
        Print("Changing state", "event");

        currentState = state;
    }

    /// <summary>
    /// Start the game.
    /// </summary>
    public void PrepareLevel(int level)
    {
        Print("Preparing level", "event");

        currentState = State.Preparing;
    }

    /// <summary>
    /// Play the game.
    /// </summary>
    public void Play()
    {
        Print("Preparing game", "event");

        currentState = State.Preparing;
        GameObject newPlayer = Instantiate(playerPrefab, gameObject.transform.position, Quaternion.identity);
        mainCamera.GetComponent<SmoothFollow2DCamera>().target = newPlayer.transform;
        mainCamera.GetComponent<SmoothFollow2DCamera>().enabled = true;
        audioManager.PlaySound(backgroundMusic);
        Reset();
        level.PrepareLevel();  
        currentState = State.Playing;

        
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void Pause()
    {
        Print("Pausing game", "event");

        currentState = State.Paused;
        audioManager.PauseSound(backgroundMusic);
        Time.timeScale = 0;
      
    }

    /// <summary>
    /// Proceed gameplay.
    /// </summary>
    public void Resume()
    {
        currentState = State.Playing;
        audioManager.ResumeSound(backgroundMusic);
        Time.timeScale = 1.0f;
    }

/// <summary>
    /// Quit the game and save settings.
    /// </summary>
    public void Quit()
    {
        Print("Quitting game", "event");
        Application.Quit();//bug
    }


    /// <summary>
    /// Reset the game.
    /// </summary>
    public void Reset()
    {
        currentScore = 0;
        playTime = 0.0f;
    }

    /// <summary>
    /// Change the current score.
    /// </summary>
    public void ChangeScore(int score)
    {
        Print("Changing score", "event");

        currentScore += score;
    }
    public void ChangeEatenFood(int totalFood){
        Print("Changing eaten food", "event");

        eatenFood += totalFood;
    }

    /// <summary>
    /// Load game preferences and other save files.
    /// </summary>
    public void Load()
    {
        Print("Loading", "event");

        currentHighScore = Deserialize<int>(Application.streamingAssetsPath + "/XML/Highscores.xml");
        userHighScore = Deserialize<string>(Application.streamingAssetsPath + "/XML/Username.xml");

    }

    /// <summary>
    /// Save preferences and progress.
    /// </summary>
    public void Save()
    {
        Print("Saving", "event");

        Serialize(currentHighScore, Application.streamingAssetsPath + "/XML/Highscores.xml");
    }
    public void Save2()
    {
        Print("Saving","event");

        Serialize(user.text, Application.streamingAssetsPath + "/XML/Username.xml");

    }
    public void spell1()
    {
        playerController.skill1();
    }

   public void camController(){
        camSize=eatenFood/70;
        if(camSize>=1){
            Camera.main.orthographicSize = 5+camSize;
        }else{
            Camera.main.orthographicSize = 5;
        }
        //foodEat = eatenFood/5;
            //Camera.main.orthographicSize = camSize + foodEat);
   }

   
}
