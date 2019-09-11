using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsMovement : MonoBehaviour {

	[SerializeField] private float _speed = 1f;
	[SerializeField] private float _step = 1f;
	[SerializeField] private float _jumpHeight = 3;
	[SerializeField] private float _distanceLength = 3;

	private bool _isMoving = false;
	private bool _canMove = true;
	private bool _isJumping = false;
	
	private Vector3 _nextPosition;
	private Vector3 _jumpAcceleration = Vector3.zero;
	private Vector3 _jumpVelocity = Vector3.zero;
	private float _groundY = 0f;
	
	
	// Update is called once per frame
	void Update () {
		if(_canMove) {
			Constants.Directions direction = Constants.Directions.kNone;
		
			if(Input.GetAxis("Vertical") > 0) {
				direction = Constants.Directions.kUp;
			} else if (Input.GetAxis("Vertical") < 0) {
				direction = Constants.Directions.kDown;
			} else if (Input.GetAxis("Horizontal") < 0) {
				direction = Constants.Directions.kLeft;
			} else if(Input.GetAxis("Horizontal") > 0) {
				direction = Constants.Directions.kRight;
			}

			CalculateStep(direction);
		}
		
		if(_isMoving) {
			Move();
		}

		if(Input.GetKeyUp(KeyCode.Space) && !_isJumping) {		
			CalculateJump();
		}

		if(_isJumping) {
			Jump();
		}
	}

	void CalculateStep(Constants.Directions direction) {
		_nextPosition = transform.position;
		//Calculate next vector position with step according to direction input.
		switch(direction) {
			case Constants.Directions.kUp:
				_nextPosition = transform.position + Vector3.forward * _step;
			break;
			case Constants.Directions.kDown:
				_nextPosition = transform.position + Vector3.back * _step;
			break;
			case Constants.Directions.kLeft:
				_nextPosition = transform.position + Vector3.left * _step;
			break;
			case Constants.Directions.kRight:
				_nextPosition = transform.position + Vector3.right * _step;
			break;
		}

		_isMoving = (direction != Constants.Directions.kNone);
	}

	void Move() {
		//Just moving towards next vector position at specified speed.		
		transform.position = Vector3.MoveTowards(transform.position,
												 _nextPosition,
												 Time.deltaTime * _speed);
		
		if(Vector3.Distance(transform.position, _nextPosition) < Mathf.Epsilon) {
			_isMoving = false;
			_canMove = true;
		}
	}

	void CalculateJump() {
		Vector3 jumpDistance = Vector3.up * _distanceLength;		
		_jumpVelocity = 2 * _jumpHeight * jumpDistance / _distanceLength;		
		_jumpAcceleration =  -2 * _jumpHeight * Vector3.Scale(jumpDistance, jumpDistance) / Mathf.Pow(_distanceLength,2);		
		_isJumping = true;
	}

	void Jump() {
		
		transform.position = Vector3.MoveTowards(transform.position, _nextPosition, Time.deltaTime * _speed);
		transform.position += _jumpVelocity * Time.deltaTime + _jumpAcceleration/2 * Mathf.Pow(Time.deltaTime , 2);
		_jumpVelocity += _jumpAcceleration * Time.deltaTime;

		float distanceLeft = _jumpVelocity.y - _jumpAcceleration.y ;
		
		if(distanceLeft < Mathf.Epsilon) {		
			_isJumping = false;
			//Reset Y position to ground level.
			Vector3 pos = transform.position;
			pos.y = _groundY;
			transform.position = pos;
		}
		
	}
}
