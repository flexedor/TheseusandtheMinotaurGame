using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    public static System.Action ToursTick;
    [SerializeField] private GameObject LooseCanvas;
    [SerializeField] private GameObject WinCanvas;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private int CurrentLevelNum=0;
    private GameObject curentLevel;
    
    public static System.Action WaitWasPressed;
    /// <summary>
    /// on Awake load first level  
    /// </summary>
    private void Awake()
    {
        LoadLevel(CurrentLevelNum);
        MinotaurMovements.GameOver += GameOverSequence;
        Finish.WinSequence += WinSequence;
    }

    
    private void OnDisable()
    {
        MinotaurMovements.GameOver -= GameOverSequence;
        Finish.WinSequence -= WinSequence;
    }
    /// <summary>
    /// LoadLevel function loading level from list "levels" 
    /// </summary>
    /// <param name="levelToLoad">num of level to load</param>
    private void LoadLevel(int levelToLoad)
    {
        WinCanvas.SetActive(false);
        LooseCanvas.SetActive(false);
        
        if (curentLevel!=null)
        {
            Destroy(curentLevel);
        }
        curentLevel = Instantiate(levels[levelToLoad],this.transform);
    }

    /// <summary>
    /// This function turns on canvas with win message 
    /// </summary>
    void WinSequence()
    {
        WinCanvas.SetActive(true);
    }
    /// <summary>
    /// This function turns on canvas with loose message 
    /// </summary>
    void GameOverSequence()
    {
        LooseCanvas.SetActive(true);
    }

    
    void Update()
    {
        
        if (Input.anyKeyDown)
        {
            if (Input.GetKey(KeyCode.R)) 
            {
                LoadLevel(CurrentLevelNum);
            }
            else if(Input.GetKey(KeyCode.Q))
            {
              WaitWasPressed?.Invoke();
            } 
            else if(Input.GetKey(KeyCode.N))
            {
                if (CurrentLevelNum+1<levels.Length)
                {
                    CurrentLevelNum++;
                }
                LoadLevel(CurrentLevelNum);
            }
            else if(Input.GetKey(KeyCode.P))
            {
                if (CurrentLevelNum!=0)
                {
                    CurrentLevelNum--;
                }
                LoadLevel(CurrentLevelNum);
            }
            else
            {
                ToursTick?.Invoke();
            }
        }
    }
}
