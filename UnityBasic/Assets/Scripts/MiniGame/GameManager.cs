using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //현재 씬을 다시 실행
    }
    public void CloseGame()
    {
        Application.Quit();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
