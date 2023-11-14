using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{ 
    float currentTime; //현재 시간   
    public float createTime; //일정 시간   
    public GameObject enemyFactory; //적 공장
    float minTime = 1f; //최소 시간
    float maxTime = 5f; //최대 시간

    void Start()
    {
        createTime = UnityEngine.Random.Range(minTime, maxTime);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        //현재 시간이 일정 시간보다 커지면
        if (currentTime > createTime)
        {
            //적 공장에서 적 생성 및 배치
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.position; //자기 위치에
            currentTime = 0; //현재 시간 초기화
            createTime = UnityEngine.Random.Range(minTime, maxTime);
        }
    }
}
