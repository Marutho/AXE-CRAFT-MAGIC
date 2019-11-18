using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    public float speed;
    public float jump;
    public float fly;


    public bool jumping, flying, chickenActive;
    public Camera chickenCamera;


    //body 
    public GameObject realBody;
    public GameObject staff;

    private CapsuleCollider col;
    private bool canJump, canBounce;
    private Joint joint;
    private Rigidbody rb;
    private Vector3 moveDir, jumpDir, origLocPos, origLocRot;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        canJump = false;
        canBounce = true;

    }

    // Update is called once per frame
    void Update()
    {
      if(chickenActive)
        {
           //GetComponent<Flock>().enabled = false;
            chickenCamera.enabled = true;
            realBody.SetActive(false);
            staff.SetActive(false);

            //CODE OF NEW INPUTS FOR THE CHICKEN
            float hmov = 0;
            float vmov = 0;


            if (CanMove(transform.right * Input.GetAxisRaw("Horizontal")))
            {
                
                hmov = Input.GetAxisRaw("Horizontal");
            }

            if (CanMove(transform.forward * Input.GetAxisRaw("Vertical")))
            {
                vmov = Input.GetAxisRaw("Vertical");
            }

            moveDir = (hmov * transform.right + vmov * transform.forward).normalized;


            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                canJump = true;
                jumpDir = moveDir;
            }
        }
      else if(!chickenActive)
        {
            //GetComponent<Flock>().enabled = true;
            realBody.SetActive(true);
            chickenCamera.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            chickenActive=false;
        }
    }

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 16)
        {
            chickenActive = true;
            SoundManager.instance.collisionVSChickenSound(GetComponent<AudioSource>());
            Destroy(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            Move();
            
            if (canJump)
            {
                //SoundManager.instance.PlaySingle(jumpSound);
                Jump();
                canJump = false;
            }
            canBounce = true;
        }
    }

    private bool CheckMovement(Vector3 dir, float rad, float dist, string obj)
    {
        float disToPoints = col.height / 2 - col.radius;

        Vector3 point1 = transform.position + col.center + Vector3.up * disToPoints;
        Vector3 point2 = transform.position + col.center - Vector3.up * disToPoints;

        float radius = col.radius * rad;
        float castDist = dist;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, dir, castDist);

        foreach (RaycastHit objectHit in hits)
        {
            if (objectHit.transform.tag == "Wall" && obj == "Wall")
            {
                return false;
            }
            if (objectHit.transform.tag == "Floor" && obj == "Floor")
            {
                return true;
            }
            if (objectHit.transform.tag == "Trampolin" && obj == "Trampolin" && canBounce)
            {
                rb.AddForce(new Vector3(-dir.x, dir.y + 0.3f, dir.z) * 3, ForceMode.VelocityChange);
                canBounce = false;
                return false;
            }


        }
        if (obj == "Floor")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Move()
    {
        Vector3 yVel = new Vector3(0, rb.velocity.y, 0);
        //Debug.Log("EEEEEEEEEE TE PILLO BROTER");
        rb.velocity = moveDir * speed * Time.deltaTime;
        rb.velocity += yVel;
    }



    private bool CanMove(Vector3 v)
    {
        return CheckMovement(v, 0.95f, 0.5f, "Wall");
    }

    private void Jump()
    {
        rb.velocity += new Vector3(0, jump * Time.deltaTime, 0);
    }

    private bool IsGrounded()
    {
        return CheckMovement(new Vector3(0, -1, 0), 1f, 0.5f, "Floor");
    }

}
