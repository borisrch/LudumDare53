using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
  float _emenySpeed = 5;

  [SerializeField] Transform point1;
  [SerializeField] Transform point2;
  Transform _target;
  Rigidbody2D _rb;



  [SerializeField] FieldOfVeiw _fov;
  enum EMENY_STATE
  {
    Wait,
    Patrol,
    NoticesPlayer,

  }

  EMENY_STATE _state;
  // Start is called before the first frame update

  void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  void Start()
  {
    _target = point1;
    _state = EMENY_STATE.Patrol;
  }

  // Update is called once per frame
  void Update()
  {
    switch (_state)
    {
      case EMENY_STATE.Wait:
      case EMENY_STATE.Patrol:
        HandleMove();
        break;
      case EMENY_STATE.NoticesPlayer:
        HandleSpotPlayer();
        break;
    }

    _fov.SetOrigin(transform.position);
    _fov.SetAimDirection()
  }

  void HandleMove()
  {
    transform.position = Vector3.MoveTowards(transform.position, _target.position, _emenySpeed * Time.deltaTime);

    if (Vector3.Distance(transform.position, _target.position) <= 0)
    {
      Debug.Log("SWAP");
      // TODO Set state to wait
      _target = _target == point1 ? point2 : point1;
    }

  }

  void HandleSpotPlayer()
  {

  }
}
