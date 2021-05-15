using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject enemyShip;
    public float minDelay, maxDelay;

    float nextLaunchTime = 0; // время след. запуска

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControllerScript.instance.isStarted)
        {
            return;
        }

        if (Time.time > nextLaunchTime)
        {
            // Полетели
            float emitterSize = transform.localScale.x;
            float positionX = Random.Range( -emitterSize/2, emitterSize/2);
            float positionY = 0;
            float positionZ = transform.position.z;

            if (Random.value > 0.5f)
            {
                GameObject newAsteriod = Instantiate(asteroid, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
                newAsteriod.GetComponent<AsteroidScript>().scaleValue = Random.Range(0.5f, 2);
                newAsteriod.transform.localScale *= newAsteriod.GetComponent<AsteroidScript>().scaleValue;
            } else
            {
                GameObject newEnemy = Instantiate(enemyShip, new Vector3(positionX, positionY, positionZ), Quaternion.Euler(0, 0, 0));
            }

            

            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
