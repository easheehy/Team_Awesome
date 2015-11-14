using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

    //Uses the levelManager as a reference
    public LevelManager levelManager;

    // Use this for initialization
    void Start() {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Player") {
            levelManager.currentCheckpoint = transform;
        }
    }
}
