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

        //������ ��Ÿ���� ����
        //Vector3 dir = Vector3.right * h + Vector3.up * v;
        Vector3 dir = new Vector3(h, v, 0); //���� ����

        //transform.Translate(dir * speed * Time.deltaTime); //�̵� = ���� * �Ÿ�(�ӵ�*�ð�)
        transform.position += dir * speed * Time.deltaTime; //���� ����
    }
}
