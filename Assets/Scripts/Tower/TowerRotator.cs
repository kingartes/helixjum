using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TowerRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _keyboardDelta;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleTouch(touch);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            float torque = _keyboardDelta * Time.deltaTime * _rotateSpeed;
            Rotate(torque);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            float torque = _keyboardDelta * Time.deltaTime * _rotateSpeed;
            Rotate(-torque);
        }

    }

    private void HandleTouch(Touch touch)
    {
        if (touch.phase == TouchPhase.Moved)
        {
            float torque = touch.deltaPosition.x * Time.deltaTime * _rotateSpeed;
            Rotate(torque);
        }
    }

    private void Rotate(float torque)
    {
        _rigidbody.AddTorque(Vector3.up * torque);
    }
}
