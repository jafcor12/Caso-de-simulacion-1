using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField]
    [Tooltip("Z Axis")]
    float forwardSpeed = 30.0F;

    [SerializeField]
    [Tooltip("x Axis")]
    float strafeSpeed = 8.0F;

    [SerializeField]
    [Tooltip("Y Axis")]
    float hoverSpeed = 3.5F;

    [SerializeField]
    [Tooltip("Velocidad mínima")]
    float minimumForwardSpeed = 50.0f;

    [Header("Acceleration")]

    [SerializeField]
    float forwardAcceleration = 2.5F;

    [SerializeField]
    float strafeAcceleration = 2.0F;

    [SerializeField]
    float hoverAcceleration = 2.0F;

    [Header("Roll")]
    float rollSpeed = 85.0F;

    [SerializeField]
    float rollAcceleration = 3.5F;

    Rigidbody _rb;

    public bool gameOver;

    float _activeForwardSpeed;
    float _activeStrafeSpeed;
    float _activeHoverSpeed;

    float _lookRateSpeed = 75.0F;

    float _rollInput;

    Vector2 _lookInput;
    Vector2 _screenCenter;
    Vector2 _mouseDistance;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _screenCenter = new Vector2(Screen.width * 0.5F, Screen.height * 0.5F);
    }

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        transform.Rotate(_mouseDistance.y * _lookRateSpeed * Time.fixedDeltaTime,
            _mouseDistance.x * _lookRateSpeed * Time.fixedDeltaTime,
              _rollInput * rollSpeed * Time.fixedDeltaTime, 
                Space.Self);

        _rb.position += transform.forward * _activeForwardSpeed * Time.fixedDeltaTime;
        _rb.position += transform.right * _activeStrafeSpeed * Time.fixedDeltaTime;
        _rb.position += transform.up * _activeHoverSpeed * Time.fixedDeltaTime;
    }

    void HandleInputs()
    {
        _lookInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _mouseDistance = new Vector2((_lookInput.x - _screenCenter.x) / _screenCenter.x,
            (_lookInput.y - _screenCenter.y) / _screenCenter.y);

        _mouseDistance = Vector2.ClampMagnitude(_mouseDistance, 1.0F);

        _rollInput = Mathf.Lerp(_rollInput, Input.GetAxis("Roll"), rollAcceleration * Time.deltaTime);

        float currentFowardSpeed = Input.GetAxisRaw("Forward") * forwardSpeed;

        if (currentFowardSpeed == 0)
        {
            _activeForwardSpeed = minimumForwardSpeed;
        }
        else
        {
            _activeForwardSpeed = Mathf.Lerp(_activeForwardSpeed, currentFowardSpeed, forwardAcceleration * Time.deltaTime);
        }

        float currentStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        _activeStrafeSpeed = Mathf.Lerp(_activeStrafeSpeed, currentStrafeSpeed, strafeAcceleration * Time.deltaTime);

        float currentHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;
        _activeHoverSpeed = Mathf.Lerp(_activeHoverSpeed, currentHoverSpeed, hoverAcceleration * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
            Debug.Log("Game Over");
            gameOver = true;
            SceneManager.LoadScene("GameOverScreen");
        } 

    }

}
