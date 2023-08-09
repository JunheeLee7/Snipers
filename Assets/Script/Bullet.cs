using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Bullet : MonoBehaviour
{
    EnemyShooting eCon;
    HPManager hpManager;

    bool isFire = false;

    [SerializeField] float bulletSpeed = 10.0f;

    public Action attackEnemy;
    public LayerMask masks;

    public GameObject bloodEffect;
    private void Start()
    {
        // eCon = FindObjectOfType<EnemyShooting>();
    }

    private void FixedUpdate()
    {
        if(isFire)
        {
            float delta = Time.fixedDeltaTime * bulletSpeed;
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, delta, masks))
            {
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    attackEnemy?.Invoke();
                    isFire = false;
                    transform.position = hit.point;
                    Destroy(gameObject);
                    eCon = hit.transform.gameObject.GetComponent<EnemyShooting>();
                    eCon.DieAnim();
                }

                else
                {
                    if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                    {
                        Destroy(gameObject);
                    }
                    else if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        Debug.Log("ER");
                        hpManager = FindObjectOfType<HPManager>();
                        hpManager.currentHP -= hpManager.damage;
                        //GameObject obj = Instantiate(bloodEffect);
                        //obj.transform.position = hit.point;
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                transform.Translate(Vector3.forward * delta);
            }
        }
       
    }

    public void OnFire()
    {
        transform.SetParent(null);
        isFire = true;
    }

    IEnumerator bulletDisappear()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }
}
