using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class control:MonoBehaviour{
	public Transform target;
	public int RotationSpeed;
	public int MoveSpeed;
	public float CurrentDistance;
	public float MaxDistance;
	public int ReactionDistance;
	public bool HaveTarget;

	public GameObject[] gos;

	private Transform myTransform;

	public enum MonsterStat{
		Idle,
		WalkPlayer,
		WalkPoint
	}

	private MonsterStat _MonsterStat;
	void Awake(){
		myTransform = transform;
	}

	void Start(){
		GameObject go = GameObject.FindGameObjectWithTag ("Player");
		target = go.transform;
		if (MaxDistance == null)
			MaxDistance = 10;
	}

	void Update(){
		CurrentDistance = Vector3.Distance (target.position, myTransform.position);

		if(CurrentDistance>=MaxDistance) && (CurrentDistance<=ReactionDistance){
			_MonsterStat=MonsterStat.WalkPlayer;
		}
		else{ 
			if(CurrentDistance>ReactionDistance){
				_MonsterStat=MonsterStat.WalkPoint;
			}
		}
		else{
			_MonsterStat=MonsterStat.Idle;
		}
		switch(_MonsterStat)
			case MonsterStat.Idle:
				Debug.Drawline(target.position,myTransform.position,Color.yellow);
			break;
			
			case MonsterStat.WalkPlayer:
				Debug.Drawline(target.position,myTransform.position,Color.red)

				myTransform.rotation=Quaternion.Slerp(myTransform.rotation,
			                     Quaternion.LookRotation(myTransform.position-target.transform.position),
			                     RotationSpeed*Time.deltaTime);
				myTransform.position+=myTransform.forward*MoveSpeed*Time.deltaTime;
			break;
			
			case MonsterStat.WalkPoint:
					Debug.Drawline(gos.transform.position,myTransform.position,Color.green);
					Debug.Drawline(target.position,myTransform.position,Color.green);
					myTransform.rotation = Quaternion.Slerp(myTransform.rotation,Quaternion.LookRotation(Point.position - myTransform.position),rotationSpeed*Time.deltaTime);
		//двигаемся к цели
					myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
					
			break;

	}
}