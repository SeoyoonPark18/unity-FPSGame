using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 200f;
    float mx = 0; //x값(y축)으로만 회전

    void Start()
    {
        
    }

    void Update()
    {
        float mouse_X = Input.GetAxis("Mouse X");
        mx += mouse_X * rotSpeed * Time.deltaTime; //이동=방향*거리
        transform.eulerAngles = new Vector3(0, mx, 0); //y축 회전
    }
}
