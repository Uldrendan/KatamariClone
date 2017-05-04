using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatamariController : MonoBehaviour {

    public float speed;

    public float pickupableRatio = 1;

    Rigidbody _rb;
    bool boostReady = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        _rb.AddForce(movement * speed);

        //just a boost in case you get stuck
        if (Input.GetKey("space") && boostReady)
        {
            boostReady = false;
            _rb.AddForce(movement * 10000);
            StartCoroutine(ResetBoost());
        }
    }

    IEnumerator ResetBoost()
    {
        yield return new WaitForSeconds(4);
        boostReady = true;
    }

    void OnCollisionEnter(Collision info)
    {
        if (info.gameObject.tag != "Pickup")
            return;

        //TODO: expand the collider to allow for easier rolling
        bool hitMainCollider = false;
        foreach (ContactPoint contact in info.contacts)
        {
            if (contact.thisCollider == GetComponent<SphereCollider>())
            {
                hitMainCollider = true;
                break;
            }
        }

        if (hitMainCollider)
        {
            if (info.rigidbody.mass < _rb.mass * pickupableRatio || info.rigidbody.isKinematic)
            {
                _rb.mass += info.rigidbody.mass;
                Destroy(info.rigidbody);
                info.transform.parent = transform;
                info.gameObject.GetComponent<BoxCollider>().enabled = false;
                info.gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }
    }
}