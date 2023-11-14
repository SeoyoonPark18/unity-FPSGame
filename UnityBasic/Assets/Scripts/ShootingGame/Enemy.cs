using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Enemy : MonoBehaviour
{
    public float speed = 7f;
    public GameObject explosionFactory; //폭발 공장

    void Start()
    {
        
    }

    void Update()
    {
        //적 이동
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {      
        GameObject explosion = Instantiate(explosionFactory); //폭발 효과 생성
        explosion.transform.position = transform.position;

        Destroy(gameObject); //적(자신) 제거
        Destroy(collision.gameObject); //플레이어(닿은 오브젝트) 제거
    }
}
