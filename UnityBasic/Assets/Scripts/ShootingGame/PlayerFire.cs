using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //총알 공장 (오브젝트 풀)
    public GameObject bulletFactory;
    //발사 위치
    public GameObject firePosition;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //총알 공장에서 총알 생성
            GameObject bullet = Instantiate(bulletFactory);
            //발사 위치에서 총알 발사
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
