using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerScript : MonoBehaviour
{
    public float speed;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameControllerScript.instance.isStarted)
        {
            return;
        }

        float shift = Mathf.Repeat(Time.time *speed, 150); // 0 1 50 100 150 ... 0

        transform.position = startPosition + new Vector3(0, 0, -shift);
    }
}
