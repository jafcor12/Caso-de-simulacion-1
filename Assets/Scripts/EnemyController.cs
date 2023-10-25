using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject enemyObjectPrefab;

    [SerializeField]
    int enemyAmount = 3;

    [SerializeField]
    float enemyRange = 200.0F;

    [SerializeField]
    float spawningSafeRange = 200.0F;

    [SerializeField]
    Transform target;                           // nave principal 

    [SerializeField]
    float speed = 5.0f;                         // velocidad a la que sigue la nave

    [SerializeField]
    float stoppingDistance = 10.0f;

    List<GameObject> spawningObjects = new List<GameObject>();

    Vector3 spawningPoint;

    private void Start()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            GetSpawningPoint();
            while (Vector3.Distance(spawningPoint, Vector3.zero) < spawningSafeRange)
            {
                GetSpawningPoint();
            }

            GameObject spawningObject = Instantiate(enemyObjectPrefab, spawningPoint,
                Quaternion.Euler(Random.Range(0.0F, 360.0F), Random.Range(0.0F, 360.0F), Random.Range(0.0F, 360.0F)));

            spawningObject.transform.parent = this.transform;
            spawningObjects.Add(spawningObject);
        }
    }

    void Update()
    {

        Vector3 direction = target.position - transform.position;                   // calcula diferencia entre los objetos
        float distance = direction.magnitude;                                       // determina la distancia entre el target y el objecto


        if (distance > stoppingDistance)                                            // mientras este mas largo que el stopping distance va a moverse al target
        {
            transform.LookAt(target.position);                                      // gira al target
            transform.Translate(Vector3.forward * speed * Time.deltaTime);                   // Se mueve hacia el target
        }


        //Vector3 targetPosition = new Vector3(2.0f, 0.0f, 0.0f); // Replace with your target position.
        //transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);




    }

    void GetSpawningPoint()
    {
        spawningPoint = new Vector3(Random.Range(-1.0F, 1.0F), Random.Range(-1.0F, 1.0F), Random.Range(-1.0F, 1.0F));
        if (spawningPoint.magnitude > 1.0F)
        {
            spawningPoint.Normalize();
        }
        spawningPoint *= enemyRange;
    }
}
