using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLabel;
    public UnityEngine.UI.Text endLabel;
    public GameObject menu;
    public UnityEngine.UI.Button startButton;
    public GameObject player;
    public Vector3 startPosition;

    public int score = 0;

    public static GameControllerScript instance;

    public bool isStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        startButton.onClick.AddListener(delegate {
            setStatusPlay(true);
        });
        this.startPosition = player.transform.position;
        this.endLabel.enabled = false;
    }

    public void setStatusPlay(bool newIsStarted)
    {
        isStarted = newIsStarted;
        menu.SetActive(!newIsStarted);
        GameControllerScript.instance.endLabel.enabled = false;

        if (newIsStarted)
        {
            score = 0;
            player.SetActive(true);
            player.transform.position = this.startPosition;
            player.GetComponent<PlayerScript>().isAlive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Score :" + score;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setStatusPlay(!isStarted);
        }
    }

    public void addScore(int value)
    {
        if (!player.GetComponent<PlayerScript>().isAlive)
        {
            return;
        }
        GameControllerScript.instance.score += value;
    }
}
