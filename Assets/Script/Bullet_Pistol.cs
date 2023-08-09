using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Pistol : MonoBehaviour
{
    EnemyShooting eShooting;
    bool isFire_Pist = false;
    [SerializeField]
    float pistBulletSpeed = 10.0f;

    public LayerMask mask;

    private void FixedUpdate()
    {
        if(isFire_Pist)
        {
            float delta = Time.fixedDeltaTime * pistBulletSpeed;
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, delta, mask))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    isFire_Pist = false;
                    transform.position = hit.point;
                    Destroy(gameObject);
                    eShooting = hit.transform.gameObject.GetComponent<EnemyShooting>();
                    eShooting.currentHp_Enemy -= eShooting.damage_Enemy;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.Translate(Vector3.forward * delta);
            }
        }
    }

    public void OnFire_Pistol()
    {
        transform.SetParent(null);
        isFire_Pist = true;
    }
}
