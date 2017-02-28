using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed = 5;
    public float rotationSpeed = 8;
    public Transform cameraHolder;
    public Animator pistolAnim;
    public Transform laserPivot;
    public Transform balloonParticle;

    private CharacterController _characterController;
    private Vector3 _moveDir;
    private Transform _transfrom;
    private float _gravity = 20;
    private Transform _mainCamera;
    private AudioSource _weaponFireAudio;

    private void Awake() {
        _transfrom = this.transform;
        _mainCamera = Camera.main.transform;
        _characterController = this.GetComponent<CharacterController>();
        _weaponFireAudio = this.GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        _moveDir = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (GvrController.ClickButtonDown) {
            pistolAnim.SetBool("isFire", true);
            FireWeapon();
            _weaponFireAudio.Play();
        } else {
            pistolAnim.SetBool("isFire", false);
        }

        MeasureDistance();
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

    private void FireWeapon() {
        Ray _ray = new Ray(laserPivot.position, laserPivot.forward);
        RaycastHit _rayHit = new RaycastHit();

        if (Physics.Raycast(_ray, out _rayHit)) {
            if(_rayHit.collider.tag == "Balloon") {
                Destroy(_rayHit.collider.gameObject);
                GameManager.isBalloonSet = false;
                Instantiate(balloonParticle, new Vector3(_rayHit.collider.transform.position.x, _rayHit.collider.transform.position.y + 0.25f, _rayHit.collider.transform.position.z), Quaternion.Euler(-90, 0, 0));
            }

            if (_rayHit.collider.GetComponent<Rigidbody>() != null) {
                Rigidbody _rig = _rayHit.collider.GetComponent<Rigidbody>();
                _rig.AddForce(-_rayHit.point * 2, ForceMode.Impulse);
                _rig.AddTorque(_rayHit.point * 2, ForceMode.Impulse);
            }
        }
    }

    private void MeasureDistance() {
        Ray _ray = new Ray(laserPivot.position, laserPivot.forward);
        RaycastHit _rayHit = new RaycastHit();

        if (Physics.Raycast(_ray, out _rayHit)) {
            Debug.Log(_rayHit.distance);

            if(_rayHit.distance <= 20 && _rayHit.distance > 2.5f) {
                GvrLaserPointer.maxReticleDistance = Mathf.Lerp(GvrLaserPointer.maxReticleDistance, _rayHit.distance, 0.25f);
            }else if (_rayHit.distance > 20) {
                GvrLaserPointer.maxReticleDistance = Mathf.Lerp(GvrLaserPointer.maxReticleDistance, 20, 0.25f);
            }
        }
    }
}
