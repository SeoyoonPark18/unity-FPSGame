using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 200f;
    float mx = 0; //x��(y��)���θ� ȸ��

    void Start()
    {
        
    }

    void Update()
    {
        float mouse_X = Input.GetAxis("Mouse X");
        mx += mouse_X * rotSpeed * Time.deltaTime; //�̵�=����*�Ÿ�
        transform.eulerAngles = new Vector3(0, mx, 0); //y�� ȸ��
    }
}
