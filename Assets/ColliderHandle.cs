using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandle : MonoBehaviour {
	public GameObject eventSystem;

	void Awake(){
		eventSystem = GameObject.Find ("EventSystem");
	}


	void OnTriggerEnter2D(Collider2D coll){
		print ("Trigger");
		eventSystem.GetComponent<ActionScript> ().OnEeventTrigger (this.gameObject , coll.gameObject);
	}
		
	void OnCollisionEnter2D(Collision2D coll){
		print ("Collision");
		eventSystem.GetComponent<ActionScript> ().OnEeventTrigger (this.gameObject , coll.gameObject);
	}
}
