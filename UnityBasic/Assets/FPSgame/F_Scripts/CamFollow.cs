using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target; //CamPosition�� Ʈ������ ������Ʈ

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = target.position; //ī�޶� ��ġ�� Ÿ�� ��ġ�� ��ġ
    }
}
