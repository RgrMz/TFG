using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Some data needed to calculate if a bugged ball should be spawned based on practices & CALMS
    // Starting probability = 0.3 maybe, for each bug decrease it since you learn about it

    // Gather the zone in which the player is to be able to know which ball to spawn
    private string zoneName;

    public TextMeshProUGUI zone;

    private GameObject ballToSpawn;

    void Start()
    {
        zoneName = zone.text;
        switch(zoneName)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
