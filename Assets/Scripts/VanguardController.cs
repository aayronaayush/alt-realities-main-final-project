using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VanguardController : MonoBehaviour
{
	Animator animator;
	NavMeshAgent agent;

	int isFlyingHash;
	int velocityXHash;
	int velocityZHash;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();

		isFlyingHash = Animator.StringToHash("isFlying");
		velocityXHash = Animator.StringToHash("velocityX");
		velocityZHash = Animator.StringToHash("velocityZ");
	}

	private void Update()
	{
		Vector3 velocity = agent.velocity;

		bool isFlying = velocity.magnitude > 0.01f && agent.remainingDistance > agent.radius;

		animator.SetBool(isFlyingHash, isFlying);

		velocity = transform.InverseTransformVector(velocity);
		//		transform.position += transform.forward * Time.deltaTime;
		animator.SetFloat(velocityXHash, velocity.x);
		animator.SetFloat(velocityZHash, velocity.z);

	}
}

