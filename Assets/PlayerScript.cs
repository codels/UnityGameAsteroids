using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float xMin, xMax, zMin, zMax;
    public float speed;
    public float tilt;
    public bool isAlive;

    public GameObject lazerShot;
    public GameObject lazerGun;

    public GameObject playerExposion;

    public float shotDelay;
    float nextShotTime = 0; // время след. выстрела

    Rigidbody Ship;

    // Start is called before the first frame update
    void Start()
    {
        Ship = GetComponent<Rigidbody>();
        // Ship.velocity = new Vector3(10, 0, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControllerScript.instance.isStarted)
        {
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal"); // -1 ... +1
        float moveVertical = Input.GetAxis("Vertical"); // -1 ... +1

        Ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        float clampedX = Mathf.Clamp(Ship.position.x, xMin, xMax);
        float clampedZ = Mathf.Clamp(Ship.position.z, zMin, zMax);

        Ship.position = new Vector3(clampedX, 0, clampedZ);

        Ship.rotation = Quaternion.Euler(Ship.velocity.z * tilt, 0, -Ship.velocity.x * tilt);

        if (Time.time > nextShotTime & Input.GetKey(KeyCode.Space))
        {
            GameObject shot = Instantiate(lazerShot, lazerGun.transform.position, Quaternion.identity);
            shot.GetComponent<LazerScript>().teamId = 0;
            nextShotTime = Time.time + shotDelay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LaserShot" && other.GetComponent<LazerScript>().teamId == 1)
        {
            this.end();
        }
    }

    public void end()
    {
        Instantiate(playerExposion, this.transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
        GameControllerScript.instance.endLabel.enabled = true;
        GameControllerScript.instance.endLabel.text = "ТЫ НУБАС\nДаже codels сделал " + (GameControllerScript.instance.score + 50) + " очков";
    }
}
