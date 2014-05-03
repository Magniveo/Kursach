using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AgentSystem : MonoBehaviour {
	public MonsterAI controls;//для компонентов
	public bool Good;//для проверки
	public GameObject[] ghostObjects;//объекты или агенты
	public Transform[] GhostTransform;//нерабочаяя
	// Use this for initialization
	void Start () {
		controls=GetComponent<MonsterAI>();
		controls.Init (this);
		controls.Awake ();
		controls.Start ();
	
	}
	
	// Update is called once per frame
	void Update() {
	
				if (controls != null) {
						Good = true;
						ghostObjects = GameObject.FindGameObjectsWithTag ("Ghost");
						GhostTransform=ghostObjects.transform;
	
								if(controls.HaveTarget = true) {
										Good = true;

										controls._monsterStat=MonsterAI.MonsterStat.walkPlayer;
				Debug.DrawLine(ghostObjects.position,ghostObjects.position,Color.blue);
								} else {
										if (controls.HaveTarget = false) {
												Good = false;
					controls._monsterStat=MonsterAI.MonsterStat.walkPoint;		
										}
								}
						}
				}

}
