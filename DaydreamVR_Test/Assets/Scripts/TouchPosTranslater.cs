using UnityEngine;
using System.Collections;

//This script is using for translating GvrController.TouchPos from value of "0 to 1" to "-1 to 1";

public class TouchPosTranslater : MonoBehaviour {
    public static Vector2 touchPos;

    private const float _middleX = 0.5f;
    private const float _middleY = 0.5f;

    private Vector2 _originalTouchPoint;
    private Vector2 _deltaTouchPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GvrController.TouchDown) {
            _originalTouchPoint = GvrController.TouchPos;
        }else if (GvrController.TouchUp) {
            _originalTouchPoint = Vector2.zero;
        }

        if (GvrController.IsTouching) {
            _deltaTouchPoint = GvrController.TouchPos - _originalTouchPoint;
            if (_deltaTouchPoint.magnitude > 0.35f) {
                touchPos = new Vector2((GvrController.TouchPos.x - _originalTouchPoint.x) / _middleX, (GvrController.TouchPos.y - _originalTouchPoint.y) / _middleY * -1);
            }
        } else if (!GvrController.IsTouching) {
            touchPos = Vector2.zero;
            _deltaTouchPoint = Vector2.zero;
        }
        //Debug.Log();
	}
}
