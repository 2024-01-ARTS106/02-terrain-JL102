using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    float horizontalMovement, forwardMovement;
    public float jumpForce = 50f;
    public float walkSpeed = 1f;
    private float forceMultiple = 100;
    public AnimationState anim;
    bool moving;
    Animator animator;
    GameObject body;
    GameObject camera;

    int floor, breakable;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        body = GameObject.Find("Body");
        camera = GameObject.Find("Main Camera");
        animator = body.GetComponent<Animator>();
        moving = false;
        floor = LayerMask.NameToLayer("Floor");
        breakable = LayerMask.NameToLayer("Breakable");
    }

    void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        forwardMovement = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalMovement * walkSpeed * forceMultiple, 0f, forwardMovement * walkSpeed * forceMultiple);
        rb.AddForce(direction);

        moving = Input.GetButton("Horizontal") || Input.GetButton("Vertical");

        // Only set rotation if one of the buttons are down, i.e. don't reset to zero rotation if both buttons are up
        if (moving)
        {
            body.transform.rotation = Quaternion.LookRotation(direction);
        }
        animator.SetBool("running", moving);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(0f, jumpForce * forceMultiple, 0f));
            animator.SetBool("inAir", true);
            animator.SetTrigger("startJump");
        }
        camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, camera.transform.position.z);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == floor)
        {
            animator.SetBool("inAir", false);
        }
        else if (other.gameObject.layer == breakable)
        {
            // If below the other breakable object, then destroy it
            if (this.transform.position.y < other.gameObject.transform.position.y)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
