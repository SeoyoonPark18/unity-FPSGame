using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 1000f; //ȸ�� �ӵ� ����
    float mx = 0; float my = 0; 

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager2.gm.gState != GameManager2.GameState.Go) //Go ���¿����� ���� ����
        {
            return;
        }

        Cursor.lockState = CursorLockMode.Confined; // ����ȭ�� ������ ���콺 Ŀ�� ������ ���ϰ� (�÷��� ������ ��Ʈ��P)

        //���콺 �Է�
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        //ȸ�� �� ������ ���콺 �Է� �� ���� (�̵�=����*�Ÿ�)
        mx += mouse_X * rotSpeed * Time.deltaTime; 
        my += mouse_Y * rotSpeed * Time.deltaTime;

        //���� ȸ���� -90������ 90���� ����
        my = Mathf.Clamp(my, -90f, 90f);

        //ī�޶� ȸ��
        transform.eulerAngles = new Vector3(-my, mx, 0); //xy��ġ ����
    }
}
