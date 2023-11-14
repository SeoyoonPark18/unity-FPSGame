using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //방향을 나타내는 변수
        //Vector3 dir = Vector3.right * h + Vector3.up * v;
        Vector3 dir = new Vector3(h, v, 0); //위와 동일

        //transform.Translate(dir * speed * Time.deltaTime); //이동 = 방향 * 거리(속도*시간)
        transform.position += dir * speed * Time.deltaTime; //위와 동일
    }
}
