using UnityEngine;

public class LerpMovement : MonoBehaviour {
	[SerializeField] private float _lerpTime = 1f;
	[SerializeField] private float _step = 1f;

	private float _elapsedTime = 0f;
	private bool _isMoving = false;
	private bool _canMove = true;
	private Vector3 _nextPosition;
	
	// Update is called once per frame
	void Update () {		
		if(_canMove) {
			Constants.Directions direction = Constants.Directions.kNone;
			//Checking User Input Behaviour
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
		
		//After Step is calculated then we move to next position.
		if(_isMoving) {
			Move();
		}
	}

	void CalculateStep(Constants.Directions direction) {
		_nextPosition = transform.position;
		//Calculamos el proximo vector de posicion segun la direccion tomada.
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

		//Lerp Movement needs a percentage of movement from point A to point B.
		_elapsedTime += Time.deltaTime;
		if(_elapsedTime > _lerpTime) {
			_elapsedTime = _lerpTime;
		}
		
		float percentage = _elapsedTime / _lerpTime;
		transform.position = Vector3.Lerp(transform.position, _nextPosition, percentage);
		
		_canMove = false;
		
		if(percentage == 1) {
			_canMove = true;
			_isMoving = false;
			_elapsedTime = 0f;
		}
	}
	
}
