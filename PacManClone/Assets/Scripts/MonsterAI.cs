using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MonsterAI : MonoBehaviour {	//настраиваемое
	public Transform target;    // Цель
	public int moveSpeed;       // Скорость перемещения
	public int rotationSpeed;   // Скорость поворота
	public float maxDistance;      // Максимальное приближение к игроку
	private float curDistance; // Текущая дистанция
	public int ReactionDistance; // Дистанция на которой монстр реагирует
	private float PointDistance; // Дистанция до поинта
	public Transform iinky; // Объект поинта
	private Transform Point; // Трансформ поинта для возвращения
	public bool HaveTarget;//есть или нету цели
	private Transform myTransform;  // Временная переменная для хранения ссылки на свойство transform

	public Vector3  Direction;	//перемещение по ввектору
	public Vector3 LastPointTarget;//точка где последний раз видели игрока


	public TextAsset matrixCome;
	public float patrolInc=0.25f;
	public float patrolRangePos=0.0f;
	public float patrolRangeNeg=0.0f;
	public Vector3 patrolAmount;
	public AgentSystem ghost;
	

	
	public enum MonsterStat //перечисление всех состояний 
	{
		idle,
		walkPlayer,
		walkPoint,
		notTarget
	}
	public GameObject[] gos;
	public MonsterStat _monsterStat;
	
	public void Init(AgentSystem ghost){
		this.ghost = ghost;
	}
	
	public void Awake(){
		//ссылка на transform чтоб сократить время обращения его в теле скрипта
		myTransform = transform;
		
	}
	
	// Use this for initialization
	public void Start () {
		//ищем по тегу player
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		GameObject iiinky = GameObject.FindGameObjectWithTag("Innky");
		//поставить на него прицел
		target = go.transform;
		Point = iinky.transform;

		
		
		
		if(maxDistance == null) maxDistance = 3;
	}
	
	// Update is called once per frame
	public void Update () {
		
		curDistance = Vector3.Distance(target.position, myTransform.position);
		PointDistance = Vector3.Distance(Point.position, myTransform.position);
		
		//если позволяет дистанция двигаемся к цели(проверка на минимальную дистанцию)
		if((curDistance >= maxDistance) && (curDistance <= ReactionDistance)){
			_monsterStat = MonsterStat.walkPlayer;
			HaveTarget=true;
		}
		else if((curDistance > ReactionDistance) && (PointDistance > 1))
		{
			_monsterStat = MonsterStat.walkPoint;
			HaveTarget=false;
		}
		else 
		{
			_monsterStat = MonsterStat.idle;
		}
		
		switch(_monsterStat){
		case MonsterStat.idle:
			//чертим линию
			Debug.DrawLine(target.position,myTransform.position,Color.yellow);

			break;
			
		case MonsterStat.walkPlayer:
			//чертим линию
			Debug.DrawLine(target.position,myTransform.position,Color.red);
			
			// Разворачиваемся в сторону игрока
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
			                                        Quaternion.LookRotation(target.position - myTransform.position),
			                                        rotationSpeed*Time.deltaTime);
			//двигаемся к цели
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
			

			break;
			
		case MonsterStat.walkPoint:
			//чертим линию
			Debug.DrawLine(Point.position,myTransform.position,Color.green);
			Debug.DrawLine(target.position,myTransform.position,Color.yellow);
			
			// Разворачиваемся в сторону inky
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation,Quaternion.LookRotation(Point.position - myTransform.position),rotationSpeed*Time.deltaTime);
			//двигаемся к цели
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
			

		break;
		//не работает идет странно о как то
		case MonsterStat.notTarget:
			patrolAmount.x = patrolInc * ((float)moveSpeed * 2f) * Time.deltaTime;
			
			if (patrolInc > 0.0f && transform.position.x >= patrolRangePos)
			{
				patrolInc = -1.0f;
			}
			else if (patrolInc < 0.0f && transform.position.x <= patrolRangeNeg)
			{
				patrolInc = 1.0f;
			}
			transform.Translate(patrolAmount);
		break;
		
		
		}
	}
}