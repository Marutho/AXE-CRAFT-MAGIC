using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffController : MonoBehaviour
{
    bool alreadyMagic = false;
    public Transform MagicOrigin;

    public GameObject magicBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(Camera.main.transform.rotation.eulerAngles.x, 0f, 20f));

        if (Input.GetMouseButtonDown(0))
            {
                MagicThrow();
            }
        if (!alreadyMagic)
        {

        }
    }

    public void MagicThrow()
    {
        SoundManager.instance.magic_Sound(GetComponent<AudioSource>());
      //  alreadyMagic = true;
        Instantiate(magicBullet, MagicOrigin.position, MagicOrigin.rotation);
    }
}
