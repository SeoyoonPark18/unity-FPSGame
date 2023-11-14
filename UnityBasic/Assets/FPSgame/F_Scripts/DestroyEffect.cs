using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float destroyTime = 2f; //지속 시간
    float currentTime = 0; //경과 시간

    void Start()
    {
        
    }

    void Update()
    {
        if(currentTime > destroyTime)
        {
            Destroy(gameObject); //이펙트 제거
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }
}
