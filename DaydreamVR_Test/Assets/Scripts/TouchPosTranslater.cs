using UnityEngine;
using System.Collections;

//This script is using for translating GvrController.TouchPos from value of "0 to 1" to "-1 to 1";

public class TouchPosTranslater : MonoBehaviour {
    public static Vector2 touchPos;

    private const float middleX = 0.5f;
    private const float middleY = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GvrController.IsTouching) {
            touchPos = new Vector2((GvrController.TouchPos.x - middleX) / middleX, (GvrController.TouchPos.y - middleY) / middleY * -1);
        }else if (!GvrController.IsTouching) {
            touchPos = Vector2.zero;
        }
        Debug.Log(touchPos);
	}
}
