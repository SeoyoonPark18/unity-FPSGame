using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //�Ѿ� ���� (������Ʈ Ǯ)
    public GameObject bulletFactory;
    //�߻� ��ġ
    public GameObject firePosition;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //�Ѿ� ���忡�� �Ѿ� ����
            GameObject bullet = Instantiate(bulletFactory);
            //�߻� ��ġ���� �Ѿ� �߻�
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
