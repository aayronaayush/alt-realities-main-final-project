using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioWokeUp : MonoBehaviour
{
    AudioSource audioSource; 
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayDelayed(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
