using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _rb;
    Vector2 _direction = Vector2.zero;
    float _speed = 5f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direction.normalized * _speed * Time.fixedDeltaTime);
    }
}
