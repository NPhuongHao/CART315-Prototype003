using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    public GameObject fruitG_pickUp;
    public GameObject fruitO_pickUp;
    public GameObject fruitB_pickUp;

    //private float min_X = -4.25f, max_X = 4.25f, min_Y = -2.26f, max_Y = 2.26f;
    private float min_X, max_Y, max_X, min_Y;
    private float z_Pos = 5.8f;
    //{minX, maxY, maxX, minY}
    float[] area1 = new float[] { -4.38f, 2.27f, 4.35f, 1.98f};
    float[] area2 = new float[] { -4.38f, -1.95f, 4.35f, -2.3f };
    float[] area3 = new float[] { -3.62f, 1.47f, -0.55f, 0.3f };
    float[] area4 = new float[] { -0.05f, 1.47f, 3.7f, 0.3f };
    float[] area5 = new float[] { 0.42f, -0.22f, 3.52f, -1.39f };
    float[] area6 = new float[] { -3.63f, -0.28f, -0.13f, -1.4f };

    private Text score_Text;
    private int scoreCount;

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        score_Text = GameObject.Find("score").GetComponent<Text>();

        Invoke("StartSpawning", 0.5f);
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //void StartSpawning()
    //{
    //    StartCoroutine(SpawnPickUps());
    //}

    public void StartSpawning()
    {
        int randomPos = Random.Range(1, 6);
        
        //{minX, maxY, maxX, minY}
        if (randomPos == 1)
        {
            min_X = area1[0];
            max_Y = area1[1];
            max_X = area1[2];
            min_Y = area1[3];
        }
        if (randomPos == 2)
        {
            min_X = area2[0];
            max_Y = area2[1];
            max_X = area2[2];
            min_Y = area2[3];
        }
        if (randomPos == 3)
        {
            min_X = area3[0];
            max_Y = area3[1];
            max_X = area3[2];
            min_Y = area3[3];
        }
        if (randomPos == 4)
        {
            min_X = area4[0];
            max_Y = area4[1];
            max_X = area4[2];
            min_Y = area4[3];
        }
        if (randomPos == 5)
        {
            min_X = area5[0];
            max_Y = area5[1];
            max_X = area5[2];
            min_Y = area5[3];
        }
        if (randomPos == 6)
        {
            min_X = area6[0];
            max_Y = area6[1];
            max_X = area6[2];
            min_Y = area6[3];
        }

        var SpanPos = new Vector3(Random.Range(min_X, max_X), Random.Range(min_Y, max_Y), z_Pos);

        int random = Random.Range(0, 9);
        if (random <= 3)
        {
            Instantiate(fruitG_pickUp,
                    SpanPos,
                    Quaternion.identity);
        } if (random > 3 && random <= 6)
        {
            Instantiate(fruitO_pickUp,
                    SpanPos,
                    Quaternion.identity);
        } if (random > 6)
        {
            Instantiate(fruitB_pickUp,
                    SpanPos,
                    Quaternion.identity);
        }

    }

    public void CancelSpawning()
    {
        CancelInvoke("StartSpawning");
    }


    //IEnumerator SpawnPickUps()
    //{
        //yield return new WaitForSeconds(Random.Range(1f, 1.5f));
       // Instantiate(fruit_pickUp, 
       //             new Vector3(Random.Range(min_X, max_X), Random.Range(min_Y, max_Y),z_Pos), 
         //           Quaternion.identity);
        //        Invoke("StartSpawning", 3f);
        // Debug.Log("Fruit up");
    //}

    public void IncreaseScore()
    {
        scoreCount++;
        score_Text.text = "Score: " + scoreCount;
    }
} //class
