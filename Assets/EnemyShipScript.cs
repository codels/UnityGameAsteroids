using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipScript : MonoBehaviour
{
    public GameObject enemyExposion;
    public GameObject playerExposion;

    public GameObject lazerShot;
    public GameObject lazerGun;

    public float shotDelay;
    float nextShotTime = 0; // время след. выстрела

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody enemyShip = GetComponent<Rigidbody>();
        //enemyShip.transform.rotation = Quaternion.Euler(0, 180, 0);
        enemyShip.velocity = new Vector3(0, 0, -60);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControllerScript.instance.isStarted)
        {
            Destroy(gameObject);
        }

        if (Time.time > nextShotTime)
        {
            GameObject shot = Instantiate(lazerShot, lazerGun.transform.position, Quaternion.identity);
            shot.GetComponent<LazerScript>().speed *= -2;
            shot.GetComponent<LazerScript>().teamId = 1;
            nextShotTime = Time.time + shotDelay;
        }
    }

    // Срабатывает при начале столкновения
    // other - тот объект, с которым столкнулся корабль
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

        if (other.tag == "LaserShot")
        {
            if (other.GetComponent<LazerScript>().teamId == 1)
            {
                return;
            }
        }

        Destroy(gameObject); // Уничтожить корабль
        Instantiate(enemyExposion, transform.position, Quaternion.identity);

        if (other.tag == "Player")
        {
            other.GetComponent<PlayerScript>().end();
        }
        else
        {
            if (other.tag == "LaserShot" && other.GetComponent<LazerScript>().teamId == 0)
            {
                Destroy(other.gameObject); // Уничтожить второй объект
                GameControllerScript.instance.addScore(Mathf.RoundToInt(30.0f));
            }
        }
    }
}
