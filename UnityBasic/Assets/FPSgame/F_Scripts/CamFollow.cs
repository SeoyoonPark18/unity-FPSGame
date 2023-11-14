using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target; //CamPosition의 트랜스폼 컴포넌트

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = target.position; //카메라 위치를 타겟 위치에 일치
    }
}
