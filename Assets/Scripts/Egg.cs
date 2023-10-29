using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public List<GameObject> chickenTypes = new List<GameObject>();
    public Rigidbody2D rb;
    public float fireForce;
    public float minSpeed;
    bool cracked = false;
    bool crack = false;
    public int powerchicken = 0;
    public bool destroy = false;
    public bool bounce = false;
    public GameObject cam;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bounce == true)
        {
            
        }
        else
        {
            if (destroy == true)
            {
                if (collision.gameObject.tag == "destructable")
                {
                    Destroy(collision.gameObject);
                    rb.velocity *= fireForce;
                }
            }
            if (collision.gameObject.tag == "Enemy")
            {

                powerchicken = collision.gameObject.GetComponent<Enemy>().power;
                Destroy(collision.gameObject);
                cam.GetComponent<CameraController>().addKill();


            }
            if (collision.gameObject.tag != "Player")
            {
                crack = true;
            }
        }
    }
    private void Awake()
    {
        FindObjectOfType<Manager>().Play("pop");
        cam = Camera.main.gameObject;
    }
    private void Update()
    {
        if (rb.velocity.magnitude < minSpeed || crack == true)
        {
            if (cracked == false)
            {
                cracked = true;
                Instantiate(chickenTypes[powerchicken], transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

        }
    }
}
