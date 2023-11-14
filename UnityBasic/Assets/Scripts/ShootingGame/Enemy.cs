using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Enemy : MonoBehaviour
{
    public float speed = 7f;
    public GameObject explosionFactory; //���� ����

    void Start()
    {
        
    }

    void Update()
    {
        //�� �̵�
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {      
        GameObject explosion = Instantiate(explosionFactory); //���� ȿ�� ����
        explosion.transform.position = transform.position;

        Destroy(gameObject); //��(�ڽ�) ����
        Destroy(collision.gameObject); //�÷��̾�(���� ������Ʈ) ����
    }
}
