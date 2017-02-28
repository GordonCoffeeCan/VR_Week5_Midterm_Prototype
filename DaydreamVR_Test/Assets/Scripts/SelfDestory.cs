using UnityEngine;
using System.Collections;

public class SelfDestory : MonoBehaviour {
    public float destoryTimer = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        destoryTimer -= Time.deltaTime;
        if(destoryTimer <= 0) {
            Destroy(this.gameObject);
        }
	}
}
