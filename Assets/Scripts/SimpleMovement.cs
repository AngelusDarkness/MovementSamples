using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour {

	[SerializeField] private float _speedX = 10f;
	[SerializeField] private float _speedZ = 10f;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPosition = transform.position;

		//Vamos hacia la derecha
		if(Input.GetAxis("Horizontal") > 0) {
			newPosition.x += Time.deltaTime * _speedX;
		} 

		//Vamos hacia la izquierda
		if(Input.GetAxis("Horizontal") < 0) {
			newPosition.x -= Time.deltaTime * _speedX;
		}

		//Vamos hacia la derecha
		if(Input.GetAxis("Vertical") > 0) {
			newPosition.z += Time.deltaTime * _speedZ;
		} 

		//Vamos hacia la izquierda
		if(Input.GetAxis("Vertical") < 0) {
			newPosition.z -= Time.deltaTime * _speedZ;
		}
		transform.position = newPosition;
	}
}
