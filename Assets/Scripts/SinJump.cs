using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinJump : MonoBehaviour {

	//Editor Members
	[SerializeField] private float _lerpTime = 1f;
	[SerializeField] private float _jumpHeight = 1;

	private float _elapsedTime = 0f;	
	private bool _isJumping = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space) && !_isJumping) {		
			_isJumping = true;
		}

		if(_isJumping) {
			Jump();
		}

	}


	void Jump() {
		Vector3 nextPosition = transform.position;
		
		_elapsedTime += Time.deltaTime;
		if(_elapsedTime > _lerpTime) {
			_elapsedTime = _lerpTime;
		}
		
		float percentage = _elapsedTime / _lerpTime;
			
		nextPosition.y = _jumpHeight * Mathf.Sin(Mathf.PI * percentage);
		transform.position = nextPosition;

		if(percentage == 1) {
			_isJumping = false;	
			_elapsedTime = 0;		
		}
	}
}
