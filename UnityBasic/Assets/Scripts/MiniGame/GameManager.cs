using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //���� ���� �ٽ� ����
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
