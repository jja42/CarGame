using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car_Cont : MonoBehaviour
{
    bool isMoving = false;
    public bool collided;
    private Vector3 startPos;
    Vector2 input;
    private Vector3 endPos;
    float t;
    private float xspeed = .16f;
    public float moveSpeed = 3f;
    int ypos = 0;
    int xpos = 0;
    public GameOver gameOver;
    public Text score_text;
    public Smoke smoke;
    private new AudioSource audio;
    public AudioClip crash;
    public int score = 0;
    public int coins = 0;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "SCORE: " + score;
        if (!isMoving && !collided)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (input != Vector2.zero)
            {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                {
                    input.y = 0;
                    if (input.x > 0 && transform.position.x < 9)
                    {
                        xpos++;
                        transform.Translate(xspeed, 0, 0);
                    }
                    if (input.x < 0 && transform.position.x > -9)
                    {
                        xpos--;
                        transform.Translate(-xspeed, 0, 0);
                    }
                }
                else
                {
                    input.x = 0;
                    if (input.y > 0 && 1 > ypos)
                    {
                        ypos++;
                        StartCoroutine(Move(transform));
                    }
                    if (input.y < 0 && -1 < ypos)
                    {
                        ypos--;
                        StartCoroutine(Move(transform));
                    }
                }
            }
        }
    }
    public IEnumerator Move(Transform entity)
    {
        isMoving = true;
        startPos = entity.position;
        t = 0;
        endPos = new Vector3(startPos.x + (2f * System.Math.Sign(input.x)), startPos.y + (2f * System.Math.Sign(input.y)), startPos.z);
        while (t < 1f)
        {
            if (collided)
            {
                break;
            }
            t += Time.deltaTime * moveSpeed;
            entity.position = Vector3.Lerp(startPos, endPos, t);
            yield return 0;
        }
        isMoving = false;
        yield return 0;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
            moveSpeed += .5f;
            xspeed += .01f;
            score += 100;
            coins++;
        }
        if (col.gameObject.tag == "Car")
       {
            collided = true;
            audio.Stop();
            audio.PlayOneShot(crash);
            gameOver.gameObject.SetActive(true);
            smoke.gameObject.SetActive(true);
            smoke.gameObject.transform.position = new Vector3(this.transform.position.x+.8f,this.transform.position.y+1f,this.transform.position.z);
        }
    }
}
