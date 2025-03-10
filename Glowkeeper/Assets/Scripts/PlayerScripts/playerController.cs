using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private Vector2 moveInput;

    private Animator animator;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    

    [SerializeField]

    public float baseMoveSpeed = 3f;

    public float movespeed;

    public float movespeedFlat = 0;
    public float movespeedScalar = 1;


    //Start is called before the first frame update
    void Start()
    {
        UpdateStats();
        
        //instantiate components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        //Prevent weird behavior causing fast diagonal movement
        //Effectively turns the input vector into a unit vector that only stores direction and not magnitude
        moveInput.Normalize();

        animator.SetFloat(_horizontal, moveInput.x);
        animator.SetFloat(_vertical, moveInput.y);
    }

    public void UpdateStats()

    {
        movespeed = Mathf.Clamp((baseMoveSpeed + movespeedFlat) * movespeedScalar, 0.5f, 10);
    }
}
