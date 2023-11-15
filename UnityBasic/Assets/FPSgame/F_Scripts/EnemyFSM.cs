using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{ 
    enum EnemyState //�� ���� ���
    {
        Idle, //���
        Move, //�̵�
        Attack, //����
        Return, //����
        Damaged, //�ǰ�
        Die //����
    }    

    EnemyState m_State; //�� ���� ����   
    CharacterController cc; //�� ĳ���� ��Ʈ�ѷ� ������Ʈ
    Transform player; //�÷��̾� Ʈ������ ������Ʈ
    public float findDistance = 8f; //�÷��̾� �߰� ����
    public float attackDistance = 2f; //�÷��̾� ���� ����
    public float moveSpeed = 3f; //�� �̵� �ӵ�
    public float currentTime = 0; //���� �ð�
    public float attackDelayTime = 2f; //���� ������ �ð�
    public int attackPower = 10; //�� ���ݷ�
    Vector3 originPos; //�ʱ� ��ġ ����
    Quaternion originRot; //�ʱ� ȸ���� ����
    public float moveDistance = 20f; //�ʱ���ġ�κ��� �̵� ���� ����
    public int enemyHp = 30; //�� ü��
    public int maxHp;
    public Slider hpSlider;
    Animator anim; //�ִϸ����� ����
    public GameObject gameOption; // restart/quit

    void Start()
    {
        cc = GetComponent<CharacterController>(); //�� ĳ������Ʈ�ѷ� �Ҵ�
        player = GameObject.Find("Player").transform; //�÷��̾� Ʈ������ �Ҵ�
        m_State = EnemyState.Idle; //������ �� ���¸� '���'�� ����
        originPos = transform.position; //�� �ʱ� ��ġ ����
        originRot = transform.rotation; //�� �ʱ� ȸ���� ����
        maxHp = enemyHp;
        anim = transform.GetComponentInChildren<Animator>(); //�ڽ� ������Ʈ�� �ִϸ����� ������Ʈ �Ҵ�
    }

    void Update()
    {
        //EnemyState üũ�ؼ� ���º� ��� ����
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
        }

        hpSlider.value = (float)enemyHp / (float)maxHp; //UI�� �� Hp ȭ�鿡 ǥ��
    }

    void Idle()
    {
        //���� �÷��̾�� ���� �Ÿ��� '�߰� ����'���� �۴ٸ�, ((Move�� ��ȯ))
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            Debug.Log("Idle -> Move");
            anim.SetTrigger("IdleToMove"); //�̵� �ִϸ��̼����� ��ȯ
        }
    }
    void Move()
    {
        //���� ���� ��ġ�� �ʱ� ��ġ�� �Ÿ��� '�̵� ���� ����'���� ũ�ٸ� ((Return ��ȯ))
        if(Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            Debug.Log("Move -> Return");
        }
        //���� �÷��̾�� ���� �Ÿ��� '���� ����'���� ũ�ٸ�, ((�÷��̾�� �̵�))
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized; //�̵� ���� (���� �÷��̾�� ���� ���� = �÷��̾� ��ġ- �� ��ġ)
            cc.Move(dir * moveSpeed * Time.deltaTime); //�÷��̾�� �̵� (����*�ӵ�*�ð�)
            transform.forward = dir; //�÷��̾ ���� ���� ��ȯ
        }
        else //���� �÷��̾�� ���� �Ÿ��� '���� ����'���� �۴ٸ�, ((Attack ��ȯ))
        {
            m_State = EnemyState.Attack;
            Debug.Log("Move -> Attack");
            currentTime = attackDelayTime; //�����ð��� ���� ������ �ð���ŭ �̸� ����
            anim.SetTrigger("MoveToAttackDelay"); //���� ��� �ִϸ��̼� ����
        }
    }

    void Attack()
    {
        //���� �÷��̾�� ���� �Ÿ��� '���� ����'���� �۴ٸ�, ((�÷��̾ ����))
        if (Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            //Vector3 dir = (player.position - transform.position).normalized; //�̵� ���� (���� �÷��̾�� ���� ���� = �÷��̾� ��ġ- �� ��ġ)
            //cc.Move(dir * moveSpeed * Time.deltaTime); //�÷��̾�� �̵� (����*�ӵ�*�ð�)

            currentTime += Time.deltaTime; //���� �ð� ����
            if(currentTime > attackDelayTime) //���� �ð����� �÷��̾� ����
            {
                Debug.Log("����");

                PlayerMoving pmObject = GameObject.Find("Player").GetComponent<PlayerMoving>(); 
                pmObject.DamageAction(attackPower); //�÷��̾��� PlayerMoving ��ũ��Ʈ�� DamageAction ȣ��
                currentTime = 0;
                anim.SetTrigger("StartAttack"); //���� �ִϸ��̼� ����
            }
        }
        else //���� �÷��̾�� ���� �Ÿ��� '���� ����'���� ũ�ٸ�, ((Move�� ��ȯ))
        {
            m_State = EnemyState.Move;
            Debug.Log("Attack -> Move");
            currentTime = 0;
            anim.SetTrigger("AttackToMove"); //�̵� �ִϸ��̼� ����
        }
    }
    
    void Return()
    {
        //���� �ʱ� ��ġ������ �Ÿ��� 0.1���� ũ�ٸ� ��� �ʱ� ��ġ ������ �̵�
        if(Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized; //����
            //'normalized'�� ������ ����ȭ. ��� ����: ��� ������ ���� ���̸� 1�� �����, ���⿡ ���� �̵� �ӵ��� �����ϰ� ����.
            cc.Move(dir * moveSpeed * Time.deltaTime); //�̵�
            transform.forward = dir; //���� �������� ���� ��ȯ
        }
        else //�׷��� �ʴٸ� �� ��ġ�� �ʱ� ��ġ�� ����, ((Idle�� ��ȯ))
        {
            transform.position = originPos;
            transform.rotation = originRot;

            m_State = EnemyState.Idle;
            Debug.Log("Return -> Idle");
            anim.SetTrigger("MoveToIdle"); //��� �ִϸ��̼����� ��ȯ
        }
    }

    //������ ó�� �ڷ�ƾ �Լ�
    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.5f); //�ǰ� ��Ǹ�ŭ ��ٸ�
        m_State = EnemyState.Move; //�̵� ���� ��ȯ

    }
    void Damaged()
    {
        StartCoroutine(DamageProcess()); //�ǰ� �ڷ�ƾ ����
    }

    public void HitEnemy(int hitPower) //������ ���� �Լ�
    {

        if(m_State == EnemyState.Damaged)
        {
            return;
        }

        enemyHp -= hitPower; //�÷��̾��� ���ݷ¸�ŭ �� ü�� ����
        Debug.Log("�� ü��: " +  enemyHp);
         if(enemyHp > 0) //�� ü���� 0���� ũ�� �ǰ�
        {
            m_State = EnemyState.Damaged;
            Damaged();
        }
        else //�� ü���� 0���� ������ ����
        {
            m_State=EnemyState.Die;
            Debug.Log("����");
            anim.SetTrigger("Die"); //���� �ִϸ��̼� ����
            Die();
        }        
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;
        yield return new WaitForSeconds(6f); //�ǰ� ��Ǹ�ŭ ��ٸ�
        Debug.Log("�� ����");
        Destroy(gameObject); //�� ����
        gameOption.SetActive(true); //������ ���� 1���̱� ������, ���� �� �ɼ� â ���

    }
    void Die()
    {
        StopAllCoroutines(); //���� ���� ���� �ڷ�ƾ ����
        StartCoroutine(DieProcess()); //���� �ڷ�ƾ ����
    }
}
