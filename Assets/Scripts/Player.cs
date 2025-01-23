using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {

    Rigidbody2D playerRb;
    Transform PlayerTransform;
    Manager manager;

    bool IsAirborn;
    bool ValuesAreCloseEnough;
    bool IsDead;
    bool EnvironmentHasCaughtUp;
    bool CalledOnce;

    public Transform Environment;
    public Transform Jumper;

    public float Seconds;
    public float speed = 0.01f;
    public float JumpForce = 500;
    public float Increment = 1.1f;
    public float SmoothTime = 5f;

    public BoxCollider2D WhiteArea;
    public BoxCollider2D BlackArea;
    
    Vector3 TargetPosition;

    //public ParticleSystem DeathParticle;

    void Start()
    {
        manager = FindObjectOfType<Manager>();
        playerRb = GetComponent<Rigidbody2D>();
        PlayerTransform = GetComponent<Transform>();
        PressWhiteArea();
    }


    void Update()
    {
        //if (Jumper == null)        {            Debug.Log("No jumper");        }
        if (IsDead || !manager.GameStarted) return;
        if (manager.GameStarted && CalledOnce == false)
        {
            //Environment.position = new Vector3(PlayerTransform.position.x + 2, 0, 0);
            //t = (Environment.position.x - PlayerTransform.position.x)/-PlayerTransform.position.x *Time.deltaTime *1.1f;
            //Environment.position = Vector3.Lerp(Environment.position,new Vector3(PlayerTransform.position.x - 2, 0, 0),t) ;
            //Environment.position = Vector3.MoveTowards(Environment.position, PlayerTransform.position, speed * Time.deltaTime);

            TargetPosition = new Vector3(PlayerTransform.position.x + 1, 0, 0);
            //Environment.position = Vector3.SmoothDamp(Environment.position, TargetPosition, ref velocity, SmoothTime);

            if (Environment.position.x <= TargetPosition.x)
            {
                Environment.Translate(Vector2.right * speed * 0.5f);
            }           
            if (Environment.position.x >= TargetPosition.x)
            {
                EnvironmentHasCaughtUp = true;
                CalledOnce = true;
            }
            //if (Environment.position.x != PlayerTransform.position.x-2)            {                Environment.Translate(Vector2.right * 2 * speed);            }
            
        }
        if (EnvironmentHasCaughtUp)
        {
            PlayerTransform.Translate(Vector2.right * speed);
            Environment.position = new Vector3(PlayerTransform.position.x + 1, 0, 0);
        }
        if (playerRb.velocity.y != 0)
        {
            IsAirborn = true;
        }
        if (playerRb.velocity.y == 0)
        {
            IsAirborn = false;
        }       
        if(Mathf.Abs(0.2592f - Mathf.Abs(PlayerTransform.position.y))< 0.01f)
        {
            ValuesAreCloseEnough = true;
        }
        speed += 0.00002f;
    }
    
    public void PressWhiteArea()
    {
        if (!IsAirborn )
        {
            //if is in white area
            if(PlayerTransform.position.y > 0)
            {
                JumpForce = 500;
                if (ValuesAreCloseEnough)
                {
                    StartCoroutine(Jump());
                    //playerRb.AddForceAtPosition(Jumper.up * JumpForce, Jumper.position);
                }
            }
            //if isn't in white area
            if (PlayerTransform.position.y < 0)
            {
                BlackArea.isTrigger = false;
                WhiteArea.isTrigger = true;
                Physics2D.gravity = new Vector2(0, -20f);
                if (Mathf.Abs(0.26f + PlayerTransform.position.y) > 0.51f)
                {
                    PlayerTransform.position = new Vector3(PlayerTransform.position.x, -PlayerTransform.position.y, -1);
                }
                //DeathParticle.startColor = Color.black;
            }           
        }
        if (IsDead)
        {
            ReStartGame();
        }
    }

    public void PressBlackArea()
    {
        if (!IsAirborn)
        {
            //if is in black area
            if (PlayerTransform.position.y < 0)
            {
                JumpForce = -500;
                if (ValuesAreCloseEnough)            
                {
                    StartCoroutine(Jump());
                    //playerRb.AddForceAtPosition(Jumper.up * JumpForce, Jumper.position);
                }
            }
            //if isn't in black area
            if (PlayerTransform.position.y > 0)
            {
                WhiteArea.isTrigger = false;
                BlackArea.isTrigger = true;
                Physics2D.gravity = new Vector2(0, 20f);
                if (Mathf.Abs(0.26f + PlayerTransform.position.y) > 0.51f)
                {
                    PlayerTransform.position = new Vector3(PlayerTransform.position.x, -PlayerTransform.position.y, -1);
                }
                //DeathParticle.startColor = Color.white;
            }
        }
        if (IsDead)
        {
            ReStartGame();
        }
    }

    IEnumerator Jump()
    {
        playerRb.AddForceAtPosition(Jumper.up * JumpForce, Jumper.position);
        yield return new WaitForSeconds(Seconds);
        playerRb.AddForceAtPosition(-Jumper.up * JumpForce * Increment, Jumper.position);
    }

    public void Die()
    {
        IsDead = true;
        manager.GameStarted = false;
        Time.timeScale = 0;
        //DeathParticle.gameObject.SetActive(true);
        //DeathParticle.Play();
    }

    void ReStartGame()
    {
        SceneManager.LoadScene(0);
        IsDead = false;
        Time.timeScale = 1;
        BlackArea.isTrigger = false;
        WhiteArea.isTrigger = true;
        Physics2D.gravity = new Vector2(0, -20f);
    }

}