using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*  
 *  Keeps track of points and spawns items.
 *  
 */

public class GameManager : MonoBehaviour
{

    private Dictionary<string, int> playersPoints = new Dictionary<string, int>();
    public Text pointsDisplay;
    private int maxPoint = 0;
    private int winnerPointAmount = 5;

    public float waitOnBegin = 3f;
    public float waitOnEnd = 3f;

    public Transform ammoBox;
    public Transform[] ammoSpawnPoints;

    void Start () {
        //StartCoroutine(GameLoop());
    }

    //----------------------------------------------------------
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //StartCoroutine(GameLoop());
            //AddPoint("PlayerOne");
        }
    }

    public void StartGame()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameBegin());
        StartCoroutine(Spawner());
        yield return StartCoroutine(GameGoing());
        yield return StartCoroutine(GameEnding());
    }

    // adds a point to player and checks for max point
	public void AddPoint (string playerName)
    {
        playersPoints[playerName]++;
        foreach (int i in playersPoints.Values)
        {
            if (i > maxPoint)
            {
                maxPoint = i;
                print(maxPoint);
            }
        }
        DisplayPoints();
    }

    // shows points at the top of the screen
    private void DisplayPoints ()
    {
        string text = "";
        foreach (string playerName in playersPoints.Keys)
        {
            text += playerName + ": " + playersPoints[playerName].ToString();
            text += "\n";
        }
        pointsDisplay.text = text;
    }

    //spawns items on a random spawn point choosen from an array
    private IEnumerator Spawner()
    {
        while (true)
        {
            //float posX = Random.Range(-63f, 63f);
            //float posZ = Random.Range(-38.5f, 38.5f);
            //Vector3 spawnPos = new Vector3(posX, 0, posZ);

            Transform spawnerPoint = ammoSpawnPoints[Random.Range(0, ammoSpawnPoints.Length)];
            Vector3 spawnPos = new Vector3(spawnerPoint.position.x, 0, spawnerPoint.position.z);

            float spawnTimer = Random.Range(5f, 15f);

            yield return new WaitForSeconds(spawnTimer);

            if (Physics.OverlapSphere(spawnPos, 1).Length == 0)
            {
                Transform box = Instantiate(ammoBox, spawnPos, Quaternion.identity);
                if (spawnerPoint.name == "mine")
                {
                    box.GetComponent<AmmoBox>().which = "mine";
                }
                else
                {
                    box.GetComponent<AmmoBox>().which = "shell";
                }
                box.GetComponent<AmmoBox>().enabled = true;
            }
        }        
    }

    // sets up players and their point counters
    private IEnumerator GameBegin()
    {
        yield return new WaitForSeconds(waitOnBegin);
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Player"))
        {
            i.GetComponent<PlayerInput>().enabled = true;
            playersPoints.Add(i.name, 0);
        }
    }

    // runs until someone wins
    private IEnumerator GameGoing()
    {
        while (maxPoint < winnerPointAmount)
        {
            yield return null;
        }
    }

    // disables ships and loads main menu
    private IEnumerator GameEnding()
    {
        foreach (string i in playersPoints.Keys)
        {
            GameObject.Find(i).GetComponent<PlayerInput>().enabled = false;
            GameObject.Find(i).GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        yield return new WaitForSeconds(waitOnEnd);
        SceneManager.LoadScene("Menu");
    }
}
