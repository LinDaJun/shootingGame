using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour {

	private float scrollSpeed = 0.1F;

	private Renderer render;
	float offset ;


	// Use this for initialization
	void Start () {
		render = this.GetComponent<Renderer> ();
	}

	void Update () {
		offset = Time.time * scrollSpeed;
		render.material.mainTextureOffset = new Vector2 (offset ,0);
	}
}
