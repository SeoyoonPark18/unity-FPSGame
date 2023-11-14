using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{  
    public Material bgMaterial; //��� ���͸��� (���͸����� vector2 ������)
    public float speed = 0.2f; //��ũ�� �ӵ�

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 dir = Vector2.up; //���� ����
        bgMaterial.mainTextureOffset += dir * speed * Time.deltaTime; //�̵�
    }
}
