using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject player; //�÷��̾� ���ӿ�����Ʈ ����
    Vector3 offset; //ī�޶�� �÷��̾� ������ �Ÿ� ����

    void Start()
    {
        offset = transform.position - player.transform.position; //�� ������Ʈ ������ �Ÿ� ��� = ������Ʈ1 ��ġ - ������Ʈ2 ��ġ

    }

    void LateUpdate() //��� Update()���� ����� �Ŀ� ����
    {
        //Main Camera�� Player�� ���� �̵�
        transform.position = player.transform.position + offset; //ī�޶� ��ġ = �÷��̾� ��ġ + ������ �Ÿ�

    }
}
