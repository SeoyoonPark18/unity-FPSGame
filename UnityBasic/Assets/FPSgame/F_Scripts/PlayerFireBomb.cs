using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBomb : MonoBehaviour
{
    public GameObject firePosition; //�߻� ��ġ
    public GameObject bombFactory; //��ź ������Ʈ
    public float throwPower = 15f; //��ź ��ô �Ŀ�
    public GameObject bulletEffect; //�Ѿ� ����Ʈ
    ParticleSystem ps; //��ƼŬ �ý���
    public int weaponPower = 10; //�Ѿ� ���ݷ�

    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //���콺 ������ ��ư �Է�
        if (Input.GetMouseButtonDown(1)) 
        {
            GameObject bomb = Instantiate(bombFactory); //��ź ����
            bomb.transform.position = firePosition.transform.position; //��ġ ����

            Rigidbody rb = bomb.GetComponent<Rigidbody>(); //��ź�� ������ٵ�
            //ī�޶��� �������� ��ź�� ���� ���� (Impulse�� ������ �������� ���� ����)
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse); 
        }

        //���콺 ���� ��ư �Է�
        if (Input.GetMouseButtonDown(0))
        {
            //����ĳ��Ʈ
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //���� ���� �� ��ġ�� ���� ����
            RaycastHit hitInfo = new RaycastHit(); //���̰� �ε��� ��� ����
            if (Physics.Raycast(ray, out hitInfo)) //���̸� �߻�, ����ĳ��Ʈ�� �ε��� ��ü�� �ִٸ�
            {               
                if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("FPSEnemy")) //���� �ε��� ����� ���̾ Enemy���
                {                   
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>(); //EnemyFSM�� HitEnemy �Լ� ���� (transform�� ������Ʈ�� ��ũ��Ʈ �ҷ��� �� ����)
                    eFSM.HitEnemy(weaponPower);
                }

                bulletEffect.transform.position = hitInfo.point; //�ε��� ��ü�� ��ġ��ǥ(point)
                bulletEffect.transform.forward = hitInfo.normal; //�ε��� ������ '���� ����'�� ��ġ
                // ���� ���Ͷ�: 
                ps.Play();
            }
        }
    }
}
