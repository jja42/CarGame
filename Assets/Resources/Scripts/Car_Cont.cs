using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Car_Cont : MonoBehaviour
{
    bool isMoving = false;
    public bool collided;
    
    private Vector3 startPos;
    Vector2 input;
    private Vector3 endPos;
    float t;
    
    public float HorizontalSpeed;
    public float VerticalSpeed;
    
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
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "SCORE: " + score;
        if (!isMoving && !collided)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x + HorizontalSpeed * Time.deltaTime * input.x,-9.7f,9.7f), transform.position.y, 0);
                return;
            }
            if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
            {
                input.x = 0;
                endPos = new Vector3(transform.position.x, transform.position.y + (2f * System.Math.Sign(input.y)), startPos.z);
                if(endPos.y > 3.6 || endPos.y < -.5f)
                {
                    print("Ree");
                    input.y = 0;
                    return;
                }
                StartCoroutine(Move(transform));
                input.y = 0;
            }
        }
    }
    public IEnumerator Move(Transform entity)
    {
        isMoving = true;
        startPos = entity.position;
        t = 0;
        while (t < 1f)
        {
            if (collided)
            {
                break;
            }
            t += Time.deltaTime * VerticalSpeed;
            entity.position = Vector3.Lerp(startPos, endPos, t);
            yield return 0;
        }
        isMoving = false;
        yield return 0;
    }

    private void OnMove(InputValue value)
    {
        if (!isMoving && !collided)
        {
            Vector2 inputvector = value.Get<Vector2>();
            input = new Vector2(Mathf.RoundToInt(inputvector.x), Mathf.RoundToInt(inputvector.y));
        }
    }   

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
            //TODO: Add Clamps
            VerticalSpeed += .25f;
            VerticalSpeed = Mathf.Clamp(VerticalSpeed, 0, 7);
            HorizontalSpeed += .5f;
            HorizontalSpeed = Mathf.Clamp(HorizontalSpeed, 0, 10);
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
            smoke.gameObject.transform.position = new Vector3(this.transform.position.x + .8f, this.transform.position.y + 1f, this.transform.position.z);
        }
    }
}
