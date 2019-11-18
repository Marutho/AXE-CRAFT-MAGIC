using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.

    //MOVEMENT
    public AudioClip mv1;
    public AudioClip mv2;

    //Music
    public AudioClip MainTheme1;

    //Magic
    public AudioClip magic;

    //JOINTS
    public AudioClip joint1;
    public AudioClip joint2;

    public AudioClip lanzar;
    public AudioClip collisionVSwall;
    public AudioClip collisionVSGround;
    public AudioClip collisionVSBox;
    public AudioClip normalCollision;
    public AudioClip jumpSound;

    public AudioClip chickenSound1;
    public AudioClip chickenSound2;

    public AudioClip stick;

    //WOOD
    public AudioClip woodBridgeS;

    //PLAYER PICK RESOURCE
    public AudioClip pickResourceSound;

    //DOOOR
    public AudioClip doorSound;

    //POINTS
    public AudioClip point1;
    public AudioClip point2;

    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
    


    void Start()
    {
        PlayMusic(MainTheme1);
        Debug.Log(musicSource.loop);

    }

    private void Update()
    {
        if(!musicSource.isPlaying)
        {
            musicSource.clip=MainTheme1;
            musicSource.Play();
        }
    }

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }


    /// <summary>
    /// Plays the single clip
    /// </summary>
    /// <param name="clip">Clip.</param>
    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip, AudioSource src)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        src.clip = clip;

        //Play the clip.
        src.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        musicSource.clip = clip;

        //Play the clip.
        musicSource.Play();
        
    }


    /// <summary>
    /// Randomizes the sfx.
    /// </summary>
    /// <param name="clips">Clips.</param>
    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }

    /// <summary>
    /// Plays the robot sound movement (call from RobotManager).
    /// </summary>
    public void PlayPlayerSoundMovement(AudioSource audioSrc)
    {
        int randomSound = Random.Range(0, 2);

        if (randomSound == 0)
        {
            instance.PlaySingle(mv1, audioSrc);
        }
        else
        {
            instance.PlaySingle(mv2, audioSrc);
        }

    }

    public void openDoor(AudioSource audioSrc)
    {
        instance.PlaySingle(doorSound, audioSrc);
    }

    //SOUNDLANZAR
    public void lanzarSound(AudioSource audioSrc)
    {
        instance.PlaySingle(lanzar, audioSrc);
    }

    //Sonido opcion
    public void optionSound(AudioSource audioSrc)
    {
        instance.PlaySingle(lanzar, audioSrc);
    }


    //SOUND COLLISIONVSWALL
    public void collisionVSwallSound(AudioSource audioSrc)
    {
        instance.PlaySingle(collisionVSwall, audioSrc);
    }

    //SOUND COLLISIONVSGROUND
    public void collisionVSGroundSound(AudioSource audioSrc)
    {
        instance.PlaySingle(collisionVSGround, audioSrc);
    }

    //SOUND NORMALCOLLISION
    public void normalCollisionSound(AudioSource audioSrc)
    {
        instance.PlaySingle(normalCollision, audioSrc);
    }

    //SOUND COLLISIONBROKEBOX
    public void collisionVSBoxSound(AudioSource audioSrc)
    {
        instance.PlaySingle(collisionVSBox, audioSrc);
    }

    //SOUND JUMP
    public void jump_Sound(AudioSource audioSrc)
    {
        instance.PlaySingle(jumpSound, audioSrc);
    }

    //SOUND Magic
    public void magic_Sound(AudioSource audioSrc)
    {
        instance.PlaySingle(magic, audioSrc);
    }

    //SOUND Sticky ball
    public void pickUp(AudioSource audioSrc)
    {
        instance.PlaySingle(stick, audioSrc);
    }

    //SOUND collision wood bridge   
    public void collisionVSwoodBridge(AudioSource audioSrc)
    {
        instance.PlaySingle(woodBridgeS, audioSrc);
    }

    //sound chicken 
    public void collisionVSChickenSound(AudioSource audioSrc)
    {
        int random = Random.Range(0, 1);
        if(random==1)
        {
            instance.PlaySingle(chickenSound1, audioSrc);
        }
        else
        {
            instance.PlaySingle(chickenSound2, audioSrc);
        }
    }

    //pick a resource from something   
    public void pickResource(AudioSource audioSrc)
    {
        instance.PlaySingle(pickResourceSound, audioSrc);
    }







}
