using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoving : MonoBehaviour
{
    public float moveSpeed = 7f; //�̵� �ӷ� ����
    CharacterController cc; //ĳ���� ��Ʈ�ѷ� ������Ʈ
    float gravity = -20f; //�߷� ����
    float yVelocity = 0; //���� �ӷ� ����
    public float jumpPower = 7f; //������ ����
    bool isJump = false; //���� ���� ���� ����
    public int playerHp = 100; //�÷��̾� ü�� ����
    public int maxHp; //�ִ� ü��
    public Slider hpSlider; //ü�� �����̴� ����


    void Start()
    {
        cc = GetComponent<CharacterController>();
        maxHp = playerHp;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); //Ű���� �Է�
        float v= Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v); //���� ����
        dir = dir.normalized; //�Ų����� �̵� ���� normalized�� ������ �� �ֱ�
        dir = Camera.main.transform.TransformDirection(dir); //ī�޶� �������� ���� ��ȯ
      
        if(cc.collisionFlags == CollisionFlags.Below) //�ٴڿ� �����ߴٸ�
        {
            isJump = false;
            yVelocity = 0; //����ȭ��
        }        
        if (Input.GetButtonDown("Jump") && isJump == false) //�����̽��ٸ� ������ ����
        {
            yVelocity = jumpPower;
            isJump = true;
        }

        yVelocity += gravity * Time.deltaTime; //���� �ӵ��� �߷� �� ����
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime); //ĳ���� �̵�
        //transform.position += dir * moveSpeed * Time.deltaTime; //���� ����

        hpSlider.value = (float)playerHp / (float)maxHp; //���� ü���� �����̴��� value�� �ݿ�
        //Debug.Log(hpSlider.value);
    }

    public void DamageAction(int damage) //�÷��̾� �ǰ� �Լ�
    {
        playerHp -= damage; //���� ���ݷ¸�ŭ �÷��̾� ü�� ����
        if(playerHp < 0)
        {
            playerHp = 0; //ü���� ������� 0���� �ʱ�ȭ
        }
        Debug.Log("HP: " + playerHp);
    }
}
