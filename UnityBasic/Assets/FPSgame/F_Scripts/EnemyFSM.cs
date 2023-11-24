using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{ 
    enum EnemyState //적 상태 상수
    {
        Idle, //대기
        Move, //이동
        Attack, //공격
        Return, //복귀
        Damaged, //피격
        Die //죽음
    }    

    EnemyState m_State; //적 상태 변수   
    CharacterController cc; //적 캐릭터 컨트롤러 컴포넌트
    Transform player; //플레이어 트랜스폼 컴포넌트
    public float findDistance = 8f; //플레이어 발견 범위
    public float attackDistance = 2f; //플레이어 공격 범위
    public float moveSpeed = 3f; //적 이동 속도
    public float currentTime = 0; //누적 시간
    public float attackDelayTime = 2f; //공격 딜레이 시간
    public int attackPower = 10; //적 공격력
    Vector3 originPos; //초기 위치 저장
    Quaternion originRot; //초기 회전값 저장
    public float moveDistance = 20f; //초기위치로부터 이동 가능 범위
    public int enemyHp = 30; //적 체력
    public int maxHp;
    public Slider hpSlider;
    Animator anim; //애니메이터 변수
    public GameObject gameOption; // restart/quit

    void Start()
    {
        cc = GetComponent<CharacterController>(); //적 캐릭터컨트롤러 할당
        player = GameObject.Find("Player").transform; //플레이어 트랜스폼 할당
        m_State = EnemyState.Idle; //최초의 적 상태를 '대기'로 설정
        originPos = transform.position; //적 초기 위치 저장
        originRot = transform.rotation; //적 초기 회전값 저장
        maxHp = enemyHp;
        anim = transform.GetComponentInChildren<Animator>(); //자식 오브젝트의 애니메이터 컴포넌트 할당
    }

    void Update()
    {
        //EnemyState 체크해서 상태별 기능 수행
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
        }

        hpSlider.value = (float)enemyHp / (float)maxHp; //UI로 적 Hp 화면에 표시
    }

    void Idle()
    {
        //만약 플레이어와 적의 거리가 '발견 범위'보다 작다면, ((Move로 전환))
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            Debug.Log("Idle -> Move");
            anim.SetTrigger("IdleToMove"); //이동 애니메이션으로 전환
        }
    }
    void Move()
    {
        //만약 현재 위치와 초기 위치의 거리가 '이동 가능 범위'보다 크다면 ((Return 전환))
        if(Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            Debug.Log("Move -> Return");
        }
        //만약 플레이어와 적의 거리가 '공격 범위'보다 크다면, ((플레이어에게 이동))
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized; //이동 방향 (적이 플레이어에게 가는 방향 = 플레이어 위치- 적 위치)
            cc.Move(dir * moveSpeed * Time.deltaTime); //플레이어에게 이동 (방향*속도*시간)
            transform.forward = dir; //플레이어를 향해 방향 전환
        }
        else //만약 플레이어와 적의 거리가 '공격 범위'보다 작다면, ((Attack 전환))
        {
            m_State = EnemyState.Attack;
            Debug.Log("Move -> Attack");
            currentTime = attackDelayTime; //누적시간을 공격 딜레이 시간만큼 미리 진행
            anim.SetTrigger("MoveToAttackDelay"); //공격 대기 애니메이션 실행
        }
    }

    void Attack()
    {
        //만약 플레이어와 적의 거리가 '공격 범위'보다 작다면, ((플레이어에 공격))
        if (Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            //Vector3 dir = (player.position - transform.position).normalized; //이동 방향 (적이 플레이어에게 가는 방향 = 플레이어 위치- 적 위치)
            //cc.Move(dir * moveSpeed * Time.deltaTime); //플레이어에게 이동 (방향*속도*시간)

            currentTime += Time.deltaTime; //현재 시간 누적
            if(currentTime > attackDelayTime) //일정 시간마다 플레이어 공격
            {
                Debug.Log("공격");

                PlayerMoving pmObject = GameObject.Find("Player").GetComponent<PlayerMoving>(); 
                pmObject.DamageAction(attackPower); //플레이어의 PlayerMoving 스크립트의 DamageAction 호출
                currentTime = 0;
                anim.SetTrigger("StartAttack"); //공격 애니메이션 실행
            }
        }
        else //만약 플레이어와 적의 거리가 '공격 범위'보다 크다면, ((Move로 전환))
        {
            m_State = EnemyState.Move;
            Debug.Log("Attack -> Move");
            currentTime = 0;
            anim.SetTrigger("AttackToMove"); //이동 애니메이션 실행
        }
    }
    
    void Return()
    {
        //만약 초기 위치에서의 거리가 0.1보다 크다면 계속 초기 위치 쪽으로 이동
        if(Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized; //방향
            //'normalized'는 벡터의 정규화. 사용 이유: 모든 방향의 벡터 길이를 1로 만들어, 방향에 따른 이동 속도를 동일하게 해줌.
            cc.Move(dir * moveSpeed * Time.deltaTime); //이동
            transform.forward = dir; //복귀 지점으로 방향 전환
        }
        else //그렇지 않다면 적 위치를 초기 위치로 조정, ((Idle로 전환))
        {
            transform.position = originPos;
            transform.rotation = originRot;

            m_State = EnemyState.Idle;
            Debug.Log("Return -> Idle");
            anim.SetTrigger("MoveToIdle"); //대기 애니메이션으로 전환
        }
    }

    //데미지 처리 코루틴 함수
    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.5f); //피격 모션만큼 기다림
        m_State = EnemyState.Move; //이동 상태 전환
        anim.SetTrigger("IdleToMove");

    }
    void Damaged()
    {
        StartCoroutine(DamageProcess()); //피격 코루틴 실행
    }

    public void HitEnemy(int hitPower) //데미지 실행 함수
    {

        if(m_State == EnemyState.Damaged)
        {
            return;
        }

        enemyHp -= hitPower; //플레이어의 공격력만큼 적 체력 감소
        Debug.Log("적 체력: " +  enemyHp);
         if(enemyHp > 0) //적 체력이 0보다 크면 피격
        {
            m_State = EnemyState.Damaged;
            Damaged();
        }
        else //적 체력이 0보다 작으면 죽음
        {
            m_State=EnemyState.Die;
            Debug.Log("죽음");
            anim.SetTrigger("Die"); //죽음 애니메이션 실행
            Die();
        }        
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;
        yield return new WaitForSeconds(6f); //피격 모션만큼 기다림
        Debug.Log("적 제거");
        Destroy(gameObject); //적 제거
        gameOption.SetActive(true); //지금은 적이 1명이기 때문에, 죽을 시 옵션 창 띄움

    }
    void Die()
    {
        StopAllCoroutines(); //지금 진행 중인 코루틴 중지
        StartCoroutine(DieProcess()); //죽음 코루틴 실행
    }
}
