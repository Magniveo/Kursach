using UnityEngine;
using System.Collections;

public class randomcontrol : MonoBehaviour {

	public Transform myTransform;
	public float howLong;
	public float  howFast;
	public Vector3 direction;
	public float nextUpdate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextUpdate) {
			nextUpdate=Time.time+(Random.value*howLong);
			direction=Random.onUnitSphere;
			direction.y=0;
			direction.Normalize();
			direction*=howFast;
			direction.y=1.5f-transform.position.y;

		}

		myTransform.position += myTransform.forward * howFast * Time.deltaTime;
	}
}
