using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetter : MonoBehaviour {

	public GameObject go_enemyPool;
	private ObjectPool enemyPool;

	[Header("生怪時間長度")]
	[Range( 0f ,3.0f )]
	public float CDTime;

	private float CoolDown ;
	private int enemyAmount;

	void Awake (){
		CoolDown = 0f;
		enemyPool = go_enemyPool.GetComponent<ObjectPool> ();
	}

	void Update () {
		setEnemy ();
	}

	private void setEnemy(){
		if(CoolDown <= 0 ){
			int y = Random.Range (-200, 195);
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x, y, this.transform.localPosition.z);

			enemyPool.getObject (this.transform);
			CoolDown = CDTime;
		}
		OnCoolDown ();
	}

	//冷卻時間
	private void OnCoolDown (){
		if (CoolDown > 0) {
			CoolDown = CoolDown - Time.deltaTime;
		}
	}
}
