using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoving : MonoBehaviour
{
    public float moveSpeed = 7f; //이동 속력 변수
    CharacterController cc; //캐릭터 컨트롤러 컴포넌트
    float gravity = -20f; //중력 변수
    float yVelocity = 0; //수직 속력 변수
    public float jumpPower = 7f; //점프력 변수
    bool isJump = false; //연속 점프 방지 변수
    public int playerHp = 100; //플레이어 체력 변수
    public int maxHp; //최대 체력
    public Slider hpSlider; //체력 슬라이더 변수
    public GameObject bloodEffect; //블러드 효과 이미지
    public GameObject gameOption; // restart or quit

    void Start()
    {
        cc = GetComponent<CharacterController>();
        maxHp = playerHp;
        bloodEffect.SetActive(false);
    }

    void Update()
    {
        if(GameManager2.gm.gState != GameManager2.GameState.Go) //Go 상태에서만 조작 가능
        {
            return;
        }

        float h = Input.GetAxis("Horizontal"); //키보드 입력
        float v= Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v); //방향 설정
        dir = dir.normalized; //매끄러운 이동 위해 normalized로 균일한 값 주기
        dir = Camera.main.transform.TransformDirection(dir); //카메라를 기준으로 방향 변환
      
        if(cc.collisionFlags == CollisionFlags.Below) //바닥에 착지했다면
        {
            isJump = false;
            yVelocity = 0; //최적화용
        }        
        if (Input.GetButtonDown("Jump") && isJump == false) //스페이스바를 누르면 점프
        {
            yVelocity = jumpPower;
            isJump = true;
        }

        yVelocity += gravity * Time.deltaTime; //수직 속도에 중력 값 적용
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime); //캐릭터 이동
        //transform.position += dir * moveSpeed * Time.deltaTime; //위와 같음

        hpSlider.value = (float)playerHp / (float)maxHp; //현재 체력을 슬라이더의 value에 반영
        //Debug.Log(hpSlider.value);
    }

    public void DamageAction(int damage) //플레이어 피격 함수
    {
        playerHp -= damage; //적의 공격력만큼 플레이어 체력 감소
        if(playerHp < 0)
        {
            playerHp = 0; //체력이 음수라면 0으로 초기화
            gameOption.SetActive(true);
        }
        else //체력이 양수일 때
        {
            StartCoroutine(PlayBloodEffect()); //피격 효과 출력
        }
        Debug.Log("HP: " + playerHp);
    }

    //피격 효과 코루틴 함수
    IEnumerator PlayBloodEffect()
    {
        bloodEffect.SetActive(true); //효과 활성화
        yield return new WaitForSeconds(0.3f); //3초간 대기
        bloodEffect.SetActive(false); //비활성화
    }
}
