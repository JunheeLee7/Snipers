using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    TestNav nav;

    Animator anims;
    Bullet enemtBullet;
    Player player;

    public Transform target;
    public float angle = 60.0f;     // 각도
    public float radius = 10.0f;    // 반지름

    public float runAngle = 60.0f;  // 달리기 각도
    public float runRaius = 10.0f;  // 달리기 반지름

    public float rotateSpeed = 0.5f;    // 회전 속도

    public bool isHit = false;
    public bool isRun = false;
    public bool isDying = false;

    Color safe = new Color(0, 1, 0, 0.1f);
    Color runSafe = new Color(0, 1, 1, 0.1f);
    Color runFind = new Color(0, 0, 1, 0.1f);
    Color find = new Color(1, 0, 0, 0.1f);

    //총알 생성
    public GameObject enemyBullets;
    public Transform spotOfBullet;
    public GameObject shotEffect;
    public GameObject DropItem;
    public bool isItemInst = false;

    // 체력 및 죽음
    public float maxHp_Enemy = 10.0f;
    public float currentHp_Enemy;
    public float damage_Enemy = 2.0f;

    private void Start()
    {
        currentHp_Enemy = maxHp_Enemy;
        nav = GetComponent<TestNav>();
        anims = GetComponent<Animator>();
        if (target == null)
        {
            target = FindObjectOfType<Player>().transform;
        }

        player = FindObjectOfType<Player>();
        if(spotOfBullet == null)
        {
            spotOfBullet = transform.GetChild(2);
        }
    }

    private void Update()
    {
        Vector3 intervalVec = target.position - transform.position;

        float dot = Vector3.Dot(intervalVec.normalized, transform.forward);
        float theta = Mathf.Acos(dot);
        float degree = Mathf.Rad2Deg * theta;
        float rase = angle / 2.0f;

        float runDot = Vector3.Dot(intervalVec.normalized, transform.forward);
        float runTheta = Mathf.Acos(runDot);
        float runDegree = Mathf.Rad2Deg * runTheta;
        float runRase = runAngle / 2.0f;

        if (intervalVec.magnitude <= radius)
        {
            if (degree <= rase)
            {
                //Quaternion rot = Quaternion.LookRotation(target.position);
                //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 4 * Time.deltaTime);
                Quaternion rot = Quaternion.LookRotation((target.position - transform.position).normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 4 * Time.deltaTime);
                nav.ChangeState(TestNav.State.Hit);
            }
            else
            {
                isHit = false;
                if (nav.state != TestNav.State.Walk)
                {
                    nav.ChangeState(TestNav.State.Idle);
                    isHit = false;
                }
            }
        }

        else if (intervalVec.magnitude <= runRaius)
        {
            if (runDegree <= runRase)       // runDegree <= runRase 예비용
            {
                isHit = false;
                Quaternion rot = Quaternion.LookRotation((target.position - transform.position).normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5 * Time.deltaTime);
                nav.ChangeState(TestNav.State.Run);
            }
            else
            {
                if (nav.state != TestNav.State.Walk)
                {
                    nav.ChangeState(TestNav.State.Idle);
                    isHit = false;
                }
            }
        }

        else
        {
            if (nav.state != TestNav.State.Walk)
            {
                nav.ChangeState(TestNav.State.Idle);
                isHit = false;
            }
        }

        if (isDying)
        {
            intervalVec = Vector3.zero;
        }

        if(currentHp_Enemy <= 0)
        {
            currentHp_Enemy = 0;
            DieAnim();
        }
    }


    private void OnDrawGizmos()
    {
        if(isHit)
        {
            // 빨강, 공격
            Handles.color = find;
        }
        else
        {
            Handles.color = safe;
        }
        // Handles.color = isCol? find : safe;
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angle/ 2.0f, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angle / 2.0f, radius);

        if(isRun)
        {
            Handles.color = runFind;
        }
        else
        {
            Handles.color = runSafe;
        }
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, runAngle / 2.0f, runRaius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -runAngle / 2.0f, runRaius);
    }

    public void DieAnim()
    {
        isDying = true;
        if (isDying)
        {
            anims.SetTrigger("Dead");
            nav.ChangeState(TestNav.State.Die);
            StartCoroutine(DisappearEnemy(3.0f));
        }
    }

    IEnumerator DisappearEnemy(float t)     // 죽은 뒤 아이템 생성;
    {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
        GameObject ammon = Instantiate(DropItem);
        ammon.transform.position = transform.position;
    }
}
