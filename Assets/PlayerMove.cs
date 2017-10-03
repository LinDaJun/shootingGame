using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour {


	private Rigidbody2D player;

	private float CoolDown;

	[Header("冷卻時間長度")]
	[Range( 0f ,1.0f )]
	public float CDTime;

	[Header("單位移動量")]
	[Range(0, 50)]
	public float perDistance;

	ObjectPool bulletsPool;
	/// <summary>
	/// 上 下 左 右，各自的邊緣檢測
	/// </summary>
	private bool isInUpper = false;
	private bool isInLower = false;
	private bool isInLeft  = false;
	private bool isInRight = false;

	void Awake(){
		bulletsPool = GameObject.Find ("BulletsPool").GetComponent<ObjectPool> ();
	}

	// Use this for initialization
	void Start () {
		CDTime = 0.2f;
		CoolDown = 0;
		perDistance = 10;
		player = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		movingControl ();
		OnShooting ();
		OnCoolDown ();
	}

	/// <summary>
	/// 移動控制
	/// </summary>
	private void movingControl(){
		//player.velocity = new Vector2 (0, 1);
		//player.AddForce(new Vector2(1, 0));

		if( Input.GetKey(KeyCode.UpArrow) & (!isInUpper) ){
			transform.Translate(new Vector2(0 , 1 * perDistance)* Time.deltaTime);
		}

		if( Input.GetKey(KeyCode.DownArrow) & (!isInLower) ){
			transform.Translate(new Vector2(0 ,-1 * perDistance)* Time.deltaTime);
		}

		if( Input.GetKey(KeyCode.RightArrow) & (!isInRight) ){
			transform.Translate(new Vector2( 1 * perDistance ,0)* Time.deltaTime);
		}

		if( Input.GetKey(KeyCode.LeftArrow) & (!isInLeft) ){
			transform.Translate(new Vector2(-1 * perDistance ,0)* Time.deltaTime);
		}
	}

	/// <summary>
	/// 射擊動作 , space 觸發發射子彈
	/// </summary>
	private void OnShooting(){
		if (Input.GetKey (KeyCode.Space) & (CoolDown <= 0)) {
			
		/*
		 * //無法理解為什麼直接用transform.position 會多 z 90 
		 * //可以使用 ，但是destory後會出錯誤，嘗試其他方式。
			bullet = Instantiate (bullet, transform.localPosition, new Quaternion (0, 0, 0, 0));
			bullet.transform.SetParent (GameObject.FindGameObjectWithTag ("canvas").transform, false);
			print ("shot the bullet");

		*/	

			bulletsPool.getObject (transform);
			CoolDown = CDTime;
		}
	}
	//冷卻時間
	private void OnCoolDown (){
		if (CoolDown > 0) {
			//print (CoolDown);
			CoolDown = CoolDown - Time.deltaTime;
		}
	}

	/// <summary>
	/// 邊緣碰撞控制，如果進入碰撞，就限制其能夠繼續向其碰撞的操作方向。
	/// 如與上方邊緣碰撞，設 isInUpper = true ,
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnCollisionEnter2D(Collision2D coll){
		print ("Enter : " + coll.gameObject.name);

		switch (coll.gameObject.name) {
			case "UpperEdge":
				isInUpper = true;
				break;

			case "LowerEdge":
				isInLower = true;
				break;

			case "LeftEdge":
				isInLeft = true;
				break;

			case "RightEdge":
				isInRight = true;
				break;

			default:
				print(coll.gameObject.name);
				break;
		}
	}

	/// <summary>
	/// 如果離開碰撞，將其設定為 false 
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnCollisionExit2D(Collision2D coll){
		print ("Exit : " + coll.gameObject.name);

		switch (coll.gameObject.name) {
		case "UpperEdge":
			isInUpper = false;
			break;

		case "LowerEdge":
			isInLower = false;
			break;

		case "LeftEdge":
			isInLeft = false;
			break;

		case "RightEdge":
			isInRight = false;
			break;

		default:
			print(coll.gameObject.name);
			break;
		}
	
	}


	/*
	 * void OnCollisionStay2D(){
		print("C"); //接觸著碰撞器時印出"C"
	}

	void OnCollisionExit2D(Collision2D coll){
		print (coll.gameObject.name);
		　print("B"); //離開碰撞器時印出"B"
	}*/


}
