using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera;
    private Vector2 mousePosition;
    public Rigidbody2D rb;
    public float fireForce;
    public float minForce;
    public GameObject egg;
    public Transform firePoint;
    public GameObject enemyChicken;
    public int specialChicken = 0;

    void Start()
    {
        sceneCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        ProcessInputs();
    }
    void Aim()
    {
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
    void ProcessInputs()
    {

        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(egg, firePoint.transform.position, firePoint.transform.rotation);
            if ((mousePosition - rb.position).magnitude < 1)
            {
                projectile.GetComponent<Rigidbody2D>().AddForce((transform.up) * fireForce, ForceMode2D.Impulse);

            }
            else
            {
                projectile.GetComponent<Rigidbody2D>().AddForce((mousePosition - rb.position) * fireForce, ForceMode2D.Impulse);
            }
            Instantiate(enemyChicken, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            sceneCamera.GetComponent<CameraController>().playerDie();
        }
    }

}
