using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponController : MonoBehaviour
{
    private Rigidbody weaponRb;
    private WeaponScript weaponScript;
    private float returnTime;
    public bool isObjectActive;

    //Sounds
    public AudioClip throwSound;

    //vector3 private
    private Vector3 origLocPos;
    private Vector3 origLocRot;
    private Vector3 pullPosition;

    //booleans
    public bool hasWeapon = true;
    public bool pulling = false;
    
    //transforms
    public Transform weapon;
    public Transform hand;
    public Transform curvePoint;

    //parameters
    public float throwPower = 240;
    //[Space]

    // Start is called before the first frame update
    void Start()
    {
        weaponRb = weapon.GetComponent<Rigidbody>();
        weaponScript = weapon.GetComponent<WeaponScript>();
        origLocPos = weapon.localPosition;
        origLocRot = weapon.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(isObjectActive)
        {
            if (hasWeapon)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    WeaponThrow();
                }

            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    WeaponStartPull();
                }
            }

            if (pulling)
            {
                if (returnTime < 1)
                {
                    weapon.position = GetQuadraticCurvePoint(returnTime, pullPosition, curvePoint.position, hand.position);
                    returnTime += Time.deltaTime * 1.5f;
                }
                else
                {
                    WeaponCatch();
                }
            }
        }


    }

    public void WeaponThrow()
    {
        GetComponent<Movement>().canSwitchHand = false;
        SoundManager.instance.lanzarSound(GetComponent<AudioSource>());
        hasWeapon = false;
        weaponScript.activated = true;
        weaponRb.isKinematic = false;
        weaponRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        weapon.parent = null;
        weapon.eulerAngles = new Vector3(0, -180 + transform.eulerAngles.y, 0);
        weapon.transform.position += transform.right / 5;
        weaponRb.AddForce(Camera.main.transform.forward * throwPower + transform.up * 2, ForceMode.Impulse);

    }

    public void WeaponStartPull()
    {
        pullPosition = weapon.position;
        weaponRb.Sleep();
        weaponRb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        weaponRb.isKinematic = true;
        weapon.DORotate(new Vector3(-90, -90, 0), .2f).SetEase(Ease.InOutSine);
        weapon.DOBlendableLocalRotateBy(Vector3.right * 90, .5f);
        weaponScript.activated = true;
        pulling = true;
    }

    public void WeaponCatch()
    {
        GetComponent<Movement>().canSwitchHand = true;
        returnTime = 0;
        pulling = false;
        weapon.parent = hand;
        weaponScript.activated = false;
        weapon.localEulerAngles = origLocRot;
        weapon.localPosition = origLocPos;
        hasWeapon = true;


    }

    public Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }
}
