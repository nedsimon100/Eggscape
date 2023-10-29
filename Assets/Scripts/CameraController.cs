using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CameraController : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject player;
    public Vector3 offset;
    public float CamBounds;
    public Vector3 NextTileOffset;
    public float minDisSpawn;
    public float maxDisSpawn;
    public float minspawntime;
    public float maxspwantime;
    public GameObject[] menus = new GameObject[3];

    [SerializeField]
    public TextMeshProUGUI distanceTraveled;
    public TextMeshProUGUI timespent;
    public TextMeshProUGUI kills;
    private int killcount = 0;
    private string tim = "";
    private void Start()
    {
        StartCoroutine("EnemySpawner");
    }
    public void addKill()
    {
        killcount += 1;
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.Euler(Vector3.zero);
        this.transform.position = player.transform.position + offset;
        if (this.transform.position.x > CamBounds)
        {
            transform.position = new Vector3(CamBounds,transform.position.y,transform.position.z);
        }
        else if (this.transform.position.x < -CamBounds)
        {
            transform.position = new Vector3(-CamBounds, transform.position.y, transform.position.z);
        }
        distanceTraveled.text = "" + (Mathf.RoundToInt((this.transform.position.y - 3f)*100))/100 + " m";
        timespent.text = tim;
        kills.text = killcount.ToString();
    }
    public void playerDie()
    {
        tim = Time.timeSinceLevelLoad.ToString();
        Time.timeScale = 0;
        menus[0].SetActive(false);
        menus[1].SetActive(true);
        menus[2].SetActive(false);
    }
    IEnumerator EnemySpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minspawntime,maxspwantime));
            Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector2(Random.Range(-16f,16f),Random.Range(this.transform.position.y+minDisSpawn,this.transform.position.y+maxDisSpawn)), Quaternion.identity);
        }
    }
}
