using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RestartBtn() //재시작 버튼 클릭
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //현재 활성화되어 있는 씬 불러오기
    }
    public void QuitBtn() //게임 종료 버튼
    {
        Application.Quit();
    }
}
