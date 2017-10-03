using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
	
	private Rigidbody2D plane;

	[Header("單位移動量")]
	[Range(0, 50)]
	public float perDistance;

	// Use this for initialization
	void Start () {
		perDistance = 1;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector2(-1 * perDistance ,0)* Time.deltaTime);
	}
}
