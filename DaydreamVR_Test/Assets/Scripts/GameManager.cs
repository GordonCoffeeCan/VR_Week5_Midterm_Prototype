using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Transform balloon;

    public static bool isBalloonSet = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isBalloonSet == false) {
            Instantiate(balloon, new Vector3(Random.Range(-13.5f, 13.5f), Random.Range(3, 5), Random.Range(-13.5f, 13.5f)), Quaternion.identity);
            isBalloonSet = true;
        }
	}
}
