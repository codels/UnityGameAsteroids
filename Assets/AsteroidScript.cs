using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    public float rotationSpeed;
    public float minSpeed, maxSpeed;

    public GameObject asteroidExposion;

    public float scaleValue;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        asteroid.velocity = new Vector3(0, 0, -Random.Range(minSpeed, maxSpeed));
    }

    // Срабатывает при начале столкновения
    // other - тот объект, с которым столкнулся Астероид
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            return;
        }

        if (other.tag == "GameBoundary")
        {
            return;
        }

        Destroy(gameObject); // Уничтожить астероид
        Instantiate(asteroidExposion, transform.position, Quaternion.identity);

        if (other.tag == "Player") {
            other.GetComponent<PlayerScript>().end();
        } else {
            if (other.tag == "LaserShot" && other.GetComponent<LazerScript>().teamId == 0)
            {
                Destroy(other.gameObject); // Уничтожить второй объект
                GameControllerScript.instance.addScore(Mathf.RoundToInt(10.0f * this.scaleValue));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControllerScript.instance.isStarted)
        {
            Destroy(gameObject);
        }
    }
}
