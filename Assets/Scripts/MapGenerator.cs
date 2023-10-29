using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public List<GameObject> Tiles = new List<GameObject>();
    public GameObject Player;
    public bool templateTile;
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (this.transform.position.y < Player.transform.position.y + 50)
        {
            
            Instantiate(Tiles[Random.Range(0, Tiles.Count)], this.transform.position + new Vector3(1 * FindObjectOfType<CameraController>().NextTileOffset.x, 1 * FindObjectOfType<CameraController>().NextTileOffset.y, 0), Quaternion.identity);
             Destroy(gameObject);
            
            
            
        }
    }
}
