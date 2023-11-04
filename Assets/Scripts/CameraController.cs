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
    private string bestDistKey = "BestDistKey";
    private string bestKillKey = "BestKillKey";
    private string bestTimeKey = "BestTimeKey";
    [SerializeField]
    public TextMeshProUGUI distanceTraveled;
    public TextMeshProUGUI timespent;
    public TextMeshProUGUI kills;
    public TextMeshProUGUI bestDistanceTraveled;
    public TextMeshProUGUI bestTimespent;
    public TextMeshProUGUI bestKills;
    private int killcount = 0;
    private float tim;
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

        if (killcount > PlayerPrefs.GetInt(bestKillKey, 0))
        {
            PlayerPrefs.SetInt(bestKillKey, killcount);
        }
        if (Time.timeSinceLevelLoad > PlayerPrefs.GetInt(bestTimeKey, 0))
        {
            PlayerPrefs.SetInt(bestTimeKey, Mathf.FloorToInt(Time.timeSinceLevelLoad));
        }
        if (Mathf.FloorToInt(this.transform.position.y - 3f) > PlayerPrefs.GetInt(bestDistKey, 0))
        {
            PlayerPrefs.SetInt(bestDistKey, Mathf.FloorToInt(this.transform.position.y - 3f));
        }

        bestKills.text = PlayerPrefs.GetInt(bestKillKey, 0).ToString();
        bestDistanceTraveled.text = PlayerPrefs.GetInt(bestDistKey, 0).ToString() + "m";
        bestTimespent.text = Mathf.Floor(PlayerPrefs.GetInt(bestTimeKey, 0) / 60) + ":" + Mathf.Floor(PlayerPrefs.GetInt(bestTimeKey, 0) % 60);
        PlayerPrefs.Save();

        distanceTraveled.text = Mathf.FloorToInt(this.transform.position.y - 3f)+ "m";
        timespent.text = (Mathf.Floor(tim / 60) + ":" + Mathf.Floor(tim % 60));
        kills.text = killcount.ToString();
    }
    public void playerDie()
    {
        tim = Time.timeSinceLevelLoad;
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
