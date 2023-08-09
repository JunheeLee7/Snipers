using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour
{
    Rigidbody rigid;
    Player player;
    CapsuleCollider capsuleCollider;


    public float throwAngle = 90.0f;
    public float throwForce = 10.0f;
    public float length = 10.0f;

    public GameObject smoke;
    public GameObject smoke2;
    public bool isStep = false;

    public float rayLength = 0.00001f;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        Vector3 startPos = transform.position;
        Ray ray = new Ray(startPos, Vector3.right);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, rayLength))
        {
            transform.position = hit.point;
            Debug.Log("Rel");
        }
    }

    public void ShootGrenades()
    {
        capsuleCollider.isTrigger = false;
        rigid = GetComponent<Rigidbody>();
        Vector3 targetPos = player.transform.position + transform.forward * length;
        Vector3 direction = (targetPos - player.transform.position).normalized;
        // Quaternion.Euler(throwAngle, 0, 0) * rigid.transform.forward;e
        Vector3 angle = Camera.main.transform.rotation.eulerAngles;
        angle.x -= 60;
        direction =  Quaternion.Euler(angle) * Vector3.forward;
        rigid.AddForce(direction * throwForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Elxz");
            GameObject obj = Instantiate(smoke);
            obj.transform.position = transform.position;
            GameObject obj2 = Instantiate(smoke2);
            obj2.transform.position = transform.position;
        }
    }
}
