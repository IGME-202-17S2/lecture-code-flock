using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public List<Rover> flock;
	public int flockSize;
	public GameObject roverPrefab;

	public Vector3 flockHeading;
	public Vector3 averagePos;

	public float alignWeight = 0.75f;
	public float cohereWeight = 0.5f;

	// Use this for initialization
	void Start () {
		flock = new List<Rover> ();
		createFlock ();
	}

	void createFlock() {
		if (roverPrefab) {
			for (int i = 0; i < flockSize; i++) {
//				Vector3 pos = new Vector3(Random.Range (-20f, 20f), 0, Random.Range (-20f, 20f));
//				GameObject member = Instantiate (roverPrefab, pos, Quaternion.identity);
//				Rover rover = member.GetComponent<Rover> ();
//				rover.velocity = new Vector3 (Random.Range (-3f, 3f), 0, Random.Range (-3f, 3f));
//				rover.SetManager (this);
//				flock.Add (rover);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
//		flockHeading = Vector3.zero;
//		Vector3 flockPos = Vector3.zero;
//
//		for (int i = 0; i < flock.Count; i++) {
//			flockHeading += flock [i].velocity;
//			flockPos += flock [i].transform.position;
//		}
//
//		flockHeading.Normalize ();
//		averagePos = flockPos / flock.Count;
	}
}
