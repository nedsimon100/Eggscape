using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI distanceTraveled;
    public GameObject playercam;

    void Update()
    {
        distanceTraveled.text = "" + Mathf.RoundToInt(playercam.transform.position.y - 3f)+" m";
    }
}
