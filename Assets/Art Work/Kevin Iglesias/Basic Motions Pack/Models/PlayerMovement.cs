using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController ctrler;
    public float speed = 5f;
    public bool isLanded = false;
    public Vector3 move = Vector3.zero;
    public float gravity = -0.098f;
    public Animator A;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isLanded = ctrler.isGrounded;
        

    }

    void FixedUpdate()
    {
        ground_movement();

        if (Input.GetKey(KeyCode.UpArrow) && A.GetCurrentAnimatorStateInfo(0).IsName("Idle01"))
        {
            A.SetBool("walk_bool", true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            A.SetBool("walk_bool", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            A.SetTrigger("jump");
        }

        if (A.GetCurrentAnimatorStateInfo(0).IsName("Jump") && ctrler.isGrounded ){
            A.SetTrigger("grounded");
        }
    }

    public void ground_movement()
    {
        // Get the directions vertical as going back and forth

        float x = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Horizontal");

        // if grounded --> Move 
        // Need to do : Reorient center 
        if (ctrler.isGrounded)
        {

            
            this.transform.Rotate(0, y * 3.5f, 0);
            move = transform.forward * x;
            if (Input.GetButtonDown("Jump"))
            {
                move.y = 5f;
            }
            move = move * speed;

        }

        // Add to the vector the direction of gravity 
        move = move + transform.up * gravity;
        
        // fix to mitigate too much z movement
        ctrler.Move(new Vector3(move.x,move.y,move.z*0.8f) * Time.deltaTime);
        
    }

}
