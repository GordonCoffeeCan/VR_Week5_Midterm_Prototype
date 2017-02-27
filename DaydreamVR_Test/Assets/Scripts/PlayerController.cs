using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed = 5;
    public float rotationSpeed = 8;
    public Transform cameraHolder;
    public Animator pistolAnim;

    private CharacterController _characterController;
    private Vector3 _moveDir;
    private Transform _transfrom;
    private float _gravity = 20;
    private Transform _mainCamera;

    private void Awake() {
        _transfrom = this.transform;
        _mainCamera = Camera.main.transform;
        _characterController = this.GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start () {
        _moveDir = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        if (GvrController.ClickButtonDown) {
            //Debug.Log("Clicked!");
            pistolAnim.SetBool("isFire", true);
        } else {
            pistolAnim.SetBool("isFire", false);
        }
    }

    private void FixedUpdate() {
        _transfrom.rotation = Quaternion.Euler(0, _mainCamera.eulerAngles.y, 0);

        if (_characterController.isGrounded) {
            _moveDir = new Vector3(TouchPosTranslater.touchPos.x, 0, TouchPosTranslater.touchPos.y);
            _moveDir = _transfrom.TransformDirection(_moveDir);
            _moveDir *= speed;
        }
        _moveDir.y -= _gravity * Time.deltaTime;
        _characterController.Move(_moveDir * Time.deltaTime);
        cameraHolder.transform.position = new Vector3(_transfrom.position.x, _transfrom.position.y, _transfrom.position.z);
    }
}
