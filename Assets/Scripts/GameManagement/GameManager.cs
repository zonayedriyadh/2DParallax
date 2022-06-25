using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;


public enum ResultType
{
    TimeOver,
    KilledInAction
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameStarted;
    public bool isGamePaused;
    public bool isGameOver;

    [Header("Cameras")]
    public GameObject uiCamera;

    private int selectedPlane;
    private GameHud gameHud;

    public static GameManager SharedManager()
    {
        return Instance;
    }

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        gameHud = GameHud.sharedManager();

#if UNITY_IPHONE || UNITY_IOS

            Application.targetFrameRate = 60;
#endif

#if !UNITY_EDITOR && UNITY_ANDROID
        Application.targetFrameRate = 30;
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        isGameStarted = true;
        uiCamera.SetActive(false);
        PanelGameStart.sharedInstance.StartGame();
    }

    public void GameOver()
    {
        isGameOver = true;
        PanelGameStart.sharedInstance.RemoveGamePanel();
        uiCamera.SetActive(true);
        GameHud.sharedManager().LoadeStorePanel((int)Panel.panelGameOver);
    }

    public void Restart()
    {
        //Debug.Log("restarting the scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
