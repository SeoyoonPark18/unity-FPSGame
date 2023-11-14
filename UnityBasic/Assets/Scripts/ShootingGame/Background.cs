using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{  
    public Material bgMaterial; //배경 매터리얼 (매터리얼은 vector2 형식임)
    public float speed = 0.2f; //스크롤 속도

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 dir = Vector2.up; //방향 설정
        bgMaterial.mainTextureOffset += dir * speed * Time.deltaTime; //이동
    }
}
