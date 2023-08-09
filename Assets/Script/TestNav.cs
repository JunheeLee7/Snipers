using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class TestNav : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk,
        Run,
        Hit,
        Die
    }

    public State state = State.Idle;

    EnemyShooting eShoot;

    NavMeshAgent agent;
    Animator animator;

    public Transform way1;
    public Transform way2;
    public Transform way3;
    private  Transform[] waypoints;
    public int currentPoint;
    int dir = 1;
    Coroutine cor;

    public bool isStop = false;

    public Transform target;
    public Transform effectPos;
    public GameObject hitEffect;

    Bullet bullet;
    public GameObject bulletPrefabs;
    private float maxDistance = 100.0f;
    public LayerMask lay;
    public bool isRay = false;

    public int saveSet = 0;

    private void Start()
    {
        if(saveSet > 0)
        {
            gameObject.SetActive(false);
        }
        eShoot = GetComponent<EnemyShooting>();
        waypoints = new Transform[3];
        waypoints[0] = way1;
        waypoints[1] = way2;
        waypoints[2] = way3;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentPoint = 0;
        ChangeState(State.Walk);
    }

    private void Update()
    {
        StateProcess();
        Vector3 dis = target.position - transform.position;
        Ray ray = new Ray(transform.position, dis);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, lay))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                isRay = true;
            }
            else
            {
                isRay = false;
            }
        }
        else
        {
            isRay = false;
        }
    }

    void MoveToNext()
    {
        currentPoint += dir;

        if (currentPoint == waypoints.Length)
        {
            dir = -1;
            currentPoint -= 2;
        }
        if (currentPoint == 0)
        {
            dir = 1;
        }
        StartCoroutine(Moving(waypoints[currentPoint].position, MoveToNext));
    }

    IEnumerator Moving(Vector3 pos, UnityAction done)
    {
        agent.SetDestination(pos);                                   // pos 를 목표거리로 지정
        while (agent.pathPending || agent.remainingDistance > 0.15f)  // 남은거리가 0 or patthpending이 true인경우(pathpending : agent가 움직이고 있는지 확인) : 이동
        {
            //이동중
            animator.SetBool("IsWalk", true);
            yield return null;
        }
        //도착
        animator.SetBool("IsWalk", false);
        done?.Invoke();                                             // currentPoint를 조정하는 델리게이트실행
    }

    IEnumerator Running()
    {
        animator.SetBool("IsAim", false);
        animator.SetBool("IsRun", true);
        agent.SetDestination(target.position);
        if (cor != null)
        {
            StopCoroutine(cor);
        }
        Debug.Log("Running");
        yield return null;
    }

    IEnumerator HitBullet(float t)
    {
        if (cor != null)
        {
            StopCoroutine(cor);
        }

        Debug.Log("Aim");
        animator.SetBool("IsAim", true);
        Vector3 now = transform.position;
        transform.position = now;

        while (true)
        {
            Debug.Log("Hit");
            yield return new WaitForSeconds(t);
            if (isRay)
            {
                agent.SetDestination(transform.position);
                animator.SetTrigger("Fire");
                GameObject obj = Instantiate(hitEffect);
                obj.transform.position = effectPos.position;
                Destroy(obj, 0.3f);
                bullet = (Instantiate(bulletPrefabs, transform) as GameObject).GetComponent<Bullet>();
                bullet.OnFire();
            }
            else
            {
                animator.SetBool("IsAim", false);
                animator.SetBool("IsRun", true);
                agent.SetDestination(target.position);
            }
        }
    }

    IEnumerator idleCor(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(State.Walk);
    }
    private void IdleAnim()
    {
        StopAllCoroutines();
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsAim", false);
        animator.SetBool("IsRun", false);

        Debug.Log("IDLE");
        StartCoroutine(idleCor(1.5f));
    }
    private void Dead()
    {
        if(eShoot.isDying)
        {
            StopAllCoroutines();
            agent.isStopped = true;
            saveSet = 1;
        }
    }

    public void ChangeState(State s, bool excute = false)
    {
        if (!excute && state == s)
            return;
        state = s;
        StopAllCoroutines();
        switch (state)
        {
            case State.Idle:
                IdleAnim();
                break;
            case State.Walk:
                cor = StartCoroutine(Moving(waypoints[currentPoint].position, MoveToNext));
                break;
            case State.Run:
                agent.SetDestination(transform.position);
                StartCoroutine(Running());
                break;
            case State.Hit:
                StopAllCoroutines();
                agent.SetDestination(transform.position);
                StartCoroutine(HitBullet(1.5f));
                break;
            case State.Die:
                Dead();
                break;
        }
    }

    public void StateProcess()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Walk:
                break;
            case State.Run:
                break;
            case State.Hit:
                break;
            case State.Die:
                break;
        }
    }   
}
