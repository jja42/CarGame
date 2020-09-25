using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Cont : MonoBehaviour
{
    private new AudioSource audio;
    public Car_Cont player;
    public AudioClip gameover;
    bool soundplayed = false;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.collided && !soundplayed) {
            audio.Stop();
            audio.PlayOneShot(gameover,1);
            soundplayed = true;
        }
        
    }
}
