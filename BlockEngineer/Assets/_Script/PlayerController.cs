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

    public static event Action<GameObject> collectFruit;
    public static event Action<GameObject> playerDie;
    public static event Action<GameObject> getKeyHappens;

    private void OnEnable()
    {
        Grid.UndoHappen += undoPlayerPos;
    }

    private void OnDisable()
    {
        Grid.UndoHappen -= undoPlayerPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.localScale = new Vector2(1, 1); // keep player always face right after they respawn
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
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
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
            Destroy(gameObject, 0.2f);
        }

        if (other.CompareTag("saw"))
        {
            anim.SetTrigger("playerDie");
            playerDie?.Invoke(gameObject);
            Destroy(gameObject);
            //Invoke("reStart", 0.5f);
        }

        //if fall down, restart
        if (other.CompareTag("Fall"))
        {
            Debug.Log("fall down");
            anim.SetTrigger("playerDie");
            playerDie?.Invoke(gameObject);
            Destroy(gameObject, 0.2f);
            //SceneManager.LoadScene("level1");
        }

        if (other.CompareTag("key"))
        {
            Debug.Log("hit key");
            //chestKey = true;
            //keyAnim.SetTrigger("getKey");
            getKeyHappens?.Invoke(other.gameObject);

        }

        if (other.CompareTag("bat"))
        {
            Debug.Log("bat killed player");
            anim.SetTrigger("playerDie");
            playerDie?.Invoke(gameObject);
            Destroy(gameObject, 0.2f);
        }
    }

    private void createDust()
    {
        dust.Play();
    }

    public void reStart()
    {
        SceneManager.LoadScene("level1");
    }

    public void undoPlayerPos(Grid.GameState currentSate)
    {
        Debug.Log("current Pos is: " + transform.position);
        Debug.Log("player will move to: : " + currentSate.playerPos);
        transform.position = currentSate.playerPos;
    }

    public void callPlayerDie(GameObject obj)
    {
        playerDie?.Invoke(obj);
        Destroy(obj);
    }
}
