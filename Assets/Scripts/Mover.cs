using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour {

	CharacterController characterController;
	Vector3 acceleration;
	public Vector3 velocity;

	public float mass = 1f;
	public float maxSpeed = 0.5f;
	public float maxTurn = 0.25f;
	public float radius = 0.5f;
	public GameObject futurePosIndicator;

	protected virtual void Start () {
		this.characterController = GetComponent<CharacterController> ();

		velocity = Vector3.zero;
		acceleration = Vector3.zero;
	}
		
	protected abstract void CalcSteering ();

	protected Vector3 Seek(Vector3 targetPos) {
		Vector3 toTarget = targetPos - this.transform.position;
		Vector3 desiredVelocity = toTarget.normalized * maxSpeed;
		Vector3 steeringForce = desiredVelocity - velocity;

		return VectorHelper.Clamp (steeringForce, maxTurn);
	}
		
	protected Vector3 Flee(Vector3 targetPos) {
		// not yet implemented
		return Vector3.zero;
	}
		
	protected Vector3 Arrive(Vector3 targetPos, float threshold, float radii) {
		Vector3 toTarget = targetPos - this.transform.position;
		Vector3 desiredVelocity;

		if (toTarget.magnitude - radii < threshold) {
			float percentFromCenter = (toTarget.magnitude - radii) / threshold;
			float fractionOfMaxSpeed = percentFromCenter * maxSpeed;

			desiredVelocity = toTarget.normalized * fractionOfMaxSpeed;
		} else {
			desiredVelocity = toTarget.normalized * maxSpeed;
		}
			
		Vector3 steeringForce = desiredVelocity - velocity;

		return VectorHelper.Clamp (steeringForce, maxTurn);
	}

	public void ApplyForce(Vector3 force) {
		// tired of having things steer up or down from their current location?
		// set force.y to 0, and it will never be tempted to accelerate up or down!
		// one y to rule them all!
		force.y = 0;
		acceleration += force / mass;
	}

	public Vector3 FuturePosition(float seconds) {
		return this.transform.position + velocity * seconds;
	}

	public Vector3 AvoidWall() {
		return Seek (Vector3.zero);
	}

	public Vector3 Align(Vector3 heading) {

//		Vector3 desiredVelocity = heading * maxSpeed;
//		Vector3 steeringForce = desiredVelocity - velocity;

//		return VectorHelper.Clamp(steeringForce, maxTurn);
		return Vector3.zero;
	}

	public Vector3 Cohere(Vector3 targetPos) {
//		return Seek(targetPos);
		return Vector3.zero;
	}
		
	void LateUpdate () {

		CalcSteering ();

		velocity += acceleration;
		velocity = VectorHelper.Clamp (velocity, maxSpeed);
		this.characterController.Move (velocity * Time.deltaTime);

		this.futurePosIndicator.transform.position = FuturePosition (3f);

		acceleration = Vector3.zero;
	}

	protected virtual void OnRenderObject() {
		ColorHelper.black.SetPass (0);

		GL.Begin (GL.LINES);
		GL.Vertex (transform.position);
		GL.Vertex (transform.position + velocity.normalized);
		GL.End ();
	}
}
