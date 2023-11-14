using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //충돌했을 때
    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(bombEffect); //효과 프리팹 생성
        eff.transform.position = transform.position; //폭탄 위치로 위치 변경
        Destroy(gameObject); //폭탄 제거 (자기 자신)   
    }
}
