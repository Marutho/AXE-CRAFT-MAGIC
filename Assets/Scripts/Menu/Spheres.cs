using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spheres : MonoBehaviour
{
    public float scale, animationTime;
    public int num;
    public Camera mCamera;
    public GameObject text;

    private Vector3 iscale;
    private float time, t;
    private bool activation;
    private Renderer render;
    private Color mColor;
    private Animation mAnimation;
    
    void Start()
    {
        mAnimation = mCamera.GetComponent<Animation>();
        time = Time.deltaTime;
        t = 0f;
        iscale = transform.localScale;
        activation = false;
        render = gameObject.GetComponent<Renderer>();
        mColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
        Time.timeScale = 1;
    }
    
    void FixedUpdate()
    {
        if (activation)
        {
            transform.localScale = Animation();
        }
    }

    private Vector3 Animation()
    {
        float dt = Time.deltaTime;
        time += dt;

        if (time > animationTime)
        {
            time = 0.0f;
        }

        if (time < (animationTime / 2))
        {
            t = time / (animationTime / 2);

            return Vector3.Lerp(iscale, scale * iscale, t);

        }
        else
        {
            t = (time - (animationTime / 2)) / (animationTime - (animationTime / 2));

            return Vector3.Lerp(scale * iscale, iscale, t);

        }

    }

    private void OnMouseOver()
    {
        render.material.color = mColor;

        activation = true;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            text.SetActive(false);

            switch (num)
            {
                case 1:
                    mAnimation.Play("Sphere1Anim", PlayMode.StopAll);
                    break;
                case 2:
                    mAnimation.Play("Sphere2Anim", PlayMode.StopAll);
                    break;
                case 3:
                    mAnimation.Play("Sphere3Anim", PlayMode.StopAll);
                    break;
            }
        }

    }
    private void OnMouseExit()
    {
        mColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);

        render.material.color = Color.white;

        activation = false;
    }
}
