using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{ 
    float currentTime; //���� �ð�   
    public float createTime; //���� �ð�   
    public GameObject enemyFactory; //�� ����
    float minTime = 1f; //�ּ� �ð�
    float maxTime = 5f; //�ִ� �ð�

    void Start()
    {
        createTime = UnityEngine.Random.Range(minTime, maxTime);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        //���� �ð��� ���� �ð����� Ŀ����
        if (currentTime > createTime)
        {
            //�� ���忡�� �� ���� �� ��ġ
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.position; //�ڱ� ��ġ��
            currentTime = 0; //���� �ð� �ʱ�ȭ
            createTime = UnityEngine.Random.Range(minTime, maxTime);
        }
    }
}
