using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private AudioClip[] backgroundClips;
    private int clipsPlayed;
    private void Awake()
    {
        clipsPlayed = 0;
    }

    void Update()
    {
        NextBackgroundAudioTrack();
    }

    internal void NextBackgroundAudioTrack()
    {
        if (!gameManager.GetComponent<AudioSource>().isPlaying)
        {
            gameManager.GetComponent<AudioSource>().clip = backgroundClips[clipsPlayed % backgroundClips.Length];
            gameManager.GetComponent<AudioSource>().Play();
        }
    }
}
