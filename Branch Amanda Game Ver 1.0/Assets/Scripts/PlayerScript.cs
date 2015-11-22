using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    //Player Sprite by default should be facing right

    public LevelManager levelManager;
    // Objects Collections Varibles 
    public int Score = 0;

    //variable for loading level 
    // Error: can only be loaded from main thread
    //private int current_level = Application.loadedLevel;

    //Player movement and jumping stats
    public int moveSpeed;
    public int jumpHeight;
    public int maxJumps;
    private int currentJump;

    //Player attributes
    private Rigidbody2D rb;
    public bool died;
    private Vector2 scale;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        levelManager = FindObjectOfType<LevelManager>();
        died = false;
    }

    void FixedUpdate() {
        //Resets jump counter if they are on the ground
        if ((currentJump > 0) && isGrounded()) currentJump = 0;
    }

    // Update is called once per frame
    void Update() {
        //Make player act according to KeyPresses
        if ((currentJump < maxJumps) && Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            currentJump++;
        }

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);

        //Switches player sprite depending on left or right
        if (rb.velocity.x > 0) {
            changePlayerDirection(scale.x);
        }
        else if (rb.velocity.x < 0) {
            changePlayerDirection(-scale.x);
        }

       // Debug.Log(rb.velocity);

    }

    //Checks to see if player is on the ground and not relatively moving up
    bool isGrounded() {
        GameObject objectTouched = gameObject.GetComponentInChildren<CheckCollided>().objectTouched;
        return (objectTouched != null && 
                !(objectTouched.GetComponent<Collider2D>().isTrigger) &&
                !(rb.velocity.y > 0));
    }

    //Changes the way the player faces
    void changePlayerDirection(float posOrNeg) {
        posOrNeg /= Mathf.Abs(posOrNeg);

        Vector3 currentScale = transform.localScale;
        if ((currentScale.x /= Mathf.Abs(currentScale.x)) != posOrNeg) {
            transform.localScale = new Vector3(-transform.localScale.x, currentScale.y, currentScale.z);
        }
    }

    //When the player collides with something
    void OnCollisionEnter2D(Collision2D other) {
        //Puts player as a child of the parent of the moving platform so the player moves with it
        //It needs to be the parent of the moving platform so the player can still interact with it
        if (other.gameObject.tag.Equals("MovingPlatforms")) {
            this.transform.parent = other.transform.parent;
            return;
        }

        if (other.gameObject.tag.Equals("Collectables")== true)
        {
            Debug.Log("Contact");
            Score += 1;
            other.gameObject.SetActive(false);
            Debug.Log(Score);

            if(Score == 3)
            {
                Debug.Log("Game has Concluded");
            }
         
         }
        if (other.gameObject.tag.Equals("DoorExit"))
        {
            Debug.Log("Attempt to Exit");
            if(Score == 3)
            {
                Application.LoadLevel("Level 1");
                
            }
        }

    }

    void OnCollisionExit2D(Collision2D other) {
        if (this.transform.parent != null) {
            this.transform.parent = null;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.isTrigger && other.tag == "CamLimit") {
            levelManager.killPlayer();
        }
    }
}
