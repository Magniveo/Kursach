using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public Transform target;
	public NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		agent=(NavMeshAgent)this.GetComponent("NavMeshAgent");

	
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination (target.position);
	}
}
