using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletMove: MonoBehaviour {
	void Update (){
		transform.Translate(new Vector2( 1 * 8 ,0)* Time.deltaTime);
	}
}
