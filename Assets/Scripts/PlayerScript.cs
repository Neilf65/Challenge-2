using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;
    public TextMeshProUGUI livesText;
    public GameObject lossTextObject;
    
    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    Animator anim;

    private Rigidbody2D rd2d;
    public float speed;
    private int score;
    private int lives; 
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        musicSource.clip = musicClipOne;
        musicSource.Play();


        score = 0;
        
        SetScoreText();
        winTextObject.SetActive(false);

        lives = 3;

        SetLivesText();
        lossTextObject.SetActive(false);

    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score == 8)
        {
            winTextObject.SetActive(true);
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
        else if (score == 4)
        {
            transform.position = new Vector2(46.29f, 1.45f);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        
        //Checks to see if player is facing the direction of the movement
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            lossTextObject.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            score += 1;
            Destroy(collision.collider.gameObject);

            SetScoreText();
        }
        else if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            transform.position = new Vector2(0.0f, 0.098f);

            if(score >= 4)
            {
                transform.position = new Vector2(46.29f, 1.45f);
            }

            SetLivesText();
        }

        if(collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);
            }
        }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    //Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        
        if(Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        
        if(Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if(lives == 0)
        {
            Destroy(gameObject);
        }
        //Checks to see if player is facing the direction of the movement
        
        if (Input.GetKey("escape"))
        {
        Application.Quit();
        }
    }
}