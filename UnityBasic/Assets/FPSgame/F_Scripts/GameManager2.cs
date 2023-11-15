using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    //�̱��� ����
    //Ư�� Ŭ������ �ν��Ͻ��� ���� �ϳ��� �����ǵ��� �������ִ� ������ ����
    //������ ���� ������ �����ؾ� �ϱ� ������
    public static GameManager2 gm; //�÷��̾��� ������ �����ϴ� Ŭ����

    private void Awake() //Start()���ٵ� ���� ȣ���
    {
        if(gm == null)
        {
            gm = this;
        }
    }
  
    public enum GameState //���� ���� ���
    {
        Ready,
        Go,
        Pause,
        GameOver
    }
    public GameState gState;
    public GameObject gameLabel; //UI ������Ʈ ����
    Text gameText; //���� ���� �ؽ�Ʈ
    public GameObject gameOption;


    void Start()
    {
        gState = GameState.Ready; //�ʱ� ���¸� �غ�� ����
        gameText = gameLabel.GetComponent<Text>();
        gameText.text = "Ready";

        StartCoroutine(ReadyToStart()); //�ڷ�ƾ �Լ� ����
    }

    void Update()
    {
        
    }

    IEnumerator ReadyToStart()
    {
        yield return new WaitForSeconds(2f); //2�� ���
        gState = GameState.Go; //���� ����
        gameText.text = "Go!";
        yield return new WaitForSeconds(0.5f); 
        gameLabel.SetActive(false); //�ؽ�Ʈ ��Ȱ��ȭ
    }

    public void OpenOption() //â ����
    {
        gameOption.SetActive(true);
        Time.timeScale = 0;
        gState = GameState.Pause;
    }
    public void CloseOption() //â �ݱ�
    {
        gameOption.SetActive(false);
        Time.timeScale = 1;
        gState = GameState.Go;
    }
}
