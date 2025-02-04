using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;


public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 moveInput;
    
    [SerializeField]
    private float moveSpeed = 3f;
    //Note: could change move speed through items in the future

    // Start is called before the first frame update
    void Start()
    {
        //removes the need to call it every frame
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Raw only allows -1, 0, or 1
        //Will prevent issues that may arise from controller use

        moveInput.x = Input.GetAxisRaw("Horizontal"); // X input
        moveInput.y = Input.GetAxisRaw("Vertical"); // Y input

        //Prevent weird behavior causing fast diagonal movement
        //Effectively turns the input vector into a unit vector that only stores direction and not magnitude
        moveInput.Normalize();
    }

    private void FixedUpdate()
    {
        //Apply the input to velocity
        rb.velocity = moveInput * moveSpeed;
    }
}
