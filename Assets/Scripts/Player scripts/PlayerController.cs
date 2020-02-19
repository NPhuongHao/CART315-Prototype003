using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] //hide the variable from the inspector
    public PlayerDirection direction;

    [HideInInspector]
    public float step_Length = 0.2f; //how much unit we're going to move every node

    [HideInInspector]
    public float movement_Frequency = 0.2f;//how many time we can move in a second (in this case every .1 sec)

    private float counter;
    private bool move;

    [SerializeField] //opposite to hideininspector
    private GameObject tailPrefab; //add this prefab to the snake when it eats the fruit

    [SerializeField]
    private Material green;
    [SerializeField]
    private Material blue;
    [SerializeField]
    private Material orange;

    private List<Vector3> delta_Position;

    private List<Rigidbody> nodes; //every part of the snake's body

    private Rigidbody main_Body;
    private Rigidbody head_Body;
    private Transform tr;

    private bool create_Node_At_Tail; //to tell if you need to add tail or not


    void Awake()
    {
        tr = transform;
        main_Body = this.GetComponent<Rigidbody>();

        InitSnakeNodes();//set up the snake
        InitPlayer();


        delta_Position = new List<Vector3>
        {
            new Vector3(-step_Length, 0f), //-dx ..LEFT
            new Vector3(0f, step_Length), // dy .. UP
            new Vector3(step_Length, 0f), // dx .. RIGHT
            new Vector3(0f, -step_Length) // -dy .. DOWN
        };
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementFrequency();//set movement speed
    }

    void FixedUpdate()
    {
        if (move)//if snake can move
        {
            move = false;//set the move determiner back to false

            Move();//execute the movement
        }
    }

    void InitSnakeNodes()//get all the snake parts
    {
        nodes = new List<Rigidbody>();

        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());//child index 0 of the GameObject
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());

        head_Body = nodes[0];

    }

    void SetDirectionRandom()
    {
        int dirRandom = Random.Range(0, (int)PlayerDirection.COUNT);
        direction = (PlayerDirection)dirRandom;
    }

    void InitPlayer()
    {
        direction = (PlayerDirection)2;

        switch(direction)
        {
            case PlayerDirection.RIGHT:
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;

            case PlayerDirection.LEFT:
                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;

            case PlayerDirection.UP:
                nodes[1].position = nodes[0].position - new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position - new Vector3(0f, Metrics.NODE * 2f, 0f);
                break;

            case PlayerDirection.DOWN:
                nodes[1].position = nodes[0].position + new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position + new Vector3(0f, Metrics.NODE * 2f, 0f);
                break;
        }
    }

    private void Move()
    {
        Vector3 dPosition = delta_Position[(int)direction];

        Vector3 parentPos = head_Body.position;
        Vector3 prevPosition;

        main_Body.position = main_Body.position + dPosition;
        head_Body.position = head_Body.position + dPosition;

        for (int i = 1; i<nodes.Count; i++)
        {
            prevPosition = nodes[i].position;

            nodes[i].position = parentPos;
            parentPos = prevPosition;
  
        }

        //check if we need to create a new node
        if (create_Node_At_Tail)
        {
            create_Node_At_Tail = false;
            GameObject newNode = Instantiate(tailPrefab, nodes[nodes.Count - 1].position, 
                                            Quaternion.identity);

            newNode.transform.SetParent(transform, true);
            nodes.Add(newNode.GetComponent<Rigidbody>());

        }
    }

    void CheckMovementFrequency()
    {
        counter += Time.deltaTime;
        if (counter >= movement_Frequency)
        {
            Debug.Log(movement_Frequency);
            Debug.Log(counter);
            counter = 0f;//reset counter
            move = true;
        }
    }

    public void SetInputDirection(PlayerDirection dir)
    {
        //prevent movement in the opposite direction
        if(dir == PlayerDirection.UP && direction == PlayerDirection.DOWN || 
           dir == PlayerDirection.DOWN && direction == PlayerDirection.UP ||
            dir == PlayerDirection.RIGHT && direction == PlayerDirection.LEFT ||
            dir == PlayerDirection.LEFT && direction == PlayerDirection.RIGHT)
        {
            return;//stop here
        }

        direction = dir;

        ForceMove();
    }

    void ForceMove()
    {
        counter = 0;//move the snake immediately without waiting for movement frequency threshold
        move = false; // reset move bool
        Move();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.FRUITG || other.tag == Tags.FRUITO || other.tag == Tags.FRUITB)
        {
            other.gameObject.SetActive(false);
            create_Node_At_Tail = true;

            if (other.tag == Tags.FRUITG)
            {
                transform.gameObject.tag = "snekgreen";
                head_Body.GetComponent<MeshRenderer>().material = green;
            }
            if (other.tag == Tags.FRUITO)
            {
                transform.gameObject.tag = "snekorange";
                head_Body.GetComponent<MeshRenderer>().material = orange;
            }
            if (other.tag == Tags.FRUITB)
            {
                transform.gameObject.tag = "snekblue";
                head_Body.GetComponent<MeshRenderer>().material = blue;
            }

            GameplayController.instance.IncreaseScore();
            GameplayController.instance.StartSpawning();
            
        }
        if(other.tag == Tags.WALLG || other.tag == Tags.WALLO || other.tag == Tags.WALLB)
        {
            if ((other.tag == Tags.WALLG && gameObject.tag == "snekgreen") || (other.tag == Tags.WALLB && gameObject.tag == "snekblue") || (other.tag == Tags.WALLO && gameObject.tag == "snekorange"))
            {
                Time.timeScale = 0f;
            }
        }
        if (other.tag == Tags.TAIL)
        {
            Time.timeScale = 0f;
        }
    }
}
