using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FindMushroomScript : MonoBehaviour {

    [Tooltip("The audio clip to play when the coin lands")]
    public AudioSource yay;

    [Tooltip("The audio clip to play when coin misses")]
    public AudioSource fail;

    [Tooltip("The points awarded for winning!")]
    public int pointsRewarded;

    [Tooltip("Floating Score Prefab")]
    public ScoreHighlight ScoreHighlighterPrefab;

    private bool isGameActive;
    public int roundsInGame;
    public float roundTime;
    private int roundNumber;
    private float roundStart;
    public GameObject startMenu;
    public GameObject mushrooms;
    private GameObject[] arrayOfShrooms;
    public GameObject mushroomRed;
    public GameObject mushroomBlue;
    public GameObject mushroomPink;
    public GameObject mushroomPurple;
    public GameObject mushroomYellow;

    // Use this for initialization
    private void Awake()
    {
        isGameActive = false;
        roundNumber = 0;
        arrayOfShrooms = new GameObject[5];
        arrayOfShrooms[0] = mushroomRed;
        arrayOfShrooms[1] = mushroomBlue;
        arrayOfShrooms[2] = mushroomPink;
        arrayOfShrooms[3] = mushroomPurple;
        arrayOfShrooms[4] = mushroomYellow;
        foreach(GameObject shroom in arrayOfShrooms)
        {
            Debug.Log("setting active of shroom:" + shroom.name);
            shroom.SetActive(false);
        }
    }
    public void Update()
    {
        if(isGameActive && Time.time - roundStart > roundTime)
        {
            Debug.Log("Ending game because Time.time - roundStart was" + (Time.time - roundStart).ToString());
            EndGame(false);
        }
    }

    public void StartRound()
    {
        Debug.Log("Starting Round");
        Shuffle(arrayOfShrooms);
        for (int i = 0; i < arrayOfShrooms.Length; i++)
        {
            Debug.Log("Shuffling:" + arrayOfShrooms[i].name);
            float xPosition = i - 2;
            Debug.Log("Shuffling with location of:" + xPosition.ToString());
            arrayOfShrooms[i].transform.localPosition = new Vector3(xPosition, 0, 0);
            arrayOfShrooms[i].SetActive(true);
        }
        roundStart = Time.time;
        Debug.Log("roundStart = " + roundStart.ToString());
    }

    public void StartGame()
    {
        Debug.Log("Starting Game");
        startMenu.SetActive(false);
        StartRound();
        roundNumber = 0;
        isGameActive = true;
    }
    public void EndGame(bool won)
    {
        Debug.Log("Ending game with won = " + won.ToString());
        if (won)
        {
            CarnivalScores.Instance.IncrementShroomScore(pointsRewarded);
            ScoreHighlight sh = Instantiate(ScoreHighlighterPrefab, transform.position,
                transform.rotation);
            sh.SetPoints(pointsRewarded);
            yay.Play();
            Debug.Log("Won Game");
        }
        else
        {
            fail.Play();
            Debug.Log("Lost Game");
        }
        roundNumber = 0;
        isGameActive = false;
        foreach (GameObject shroom in arrayOfShrooms)
        {
            shroom.SetActive(false);
        }
        startMenu.SetActive(true);
    }

    public void ClickedShroom(bool wasRed = false)
    {
        Debug.Log("Clicked shroom with wasRed = " + wasRed.ToString());
        if (wasRed)
        {
            CompleteRound();
        }
        else
        {
            EndGame(false);
        }
    }

    public void CompleteRound()
    {
        if(roundNumber == roundsInGame - 1)
        {
            EndGame(true);            
        }
        else
        {
            roundNumber++;
            StartRound();
        }        
    }

    static void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            int r = i + Random.Range(0, n - i);
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }
}
