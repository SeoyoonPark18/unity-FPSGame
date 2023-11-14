using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBomb : MonoBehaviour
{
    public GameObject firePosition; //발사 위치
    public GameObject bombFactory; //폭탄 오브젝트
    public float throwPower = 15f; //폭탄 투척 파워
    public GameObject bulletEffect; //총알 이펙트
    ParticleSystem ps; //파티클 시스템
    public int weaponPower = 10; //총알 공격력

    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //마우스 오른쪽 버튼 입력
        if (Input.GetMouseButtonDown(1)) 
        {
            GameObject bomb = Instantiate(bombFactory); //폭탄 생성
            bomb.transform.position = firePosition.transform.position; //위치 지정

            Rigidbody rb = bomb.GetComponent<Rigidbody>(); //폭탄의 리지드바디
            //카메라의 정면으로 폭탄에 힘을 가함 (Impulse는 순간의 폭발적인 힘을 위해)
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse); 
        }

        //마우스 왼쪽 버튼 입력
        if (Input.GetMouseButtonDown(0))
        {
            //레이캐스트
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //레이 생성 후 위치와 방향 설정
            RaycastHit hitInfo = new RaycastHit(); //레이가 부딪힌 대상 저장
            if (Physics.Raycast(ray, out hitInfo)) //레이를 발사, 레이캐스트에 부딪힌 물체가 있다면
            {               
                if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("FPSEnemy")) //만약 부딪힌 대상의 레이어가 Enemy라면
                {                   
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>(); //EnemyFSM의 HitEnemy 함수 실행 (transform의 컴포넌트로 스크립트 불러올 수 있음)
                    eFSM.HitEnemy(weaponPower);
                }

                bulletEffect.transform.position = hitInfo.point; //부딪힌 물체의 위치좌표(point)
                bulletEffect.transform.forward = hitInfo.normal; //부딪힌 지점의 '법선 벡터'와 일치
                // 법선 벡터란: 
                ps.Play();
            }
        }
    }
}
