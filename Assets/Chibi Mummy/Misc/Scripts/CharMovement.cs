using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour 
{

	public float jumpSpeed = 600.0f;
	public bool grounded = false;
	public bool doubleJump = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	private Animator anim;
	public Rigidbody rb;
	float vSpeed = 10;
    bool isMoving = false;
    Vector3 moveDirection;

	void Awake()
	{
		anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
		anim.SetBool("isIdle", true);
	}
	void Start ()
	{
		
	}
	void FixedUpdate () 
	{
        if (CheckKeyDown(KeyCode.W))
            moveDirection = Vector3.forward;

        if (CheckKeyDown(KeyCode.S))
            moveDirection = Vector3.back;

        if (CheckKeyDown(KeyCode.A))
            moveDirection = Vector3.left;

        if (CheckKeyDown(KeyCode.D))
            moveDirection = Vector3.right;

        if (CheckKeyUp())
            return;

        if (isMoving) {
            Move(moveDirection);
        }
	}

	void Update () 
	{
		if (Input.GetKeyDown("space") && anim.GetBool("isIdle"))
		{
			Jump();
		}
	}

    bool CheckKeyDown(KeyCode code) {
        if (Input.GetKeyDown(code)) {
            anim.SetBool("isRun", true);
            isMoving = true;
            return true;
        }
        return false;
    }

    bool CheckKeyUp() {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
            anim.SetBool("isRun", false);
            isMoving = false;
            return true;
        }
        return false;
    }

    void Move(Vector3 direction) {
        transform.forward = direction;
        Vector3 movement = direction * vSpeed * Time.deltaTime;
        Vector3 nextPos = rb.position + movement;
        Vector3 targetPos = Vector3.Lerp(rb.position, nextPos, Time.deltaTime * vSpeed);
        rb.position = new Vector3(targetPos.x, -2f, targetPos.z);
    }

	public void Jump ()
	{
		if (grounded && rb.velocity.y == 0)
		{
			anim.SetTrigger("isJump");
            rb.AddForce(0,jumpSpeed,0, ForceMode.Impulse);
		}
	}

}
