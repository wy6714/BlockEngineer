using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private float horizontal;
    private float speed = 5f;

    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    private Animator anim;

    public ParticleSystem dust;
    public GameObject level1Collider;

    //public AudioSource winAudio;
    public static event Action<GameObject> collectFruit;
    public static event Action<GameObject> GetFlag;
    public static event Action<GameObject> playerDie;
    public static event Action<GameObject> showLevel2;
    public static event Action<GameObject> EnterLevel2;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        //run animation
        if (horizontal > 0f || horizontal < 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        Flip();
    }

    private void Flip()//change side
    {
        if(isFacingRight && horizontal <0f || !isFacingRight && horizontal > 0f)
        {
            createDust();
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            collectFruit?.Invoke(other.gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("player collide with enemy");
            Animator whaleAnim = other.gameObject.GetComponent<Animator>();
            whaleAnim.SetTrigger("WhaleEat");
            playerDie?.Invoke(gameObject);
            Destroy(gameObject,0.2f);
        }
        if (other.CompareTag("Flag"))
        {
            GetFlag?.Invoke(other.gameObject);
        }
        if (other.CompareTag("champion"))
        {
            Debug.Log("collide champion");
            showLevel2?.Invoke(gameObject);
            level1Collider.SetActive(false);
        }
        if (other.CompareTag("levelChecker"))
        {
            Debug.Log("collide level checker");
            EnterLevel2?.Invoke(gameObject);
        }
        if (other.CompareTag("saw"))
        {
            playerDie?.Invoke(gameObject);
            anim.SetTrigger("playerDie");
            Invoke("reStart", 0.5f);
        }

        //if fall down, restart
        if (other.CompareTag("Fall"))
        {
            Debug.Log("fall down");
            SceneManager.LoadScene("level1");
        }
        //if (other.CompareTag("champion"))
        //{
        //    winAudio.Play();
        //}
        
        //if (other.CompareTag("Spikes"))
        //{
        //    Debug.Log("player collide with spikes");
        //}
    }

    private void createDust()
    {
        dust.Play();
    }

    public void reStart()
    {
        SceneManager.LoadScene("level1");
    }
}
