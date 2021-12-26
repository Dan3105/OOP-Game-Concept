using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public bool isPlaying;
    public int score;
    public int rangeRespawn;

    public TMP_Text text;

    public static GameManager gameManager;
    public Transform player;
    public GameObject loseScence;

    private int countingPrefabs;
    private Queue<GameObject> aliveObject = new Queue<GameObject>();
    public List<GameObject> listPrefabs = new List<GameObject>();

    private void Awake()
    {
        isPlaying = true;
        score = 0;

        gameManager = this;
        loseScence.SetActive(false);

        Time.timeScale = 1;
        InvokeRepeating("SpawnPower", 5.0f, 6.0f);
        InvokeRepeating("SpawnGem", 1.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (aliveObject.Count > 7 && isPlaying)
        {
            Object.Destroy(aliveObject.Dequeue());

        }

        if (!isPlaying)
        {
            Time.timeScale = 0;
            loseScence.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void SpawnGem()
    {
        GameObject temp = Instantiate(listPrefabs[0], RespawnRandom(), transform.rotation);
        aliveObject.Enqueue(temp);
    }

    void SpawnPower()
    {
        GameObject temp = Instantiate(listPrefabs[1], RespawnRandom(), transform.rotation);
        aliveObject.Enqueue(temp);
    }

    public void UpdateScore(int t)
    {
        text.text = "Score: " + t.ToString();
    }

    Vector3 RespawnRandom()
    {
        float randomX = Random.Range(-rangeRespawn, rangeRespawn);
        float randomZ = Random.Range(-rangeRespawn, rangeRespawn);

        return new Vector3(randomX, 1, randomZ);
    }
}
