using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    //Controls what goes on in the player, that isn't player control related

    //Where the player has its checkpoint
    private Camera cam;
    public Transform camLimit;
    private BoxCollider2D camBoxCollider;

    private PlayerScript playerScript;
    private Rigidbody2D playerRigidBody;
    private Transform player;
    private float gravitySave;

    public Transform currentCheckpoint;

    public GameObject deathParticles;
    
	// Use this for initialization
	void Start () {
        playerScript = FindObjectOfType<PlayerScript>();
        player = playerScript.transform;
        playerRigidBody = playerScript.GetComponent<Rigidbody2D>();
        gravitySave = playerRigidBody.gravityScale;

        cam = FindObjectOfType<Camera>();
        camBoxCollider = camLimit.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        moveCamera();
        
	}

    public void killPlayer() {
        if (!playerScript.died) {
            StartCoroutine(respawnPlayer());
        }
    }

    public IEnumerator respawnPlayer() {
        Instantiate(deathParticles, player.position, player.rotation);
        setPlayerMovementEnable(false);
        playerRigidBody.gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        yield return new WaitForSeconds(deathParticles.GetComponent<ParticleSystem>().duration);

        player.position = currentCheckpoint.position;
        playerRigidBody.gravityScale = gravitySave;
        setPlayerMovementEnable(true);
    }

    public void setPlayerMovementEnable(bool value) {
        playerScript.died = !value;
        playerScript.enabled = value;
        player.GetComponent<Renderer>().enabled = value;
    }

    private void moveCamera() {
        float camSizeY = cam.orthographicSize * 2;
        float camSizeX = camSizeY * cam.aspect;
        Vector2 pos = camLimit.position;
        Vector2 size = camBoxCollider.size;
        size.Scale(camLimit.localScale);

        float minCamLimitX = pos.x - size.x / 2 + camSizeX / 2;
        float maxCamLimitX = pos.x + size.x / 2 - camSizeX / 2;
        float minCamLimitY = pos.y - size.y / 2 + camSizeY / 2;
        float maxCamLimitY = pos.y + size.y / 2 - camSizeY / 2;

        cam.transform.position = new Vector3(Mathf.Clamp(playerScript.transform.position.x, minCamLimitX, maxCamLimitX),
            Mathf.Clamp(playerScript.transform.position.y, minCamLimitY, maxCamLimitY), -10);
    }
}
