using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject player; //플레이어 게임오브젝트 참조
    Vector3 offset; //카메라와 플레이어 사이의 거리 변수

    void Start()
    {
        offset = transform.position - player.transform.position; //두 오브젝트 사이의 거리 계산 = 오브젝트1 위치 - 오브젝트2 위치

    }

    void LateUpdate() //모든 Update()문이 실행된 후에 실행
    {
        //Main Camera가 Player를 따라 이동
        transform.position = player.transform.position + offset; //카메라 위치 = 플레이어 위치 + 사이의 거리

    }
}
