using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasserController : MonoBehaviour
{
    [SerializeField]
    float force;

    [SerializeField]
    float lifespan = 5.0f; 

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * force;

        StartCoroutine(DestroyAfterLifespan());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterLifespan()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}