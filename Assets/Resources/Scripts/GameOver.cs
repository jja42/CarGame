using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Car_Cont car;
    public GameOver gameOver;
    public Smoke smoke;
    public AudioSource audios;
    public AudioClip bgm;
    public Camera_Cont camera_Cont;
    public GameObject pattern1;
    public GameObject pattern2;
    public GameObject pattern3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Reset()
    {
        SceneManager.LoadScene(0);
    }

    private void OnRestart()
    {
        Reset();
    }
}
