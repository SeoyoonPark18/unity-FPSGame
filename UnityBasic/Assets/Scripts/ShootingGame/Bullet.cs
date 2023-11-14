using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Bullet : MonoBehaviour
{  
    public float speed = 5f; //이동 속도

    void Start()
    {
        
    }

    void Update()
    {      
        Vector3 dir = Vector3.up; //방향
        transform.position += dir * speed * Time.deltaTime; //이동

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) //태그 비교
        {
            GameObject smObject = GameObject.Find("ScoreManager");
            ScoreManager sm = smObject.GetComponent<ScoreManager>();
            sm.currentScore += 10;
            sm.currentScoreUI.text = "Current Score: " + sm.currentScore.ToString();
        }      
    }
}
