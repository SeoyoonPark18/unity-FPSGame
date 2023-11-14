using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 1000f; //회전 속도 변수
    float mx = 0; float my = 0; 

    void Start()
    {
        
    }

    void Update()
    {
        //마우스 입력
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        //회전 값 변수에 마우스 입력 값 누적 (이동=방향*거리)
        mx += mouse_X * rotSpeed * Time.deltaTime; 
        my += mouse_Y * rotSpeed * Time.deltaTime;

        //상하 회전을 -90도에서 90도로 제한
        my = Mathf.Clamp(my, -90f, 90f);

        //카메라 회전
        transform.eulerAngles = new Vector3(-my, mx, 0); //xy위치 주의
    }
}
