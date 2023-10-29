using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public CircleCollider2D col;
    public bool readyToGo = false;
    public Rigidbody2D rb;
    public GameObject Player;
    public float speedDiv;
    public SpriteRenderer sr;
    public GameObject deadspr;
    public int power = 0;
    public bool destroy = false;
    void Start()
    {
        StartCoroutine("spawntime");
    }

    IEnumerator spawntime()
    {
        yield return new WaitForSeconds(1);
        readyToGo = true;
        col.enabled = true;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Vector2 aimDirection = new Vector2(Player.transform.position.x - rb.position.x,Player.transform.position.y-rb.position.y);
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
        if (readyToGo == true)
        {
            rb.velocity = (Player.transform.position-this.transform.position)/speedDiv;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "destructable")
        {
            if (destroy == true)
            {
                Destroy(collision.gameObject);
            }
        }
    }

}
