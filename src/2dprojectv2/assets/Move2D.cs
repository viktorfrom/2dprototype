using UnityEngine;
using System.Collections;

public class Move2D : MonoBehaviour
{
    GameObject ball;
    GameObject wall1;
    GameObject wall2;
    Rigidbody2D ballPhysics;
    public int thrust = 100;
    public int jumpForce = 1500;
    bool isGrounded;
    bool buttonPressed;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector3 respawnPoint;


    void Start()
    {
        ball = GameObject.Find("ball");
        wall1 = GameObject.Find("wall1");
        wall2 = GameObject.Find("wall2");
        buttonPressed = false;
        ballPhysics = ball.GetComponent<Rigidbody2D>();
        FixedUpdate();
    }

    void FixedUpdate()
    {

        var right = Input.GetKey(KeyCode.D);
        var left = Input.GetKey(KeyCode.A);
        var space = Input.GetKey(KeyCode.Space);
        var mouse1 = Input.GetButtonDown("Fire1");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (mouse1 && buttonPressed)
        {
            Destroy(wall1);
            Destroy(wall2);
        }

        if (right)
        {
            if (ball != null && !isGrounded)
            {
                ballPhysics.AddForce(Vector3.right * thrust / 4 * Time.deltaTime);
            }
            else if (ball != null)
            {
                ballPhysics.AddForce(Vector3.right * thrust * Time.deltaTime);
            }
        }

        if (left)
        {
            if (ball != null && !isGrounded)
            {
                ballPhysics.AddForce(Vector3.left * thrust / 4 * Time.deltaTime);
            }
            else if (ball != null)
            {
                ballPhysics.AddForce(Vector3.left * thrust * Time.deltaTime);
            }
        }

        if (space)
        {
            if (ball != null && isGrounded && ballPhysics.velocity.y < 0.001f)
            {
                ballPhysics.AddForce(Vector3.up * jumpForce * Time.deltaTime);
            }
        }
    }

    void Update()
    {
        var escape = Input.GetKeyDown(KeyCode.Escape);

        if (escape)
        {
            Application.Quit();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }

        if (other.tag == "Spinner")
        {
            transform.position = respawnPoint;
        }

        if (other.tag == "Flag")
        {
            Application.Quit();
        }

        if (other.tag == "Button")
        {
            buttonPressed = true;
        }
    }
}