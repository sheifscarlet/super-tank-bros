using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject selectGameTypeCanvas;
    public GameObject tutorialCanvas;
    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        selectGameTypeCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
    }
    public void ShowSelectGameTypeMenu()
    {
        selectGameTypeCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
        
    }
    
    public void ShowTutorialMenu()
    {
        selectGameTypeCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        tutorialCanvas.SetActive(true);
        
    }
    
    public void SetSinglePlayerMode()
    {
        GameSettings.IsTwoPlayerMode = false;
        
    }

    public void SetTwoPlayerMode()
    {
        GameSettings.IsTwoPlayerMode = true;
    }
}
