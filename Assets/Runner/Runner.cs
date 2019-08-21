using UnityEngine;

public class Runner : MonoBehaviour {

    public static float distanceTraveled;

    public float acceleration;
    public Rigidbody rb;
    public Vector3 boostVelocity, jumpVelocity;
    public float gameOverY;
    public float maxVelocity;

    private bool touchingPlatform;
    private Vector3 startPosition;
    private static int boosts;

    private void Start()
    {
        //GameEventsManager.GameStart += GameStart;
        //GameEventsManager.GameOver += GameOver;
        //startPosition = transform.localPosition;
        //Renderer rend = GetComponent<Renderer>();
        //if (rend != null)
        //{
        //    rend.enabled = false;
        //}
        //Rigidbody ribo = GetComponent<Rigidbody>();
        //if (ribo != null)
        //{
        //    ribo.isKinematic = true;
        //}
        //enabled = false;
    } 

    void Update () {

        // Jump button is being pressed
        if (Input.GetButtonDown("Jump"))
        {
            if (touchingPlatform)
            {
                rb.AddForce(jumpVelocity, ForceMode.VelocityChange);
                touchingPlatform = false;
            }
            else if (boosts > 0)
            {
                rb.AddForce(boostVelocity, ForceMode.VelocityChange);
                boosts -= 1;
                //GUIManager.SetBoosts(boosts);
            }
        }

        // Right button is being pressed
        else if (Input.GetButton("Right") && rb.velocity.x < maxVelocity)
        {
            if (rb.velocity.x < 0)
            {
                Vector3 increaseSpeed = new Vector3(3f, 0f, 0f);
                rb.velocity = rb.velocity + increaseSpeed;
            }
            else
            {
                Vector3 increaseSpeed = new Vector3(1f, 0f, 0f);
                rb.velocity = rb.velocity + increaseSpeed;
            }
        }

        // Left button is being pressed
        else if (Input.GetButton("Left") && rb.velocity.x > -maxVelocity)
        {
            if (rb.velocity.x > 0)
            {
                Vector3 increaseSpeed = new Vector3(3f, 0f, 0f);
                rb.velocity = rb.velocity - increaseSpeed;
            }
            else
            {
                Vector3 increaseSpeed = new Vector3(1f, 0f, 0f);
                rb.velocity = rb.velocity - increaseSpeed;
            }
        }

        // No input is being pressed
        else
        {
            if (rb.velocity.x > 0)
            {
                if (rb.velocity.x < 1)
                {
                    rb.velocity = Vector3.zero;
                }
                else if (touchingPlatform && touchingPlatform)
                {
                    Vector3 decreaseSpeed = new Vector3(1f, 0f, 0f);
                    rb.velocity = rb.velocity - decreaseSpeed;
                }
                else
                {
                    Vector3 decreaseSpeed = new Vector3(.5f, 0f, 0f);
                    rb.velocity = rb.velocity - decreaseSpeed;
                }
            }
            else if (rb.velocity.x < 0)
            {
                if (rb.velocity.x > -1 && touchingPlatform)
                {
                    rb.velocity = Vector3.zero;
                }
                else if (touchingPlatform)
                {
                    Vector3 decreaseSpeed = new Vector3(1f, 0f, 0f);
                    rb.velocity = rb.velocity + decreaseSpeed;
                }
                else
                {
                    Vector3 decreaseSpeed = new Vector3(0.5f, 0f, 0f);
                    rb.velocity = rb.velocity + decreaseSpeed;
                }
            }
            else if (touchingPlatform)
            {
                rb.velocity = Vector3.zero;
            }
        }
        distanceTraveled = transform.localPosition.x;
        //GUIManager.SetDistance(distanceTraveled);

        if (transform.localPosition.y < gameOverY)
        {
            GameEventsManager.TriggerGameOver();
        }
	}

    private void GameStart ()
    {
        boosts = 0;
        //GUIManager.SetBoosts(boosts);
        distanceTraveled = 0f;
        //GUIManager.SetDistance(distanceTraveled);
        transform.localPosition = startPosition;
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.enabled = true;
        }
        Rigidbody ribo = GetComponent<Rigidbody>();
        if (ribo != null)
        {
            ribo.isKinematic = false;
        }
        enabled = true;
    }

    private void GameOver ()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.enabled = false;
        }
        Rigidbody ribo = GetComponent<Rigidbody>();
        if (ribo != null)
        {
            ribo.isKinematic = true;
        }
        enabled = false;
    }

    //void FixedUpdate()
    //{
    //    if (touchingPlatform)
    //    {
    //        rb.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
    //        
    //    }
    //}

    private void OnCollisionEnter()
    {
        touchingPlatform = true;
    }

    void OnCollisionExit()
    {
        touchingPlatform = false;   
    }

    public static void AddBoost ()
    {
        boosts += 1;
        //GUIManager.SetBoosts(boosts);
    }
}
