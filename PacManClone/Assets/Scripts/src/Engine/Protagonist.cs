using System;
using UnityEngine;
using System.Collections;


public class Protagonist : Actor 
{
	// ----------------------------------------------
	// CONSTANTS
	// ----------------------------------------------
	public const int STATE_IDLE    	= 0;
	public const int STATE_RUN    	= 1;
	public const int STATE_DIE    	= 2;
	public const int STATE_END    	= 3;
	
	// CONFIGURATIONS
	public const float SPEED_SHOOT		= 20.0f;

	public const int DAMAGE_PLAYER 		= 10;	
	
	// ----------------------------------------------
	// VARIABLE MEMBERS
	// ----------------------------------------------	
	private GameObject m_world;

	// ----------------------------------------------
	// CONSTRUCTOR
	// ----------------------------------------------	
	// -------------------------------------------
	/* 
	 * Constructor
	 */
	public Protagonist() 
	{
		ChangeState(STATE_IDLE);		
	}
	
	// ----------------------------------------------
	// INIT/DESTROY
	// ----------------------------------------------	
	
	// -------------------------------------------
	/* 
	 * Start		
	 */
	void Start () 
	{	
		// GET REFERENCE TO WORLD SCRIPT
		m_world = GameObject.FindGameObjectWithTag(Global.CAMERA_TAG);	
			
		// INIT GAME OBJECT
		Character = this.gameObject.gameObject;		
		Type = Global.TYPE_PROTAGONIST;		
	}
	
	// -------------------------------------------
	/* 
	 * Destroy
	 */
	void Destroy () 
	{
		base.Destroy();
		if (m_world!=null) m_world.BroadcastMessage("PlayerDeath");		
		Debug.Log("Protagonist::Destroy");
	}

	// ----------------------------------------------
	// GETTERS/SETTERS
	// ----------------------------------------------	

	// ----------------------------------------------
	// LISTENERS
	// ----------------------------------------------	
	
	// ----------------------------------------------
	// PRIVATE/PROTECTED FUNCTIONS
	// ----------------------------------------------	
	
	// ----------------------------------------------
	// PUBLIC FUNCTIONS
	// ----------------------------------------------	
	// -------------------------------------------
	/* 
	 * Apply damage		
	 */
	public override void ApplyDamage(float damage) 
	{					
		base.ApplyDamage(damage);
		
		if (Life<=0)
		{
			ChangeState(STATE_DIE);
		}
	}
	
	// ----------------------------------------------
	// UPDATE
	// ----------------------------------------------	
	// -------------------------------------------
	/* 
	 * Update		
	 */
	public override void Update () 
	{
		base.Update();	
		
		switch (m_state)
		{
			//////////////////////////////
			case STATE_IDLE:	
				if (m_iterator==1)
				{
					if (Character.animation!=null) Character.animation.CrossFade("idle");
				}
			
				Global.LookAtMouse(Character.transform);
				Position = Character.transform.position;
			
			    if ((Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)||(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1))
				{
					ChangeState(STATE_RUN);
				}
				if(Input.GetButtonDown("Fire1"))
				{
					if (m_world!=null) m_world.BroadcastMessage("AskNewShoot", new ShootParameters(Character, Position, Global.TYPE_PROTAGONIST, SPEED_SHOOT, DAMAGE_PLAYER));
				}
				break;
			
			//////////////////////////////
			case STATE_RUN:
				if (m_iterator==1)
				{
					if (Character.animation!=null) Character.animation.CrossFade("jump");
				}
			
				Global.MoveAroundKeys(Character.transform, m_speedMovement);
				Position = Character.transform.position;
				Global.LookAtMouse(Character.transform);				
			
			    if ((Mathf.Abs(Input.GetAxis("Vertical")) <= 0)&&(Mathf.Abs(Input.GetAxis("Horizontal")) <= 0))
				{
				 	ChangeState(STATE_IDLE);
				}
				if (Input.GetButtonDown("Fire1"))
				{
					if (m_world!=null) m_world.BroadcastMessage("AskNewShoot", new ShootParameters(Character, Position, Global.TYPE_PROTAGONIST, SPEED_SHOOT, DAMAGE_PLAYER));
				}
				break;

			//////////////////////////////
			case STATE_DIE:
				ChangeState(STATE_END);				
				break;

			//////////////////////////////
			case STATE_END:
				if (m_iterator == 1)
				{
					Destroy();
				}
				break;
		}
	}
} 

