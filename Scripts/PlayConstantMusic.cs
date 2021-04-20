using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayConstantMusic : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();

        if (audioSource.isPlaying)
        {
            audioSource.playOnAwake = false;
        }
        else
        {
            audioSource.Play();
        }

        AudioSource[] objs = GameObject.FindObjectsOfType<AudioSource>();
        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(audioSource);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
