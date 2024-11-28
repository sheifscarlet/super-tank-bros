using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Pause Settings")] 
    [SerializeField] private bool isPaused;
    private bool isGameOver;

    [Header("Game Mode Settings")]
    [SerializeField] private GameObject firstPlayer;
    private Health firstPlayerHealth;
    [SerializeField] GameObject secondPlayerUI;
    [SerializeField] private GameObject secondPlayer;
    private Health secondPlayerHealth;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false; 
        if (GameSettings.IsTwoPlayerMode)
        {
            Debug.Log("Two Player Mode Enabled");
            secondPlayerUI.SetActive(true);
            secondPlayer.SetActive(true);

            firstPlayerHealth = firstPlayer.GetComponent<Health>();
            secondPlayerHealth = secondPlayer.GetComponent<Health>();
        }
        else
        {
            Debug.Log("Single Player Mode Enabled");
            secondPlayerUI.SetActive(false);
            secondPlayer.SetActive(false);
            
            firstPlayerHealth = firstPlayer.GetComponent<Health>();
            secondPlayerHealth = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        IsGameOverCheck();
        HandleInput();
        
        if (!isGameOver) 
        {
            if (isPaused)
            {
                Cursor.lockState = CursorLockMode.Confined; 
                Time.timeScale = 0f;
                UIController.instance.ShowPauseMenu();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; 
                Time.timeScale = 1f;
                UIController.instance.ShowMainUI();
            }
        }

        if (isGameOver)
        {
            GameOver();
        }

        if (EnemyWavesController.instance.allWavesCleared)
        {
            Win();
        }
        
    }

    void IsGameOverCheck()
    {
        if (!GameSettings.IsTwoPlayerMode)
        {
            if (firstPlayerHealth.IsDead)
            {
                isGameOver = true;
            }
        }
        else
        {
            if (firstPlayerHealth.IsDead && secondPlayerHealth.IsDead)
            {
                isGameOver = true;
            }
        }
    }
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            isPaused = !isPaused;
        }
    }

   

    void Win()
    {
        AudioController.instance.PlaySound("Win");
        isGameOver = true; 
        isPaused = false; 
        Cursor.lockState = CursorLockMode.Confined; 
        Time.timeScale = 0f;
        UIController.instance.ShowWinScreen();
    }

    void GameOver()
    {
        AudioController.instance.PlaySound("GameOver");
        isGameOver = true; 
        isPaused = false; 
        Cursor.lockState = CursorLockMode.Confined; 
        Time.timeScale = 0f;
        UIController.instance.ShowGameOverScreen();
    }

    public bool Unpause()
    {
        isPaused = false;
        return isPaused;
    }

   
}
