using UnityEngine;
using System.Collections;

public class WorldActorAct : MonoBehaviour {

	//[RequireComponent(typeof(Rigidbody))]

	private Rigidbody m_Rigidbody;

	private float actorMoveSpeed = 8f;
	private float actorStopWalkValue = 0.1f;

	private bool m_IsGrounded;
	private float m_GroundCheckDistance = 0.3f;
	private Animator m_Animator;
	private Quaternion targetRotation;

	private float rSpeed=9f;

	public GameObject actionActor;
	public GameObject aimForCamera;

	public float m_GravityMultiplier;

	// Use this for initialization
	void Start () {
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Animator = actionActor.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Quaternion.Angle (transform.rotation, targetRotation) > 0.1) {
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, rSpeed * Time.deltaTime);
		}
	}

	public void Move (Vector3 move_direction){

		//Velocity 指的是當前速度，並不需要加上deltaTime
		m_Rigidbody.velocity = move_direction * actorMoveSpeed + new Vector3(0 , m_Rigidbody.velocity.y , 0);

		//不做地面偵測但施力會懸空

		CheckGroundStatus ();
		if (!m_IsGrounded && Time.deltaTime > 0)
		{
			Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
			m_Rigidbody.AddForce(extraGravityForce);
			//Debug.Log ("not on ground , add force");
			//m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
		}


		//m_Rigidbody.velocity.Set (move_direction.x*actorMoveSpeed, m_Rigidbody.velocity.y, move_direction.z*actorMoveSpeed);

		if (move_direction.magnitude < actorStopWalkValue) {
			m_Animator.SetBool ("isWalk", false);
		} else {
			m_Animator.SetBool ("isWalk",true);
		}

		if (move_direction.magnitude != 0) {
			move_direction.Normalize ();
			move_direction *= 3;
			if(move_direction != Vector3.zero) targetRotation = Quaternion.LookRotation (move_direction);
		}


		// Smoothly rotate towards the target point.

		//transform.LookAt (transform.position + move_direction);
	}

	private bool CheckGroundStatus()
	{
		RaycastHit hitInfo;
		#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
		#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character

		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance = 0.3f))
		{
			m_IsGrounded = true;
			return true;
			//m_Animator.applyRootMotion = true;
		}
		else
		{
			m_IsGrounded = false;
			//m_GroundNormal = Vector3.up;
			//m_Animator.applyRootMotion = false;
		}
		return false;
	}

	public bool isGround {
		get { return m_IsGrounded; }
	}
}
