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

    public void RestartBtn() //����� ��ư Ŭ��
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //���� Ȱ��ȭ�Ǿ� �ִ� �� �ҷ�����
    }
    public void QuitBtn() //���� ���� ��ư
    {
        Application.Quit();
    }
}
