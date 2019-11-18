using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jump;
    public AudioClip jumpSound;
    public Transform player;
    public GameObject respown;
    

    public Transform hand;
    public GameObject objectInHand;
    public List<GameObject> handItems;
    public List<string> availableItems;
    public int currentItem = 0;
    public bool canCraftItem;

    private bool isClicking;
    private Joint joint;
    private Rigidbody rb;
    private Vector3 moveDir, jumpDir, origLocPos, origLocRot;
    private CapsuleCollider col;
    private float running;

    public bool canSwitchHand;
    private bool canJump,canBounce, onRope;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        origLocPos = player.localPosition;
        origLocRot = player.localEulerAngles;

        for (int i = 0; i < hand.childCount; i++)
        {
            handItems.Add(hand.GetChild(i).gameObject);
            handItems[i].SetActive(false);

        }

        availableItems.Add("h_Empty");
        availableItems.Add("h_Sword");
        availableItems.Add("h_Staff");

        objectInHand = handItems[5];

        isClicking = false;
        canSwitchHand = true;
        canJump = false;
        canBounce = true;
    }

    void Update()
    {
        float hmov = 0;
        float vmov = 0;
        running = 1;

        if (CanMove(transform.right * Input.GetAxisRaw("Horizontal")))
        {
            hmov = Input.GetAxisRaw("Horizontal");
        }

        if (CanMove(transform.forward * Input.GetAxisRaw("Vertical")))
        {
            vmov = Input.GetAxisRaw("Vertical");
        }

        moveDir = (hmov * transform.right + vmov * transform.forward).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            running = 1.5f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            canJump = true;
            jumpDir = moveDir;
        }
        if(Input.GetKeyDown(KeyCode.Space) && onRope)
        {
            onRope = false;
            joint.connectedBody = null;
            Destroy(joint);
            //Destroy(FixedJoint);

            /*
            
            GetComponent<Rigidbody>().isKinematic = false;
            player.parent = null;
            player.localEulerAngles = origLocRot;
            player.localPosition = origLocPos;
            */
        }

        if(transform.position.y<=-20)
        {
            transform.position = respown.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isClicking = true;
        }
        else
        {
            isClicking = false;
        }
            

        

        WillBounce();

        UpdateHand();

        CheckCraft();

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

        if (onRope)
        {
            Move();
        }
    }

    private void UpdateHand()
    {    
        if(canSwitchHand)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && availableItems.Contains("h_Sword") && objectInHand != handItems[0])
            {
                currentItem = 1;
                DeactivateItems();
                handItems[currentItem-1].SetActive(!handItems[currentItem - 1].activeSelf);
                objectInHand = handItems[currentItem - 1];
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && availableItems.Contains("h_Staff"))
            {
                currentItem = 2;
                DeactivateItems();
                handItems[currentItem - 1].SetActive(!handItems[currentItem - 1].activeSelf);
                objectInHand = handItems[currentItem - 1];
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && availableItems.Contains("i_Wood"))
            {
                currentItem = 3;
                DeactivateItems();
                handItems[currentItem - 1].SetActive(!handItems[currentItem - 1].activeSelf);
                objectInHand = handItems[currentItem - 1];
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && availableItems.Contains("i_Stone"))
            {
                currentItem = 4;
                DeactivateItems();
                handItems[currentItem - 1].SetActive(!handItems[currentItem - 1].activeSelf);
                objectInHand = handItems[currentItem - 1];
            }

            if (Input.GetKeyDown(KeyCode.Alpha5) && availableItems.Contains("i_Plastic"))
            {
                currentItem = 5;
                DeactivateItems();
                handItems[currentItem - 1].SetActive(!handItems[currentItem - 1].activeSelf);
                objectInHand = handItems[currentItem - 1];
            }

            if (Input.GetKeyDown(KeyCode.Alpha6) && availableItems.Contains("i_Iron"))
            {
                currentItem = 6;
                DeactivateItems();
                handItems[currentItem - 1].SetActive(!handItems[currentItem - 1].activeSelf);
                objectInHand = handItems[currentItem - 1];
            }
        }

    }

    private void CheckCraft()
    {
        if ((objectInHand.tag == "i_Wood" || objectInHand.tag == "i_Stone" || objectInHand.tag == "i_Iron" || objectInHand.tag == "i_Plastic") && isClicking)
            canCraftItem = true;
        

        else
            canCraftItem = false;
    }

    private void DeactivateItems()
    {
        for (int i = 0; i < hand.childCount; i++)
        {            
            handItems[i].SetActive(false);
        }

        GetComponent<WeaponController>().isObjectActive = (currentItem == 1 ? true : false);
    }


    private void Move()
    {
        Vector3 yVel = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = moveDir * speed * Time.deltaTime *running;
        rb.velocity += yVel;
    }

    private bool CheckMovement(Vector3 dir,float rad,float dist,string obj)
    {
        float disToPoints = col.height / 2 - col.radius;

        Vector3 point1 = transform.position + col.center + Vector3.up * disToPoints;
        Vector3 point2 = transform.position + col.center - Vector3.up * disToPoints;

        float radius = col.radius * rad;
        float castDist = dist;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, dir, castDist);

        foreach (RaycastHit objectHit in hits)
        {
            if (objectHit.transform.tag == "Wall" && obj=="Wall")
            {
                return false;
            }
            if (objectHit.transform.tag == "Floor" && obj == "Floor")
            {
                return true;
            }
            if(objectHit.transform.tag == "BridgeWood" && obj == "BridgeWood")
            {
                return true;
            }
            if (objectHit.transform.tag == "Trampolin" && obj == "Trampolin" && canBounce)
            {
                rb.AddForce(new Vector3(-dir.x, dir.y + 0.3f, dir.z)*3, ForceMode.VelocityChange);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Rope" && onRope==false)
        {
            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = collision.rigidbody;
            onRope = true;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17)
        {
            SoundManager.instance.pickResource(GetComponent<AudioSource>());
        }
    }
    private void Jump()
    {
        rb.velocity += new Vector3(0, jump * Time.deltaTime, 0);
    }

    private bool IsGrounded()
    {
        return CheckMovement(new Vector3(0, -1, 0), 1, 0.01f, "Floor");
    }

    private bool CanMove(Vector3 v)
    {
        return CheckMovement(v, 0.95f, 0.5f, "Wall");
    }

    private bool WillBounce()
    {
        return CheckMovement(rb.velocity, 0.95f, 0.5f, "Trampolin");
    }

}
