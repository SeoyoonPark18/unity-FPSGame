using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //�浹���� ��
    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(bombEffect); //ȿ�� ������ ����
        eff.transform.position = transform.position; //��ź ��ġ�� ��ġ ����
        Destroy(gameObject); //��ź ���� (�ڱ� �ڽ�)   
    }
}
