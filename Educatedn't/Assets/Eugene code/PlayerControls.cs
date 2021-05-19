using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody rb;
    

    public float speed;
    public float jumpForce;
    public float JumpDist;
    private float WhatsJumpForce;

    public bool GotJump;
   
    
    
    // Start is called before the first frame update
    void Start()
    {
     rb = GetComponent<Rigidbody>();
        WhatsJumpForce = jumpForce;
        
    }


    bool CheckGrounded()
    {
        Vector3 origin = transform.position;
        Vector3 direction = new Vector3(0, -1, 0);
        float maxDistance = 1.1f;
        RaycastHit hitInfo;

        Debug.DrawRay(origin, direction * maxDistance, Color.magenta);

        if (Physics.Raycast(origin, direction, out hitInfo, maxDistance))
        {

            return true;
        }
        return false;
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveUp = Input.GetAxis("Jump");

        if (!GotJump)
        {
            jumpForce = 0;
        } else
        {
            jumpForce = WhatsJumpForce; 
        }

        Vector3 movement = new Vector3(moveHorizontal, moveUp * jumpForce, moveVertical);

        if (movement.magnitude >= 0.1f)
        {
            float TurnAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, TurnAngle, 0f);
            if (CheckGrounded())
            {
                rb.AddForce(movement * speed );
            }
           
        }

        if (!CheckGrounded())
        {

            Vector3 myPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 myDirection;
            myDirection = this.gameObject.transform.TransformDirection(Vector3.forward);
            //rb.AddForceAtPosition(myDirection * JumpDist, myPos);
            rb.AddForceAtPosition(myDirection * JumpDist, myPos);
            Debug.Log("Works");
        }



        
    }
   
}
