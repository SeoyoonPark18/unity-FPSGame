using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float destroyTime = 2f; //���� �ð�
    float currentTime = 0; //��� �ð�

    void Start()
    {
        
    }

    void Update()
    {
        if(currentTime > destroyTime)
        {
            Destroy(gameObject); //����Ʈ ����
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }
}
