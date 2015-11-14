using UnityEngine;
using System.Collections;

public class charMove : MonoBehaviour
{

    public float moveSpeed;
    public float jumpHeight;
    private Rigidbody2D rb;
    private int jumpCounter = 0;
    public int maxJumps;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCounter < maxJumps)
            {
                rb.velocity = new Vector2(0, jumpHeight);
                jumpCounter++;
                    
                if(jumpCounter == maxJumps)
                {
                    jumpCounter = 0; 
                }

            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }
}
