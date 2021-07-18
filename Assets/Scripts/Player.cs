using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    private CharacterController _controller;
    private UIManager _uiManager;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private float _jumpHeight = 25f;
    [SerializeField] private float _yVelocity;  //cache y value of velocity
    [SerializeField] Vector3 velocity;
    private bool _jumpAgain = false; // _canDoubleJump
    [SerializeField] int _coinCount;
   

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager missing");
        }

    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        if(_controller.isGrounded == true)
        {
           if(Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _jumpAgain = true;
            }
        }   
        else
        {
           if (Input.GetKeyDown(KeyCode.Space))
            {
                if(_jumpAgain == true)
                {
                    _yVelocity += _jumpHeight;
                    _jumpAgain = false;
                }
            } 

            _yVelocity -= _gravity/3;  //solution for v.2020 /3
        }

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    public void AddCoins()
    {
        _coinCount++;
        _uiManager.UpdateCoinDisplay(_coinCount);

    }
}
