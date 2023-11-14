using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Bullet : MonoBehaviour
{  
    public float speed = 5f; //�̵� �ӵ�

    void Start()
    {
        
    }

    void Update()
    {      
        Vector3 dir = Vector3.up; //����
        transform.position += dir * speed * Time.deltaTime; //�̵�

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) //�±� ��
        {
            GameObject smObject = GameObject.Find("ScoreManager");
            ScoreManager sm = smObject.GetComponent<ScoreManager>();
            sm.currentScore += 10;
            sm.currentScoreUI.text = "Current Score: " + sm.currentScore.ToString();
        }      
    }
}
