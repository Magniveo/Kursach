using UnityEngine;
using System.Collections;

public class controlpnky : MonoBehaviour {
//переменные target цель призраков
	public GameObject target;
	//скорость вращения и перемещения
	public int MoveSpeed;
	public int RotationSpeed;
	//радиус атаки призрака
	public float AttackRadius = 50;
	public float MaxAttackRadius= 100;
	//расстояние на которое подходит призрак
	public float AttackDistance=2;
	public float BackRadius=10;
	//цель монстра
	public bool AttackPlayer= true;

	public float DistanseTarget;

	//проверяет есть ли у призрака цель(target)
	public bool HaveTarget;

	//ссылка на себя
	public Transform myTransform;

	//массив ссылок на цели
	public GameObject[] gos;
	// Use this for initialization

	void Awake(){
		//передает переменной свой класс transform
		myTransform = transform;

	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!HaveTarget) {
			if(AttackPlayer)FindTarget("Player");

		
		}else{
			//проверяем не убежала ли цель на расстояние больше чем максималльной 
			var diff =(target.transform.position-myTransform.position);
			var dist=diff.sqrMagnitude;
			//если убежала то сбрасываем 
			if(dist>MaxAttackRadius){
				target=null;
				DistanseTarget=0;
				HaveTarget=false;
			}
//			Debug.DrawLine(myTransform.position,target.transform.position,Color.red);
			//поворачиваем призрака
			myTransform.rotation=Quaternion.Slerp(myTransform.rotation,
			Quaternion.LookRotation(myTransform.position-target.transform.position),
			RotationSpeed*Time.deltaTime);	                                                              
		//проверка расстояния до цели	
			if(dist>AttackDistance){
				myTransform.position=myTransform.forward*MoveSpeed*Time.deltaTime;
			}

		}
	}
	void FindTarget(string FindingTag){
		gos = GameObject.FindGameObjectsWithTag(FindingTag);
		foreach (var go in gos) {
			if(HaveTarget){
				//если в процессе уже найдена цель
				if(go.tag !=tag){
					//проверяем теги
					var diff=(go.transform.position-myTransform.position);
					var dist=diff.sqrMagnitude;
					//проверяем наименьшее состояние до цели
					if(DistanseTarget>dist){
						target=go;
						DistanseTarget=dist;

					}
				Debug.DrawLine(myTransform.position,go.transform.position,Color.yellow);
				}
			}else{
				//check tag
				if(go.tag!=tag){
					var diff=(go.transform.position-myTransform.position);
					var	dist=diff.sqrMagnitude;
					if(dist<AttackRadius){
						target=go;
						DistanseTarget=dist;
						HaveTarget=true;
					}
				}
				Debug.DrawLine(myTransform.position,go.transform.position,Color.yellow);
			}
		}
	}
}

