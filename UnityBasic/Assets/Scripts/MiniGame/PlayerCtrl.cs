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
        cntText.text = "Count : " + cnt.ToString(); //텍스트 설정
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
     
        h *= speed * Time.deltaTime; //거리 = 속력 * 시간
        v *= speed * Time.deltaTime;

        //Player 이동
        transform.Translate(h * Vector3.right); //이동 = 거리 * 방향
        transform.Translate(v * Vector3.forward);

        //게임클리어 조건
        if(cnt >= 5)
        {
            panel.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision) //충돌 감지(물리ㅇ)
    {
        Debug.Log("충돌 감지!");
    }

    private void OnTriggerEnter(Collider other) //트리거 감지(관통, isTrigger 체크)
    {
        if (other.gameObject.CompareTag("Item")) //태그 비교
        {
            other.gameObject.SetActive(false); //아이템 비활성화
            cnt++; //아이템 카운트 증가
            SetCount();
        }
    }
}

/*
유니티-비주얼스튜디오 연동
Edit - Preferences - External Tools - External Script Editor - Visual Studio
*/