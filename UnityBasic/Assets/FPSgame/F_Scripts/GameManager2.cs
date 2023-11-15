using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    //싱글턴 패턴
    //특정 클래스의 인스턴스가 오직 하나만 생성되도록 보장해주는 디자인 패턴
    //언제나 같은 정보에 접근해야 하기 때문에
    public static GameManager2 gm; //플레이어의 정보를 저장하는 클래스

    private void Awake() //Start()보다도 먼저 호출됨
    {
        if(gm == null)
        {
            gm = this;
        }
    }
  
    public enum GameState //게임 상태 상수
    {
        Ready,
        Go,
        Pause,
        GameOver
    }
    public GameState gState;
    public GameObject gameLabel; //UI 오브젝트 변수
    Text gameText; //게임 상태 텍스트
    public GameObject gameOption;


    void Start()
    {
        gState = GameState.Ready; //초기 상태를 준비로 설정
        gameText = gameLabel.GetComponent<Text>();
        gameText.text = "Ready";

        StartCoroutine(ReadyToStart()); //코루틴 함수 실행
    }

    void Update()
    {
        
    }

    IEnumerator ReadyToStart()
    {
        yield return new WaitForSeconds(2f); //2초 대기
        gState = GameState.Go; //상태 변경
        gameText.text = "Go!";
        yield return new WaitForSeconds(0.5f); 
        gameLabel.SetActive(false); //텍스트 비활성화
    }

    public void OpenOption() //창 열기
    {
        gameOption.SetActive(true);
        Time.timeScale = 0;
        gState = GameState.Pause;
    }
    public void CloseOption() //창 닫기
    {
        gameOption.SetActive(false);
        Time.timeScale = 1;
        gState = GameState.Go;
    }
}
