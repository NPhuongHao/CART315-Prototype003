using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private PlayerController playerController;

    private int horizontal = 0, vertical = 0;
    public enum Axis
    {
        Horizontal,
        Vertical
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //reset hori and vert values
        horizontal = 0;
        vertical = 0;

        GetKeyboardInput();
        SetMovement();
        
    }

    public void GetKeyboardInput()
    {
        //horizontal = (int)Input.GetAxisRaw("Horizontal");//input.getaxisRaw returns a float
        //vertical = (int)Input.GetAxisRaw("Vertical"); // so (int) converts float into integer

        horizontal = GetAxisRaw(Axis.Horizontal);
        vertical = GetAxisRaw(Axis.Vertical);

        if (horizontal != 0)
        {
            vertical = 0;//prevent double movement
        }
    }

    void SetMovement()
    {
        if (vertical != 0)
        {
            //call playercontroller to change movement
            playerController.SetInputDirection((vertical == 1) ?
                PlayerDirection.UP : PlayerDirection.DOWN); //if vertical = 1 then playerdirection = up, otherwise it = down


        } else if (horizontal != 0)
        {
            playerController.SetInputDirection((horizontal == 1) ?
                 PlayerDirection.RIGHT : PlayerDirection.LEFT); //if vertical = 1 then playerdirection = up, otherwise it = down

        }
    }

    int GetAxisRaw(Axis axis)
    {
        if (axis == Axis.Horizontal)
        {
            bool left = Input.GetKeyDown(KeyCode.LeftArrow);
            bool right = Input.GetKeyDown(KeyCode.RightArrow);

            if (left)
            {
                Debug.Log("left key");
                return -1;
            }

            if (right)
            {
                Debug.Log("right key");
                return 1;
            }
            else
            {
                return 0;
            }
        }

        else if (axis == Axis.Vertical)
        {
            bool up = Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);

            if (up)
            {
                return 1;
            }
            if (down)
            {
                return -1;
            }
            else
            {
                return 0;
            }

        }
        return 0;
    }
}
