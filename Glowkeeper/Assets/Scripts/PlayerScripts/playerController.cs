using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private Vector2 moveInput;
    

    [SerializeField]
    public float baseMoveSpeed = 3f;

    private float movespeed;

    public float movespeedFlat = 0;
    public float movespeedScalar = 1;

    //Start is called before the first frame update
    void Start()
    {
        //instantiate components
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        UpdateStats();
    }

    //Update is called once per frame
    void Update()
    {
        MovementInputHandler();
    }

    private void FixedUpdate()
    {
        //Apply the input to velocity
        rb.velocity = moveInput * movespeed;
    }

    //Handles the player's movement inputs
    void MovementInputHandler()
    {
        //Raw only allows -1, 0, or 1
        //Will prevent issues that may arise from controller use

        moveInput.x = Input.GetAxisRaw("Horizontal"); // X input
        moveInput.y = Input.GetAxisRaw("Vertical"); // Y input

        //Handle which direction the sprite faces
        if(moveInput.x > 0) //Face right
        {
            sprite.flipX = false;
        }
        else if(moveInput.x < 0) //Face left
        {
            sprite.flipX = true;
        }
        //No horizontal movement just uses whatever the set direction was last

        //Prevent weird behavior causing fast diagonal movement
        //Effectively turns the input vector into a unit vector that only stores direction and not magnitude
        moveInput.Normalize();
    }

    void UpdateStats()
    {
        movespeed = Mathf.Min((baseMoveSpeed + movespeedFlat) * movespeedScalar, 0.5f, 7);
    }
}
