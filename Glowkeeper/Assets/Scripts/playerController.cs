using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private float horizontalMov;
    private float verticalMov;
    private Vector2 curVel;
    
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
        this.horizontalMov = Input.GetAxis("Horizontal"); // X input
        this.verticalMov = Input.GetAxis("Vertical"); // Y input

        this.curVel = this.rb.velocity; //get the current velocity
    }

    private void FixedUpdate()
    {
        if(this.horizontalMov != 0)
        {
            //change X velocity without affecting Y velocity
            this.rb.velocity = new Vector2(this.horizontalMov * this.moveSpeed, this.curVel.y);
        }

        /*
        if(this.verticalMov != 0)
        {
            //change X velocity without affecting Y velocity
            this.rb.velocity = new Vector2(curVel.x, this.verticalMov * this.moveSpeed);
        }
        */
    }
}
