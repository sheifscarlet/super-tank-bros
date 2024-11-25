using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Pause Settings")] 
    [SerializeField] private bool isPaused;
    private bool isGameOver; 
    
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false; 
    }

    // Update is called once per frame
    void Update()
    {
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
        isGameOver = true; 
        isPaused = false; 
        Cursor.lockState = CursorLockMode.Confined; 
        Time.timeScale = 0f;
        //UIController.instance.ShowWinScreen();
    }

    void GameOver()
    {
        isGameOver = true; 
        isPaused = false; 
        Cursor.lockState = CursorLockMode.Confined; 
        Time.timeScale = 0f;
        //UIController.instance.ShowGameOverScreen();
    }

    public bool Unpause()
    {
        isPaused = false;
        return isPaused;
    }

   
}
