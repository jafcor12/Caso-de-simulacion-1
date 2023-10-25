using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceshipController : MonoBehaviour
{
    public static Transform target;                           // nave principal 

    [SerializeField]
    float speed = 5.0f;                         // velocidad a la que sigue la nave

    [SerializeField]
    float stoppingDistance = 10.0f;

    [SerializeField]
    int puntosDeVida = 10;

    [SerializeField]

    GameObject explotionPrefab;

    float _distanceToTarget;

    void Awake()
    {
        // Asigna la nave principal a la variable estática cuando el juego comienza.
        target = GameObject.Find("Spaceship").transform;
    }

    void Update()
    {

        Vector3 direction = target.position - transform.position;                   // calcula diferencia entre los objetos
        float distance = direction.magnitude;                                       // determina la distancia entre el target y el objecto


        if (distance > stoppingDistance)                                            // mientras este mas largo que el stopping distance va a moverse al target
        {
            transform.LookAt(target.position);                                      // gira al target
            transform.Translate(Vector3.forward * speed * Time.deltaTime);                   // Se mueve hacia el target

            //transform.position = target.position - transform.position * _distanceToTarget * speed * Time.deltaTime;
        }


        //Vector3 targetPosition = new Vector3(2.0f, 0.0f, 0.0f); // Replace with your target position.
        //transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);




    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            DestruirBala(other.gameObject);
            puntosDeVida--;

            if(puntosDeVida <= 0)
            {
                DestruirNave();
            }
        }
        
    }

    void DestruirBala(GameObject laser)
    {
        Destroy(laser);
    }

    void DestruirNave()
    {
        if(explotionPrefab != null)
        {
            Instantiate(explotionPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
