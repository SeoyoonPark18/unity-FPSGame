using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{

    float speed = 5f;
    public float p_speed = 10f;
    int cnt;
    public TextMeshProUGUI cntText;
    public GameObject panel;

    void SetCount()
    {
        cntText.text = "Count : " + cnt.ToString(); //�ؽ�Ʈ ����
    }

    void Start() 
    {
        cnt = 0;
        SetCount();
        panel.SetActive(false);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
     
        h *= speed * Time.deltaTime; //�Ÿ� = �ӷ� * �ð�
        v *= speed * Time.deltaTime;

        //Player �̵�
        transform.Translate(h * Vector3.right); //�̵� = �Ÿ� * ����
        transform.Translate(v * Vector3.forward);

        //����Ŭ���� ����
        if(cnt >= 5)
        {
            panel.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision) //�浹 ����(������)
    {
        Debug.Log("�浹 ����!");
    }

    private void OnTriggerEnter(Collider other) //Ʈ���� ����(����, isTrigger üũ)
    {
        if (other.gameObject.CompareTag("Item")) //�±� ��
        {
            other.gameObject.SetActive(false); //������ ��Ȱ��ȭ
            cnt++; //������ ī��Ʈ ����
            SetCount();
        }
    }
}

/*
����Ƽ-���־�Ʃ��� ����
Edit - Preferences - External Tools - External Script Editor - Visual Studio
*/