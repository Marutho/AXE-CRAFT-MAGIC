using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Magic : MonoBehaviour
    {     
        public float lifeTime = 3f;
        public float speed = 100f;
        protected float timer = 0f;

        public bool ChickenTransform = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            move();
        }

        protected void move()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            if (timer > lifeTime) Destroy(this.gameObject);
        }

    private void OnCollisionEnter(Collision collision)
    {
      
       // Destroy(collision.gameObject);

    }

}


