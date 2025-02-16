using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.UX;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenuCanvas;
    public GameObject LevelCanvas;
    public GameObject PlaneCanvas;

    public GameObject level2Button;

    private int level1Completed;
    
    void Start()
    {

        if (!PlayerPrefs.HasKey("Level_1"))
        {
            PlayerPrefs.SetInt("Level_1",0);
    
        }
        

        MainMenuCanvas.SetActive(true);
        LevelCanvas.SetActive(false);
        PlaneCanvas.SetActive(false);
    }

    void Update()
    {
        level1Completed=PlayerPrefs.GetInt("Level_1");
        level2Button.GetComponent<PressableButton>().enabled = level1Completed == 1;
    }

    public void ShowLevelCanvas()
    {
        MainMenuCanvas.SetActive(false);
        PlaneCanvas.SetActive(false);
        LevelCanvas.SetActive(true);
    }

    public void ShowMainMenu()
    {
        LevelCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    public void Load1()
    {
        PlayerPrefs.SetInt("SelectedLevel", 1);
        LevelCanvas.SetActive(false);
        PlaneCanvas.SetActive(true);
    }
     public void Load2()
    {
        PlayerPrefs.SetInt("SelectedLevel", 2);
        LevelCanvas.SetActive(false);
        PlaneCanvas.SetActive(true);
    }

    public void LoadLevelGrey()
    {
        int selectedLevel = PlayerPrefs.GetInt("SelectedLevel");
        PlayerPrefs.SetInt("Plane",1);
        SceneManager.LoadScene(selectedLevel);
    }

    public void LoadLevelRed()
    {
        int selectedLevel = PlayerPrefs.GetInt("SelectedLevel");
        PlayerPrefs.SetInt("Plane",2);
        SceneManager.LoadScene(selectedLevel);
    }
    public void QuitGame()
    {
        PlayerPrefs.SetInt("Level_1",0);
        Application.Quit();
    }
}
