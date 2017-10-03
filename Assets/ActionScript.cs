using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionScript : MonoBehaviour {
	public GameObject go_bulletsPool;
	public GameObject go_enemyPool;
	public GameObject go_displayScore;

	private ObjectPool bulletsPool;
	private ObjectPool enemyPool;

	public GameObject endCanvas;
	public GameObject rank_panel;

	[SerializeField]
	private int _Score;

	void Awake(){
		Time.timeScale = 1f;
		_Score = 0;
		bulletsPool = go_bulletsPool.GetComponent<ObjectPool> ();
		enemyPool = go_enemyPool.GetComponent<ObjectPool> ();
	}

	/// <summary>
	/// 事件觸發後傳入碰撞兩造的GameObject，並透過tag判斷做後續處理。 
	/// 不知道怎麼取名字，就醬ㄅ。
	/// </summary>
	/// <param name="go_1">Go 1.</param> 碰撞一方
	/// <param name="go_2">Go 2.</param> 碰撞的另一方
	public void OnEeventTrigger( GameObject go_1 , GameObject go_2){
		
		print (go_1.tag +" : "+  go_2.tag);

		// 子彈碰撞到右側的邊緣，回收至原物件池
		if (go_1.tag == "bullet" & go_2.tag == "edge") {
			bulletsPool.releaseToPool (go_1);
		}

		//子彈碰撞到敵機，兩造皆回至其原本物件池
		if( go_1.tag == "bullet" & go_2.tag == "enemy"){
			bulletsPool.releaseToPool (go_1);
			enemyPool.releaseToPool (go_2);

			this.OnScroreAdd (1);
			//do something 觸發分數的計算
		}

		//敵機撞到左側的邊緣，回收至原物件池。
		if (go_1.tag == "enemy" & go_2.tag == "edge") {
			enemyPool.releaseToPool (go_1);
		}

		//玩家碰撞到敵機，跳出遊戲結束及總分計算的顯示介面。
		if(go_1.tag == "enemy" & go_2.tag == "player"){
			
			endCanvas.SetActive(true);
			if (getScore() > 0) {
				rank_panel.GetComponent<RankScript> ().insertRank (getScore ());
			}
			print("Game Over");
		}
	}

	private void OnScroreAdd( int i){
			_Score = _Score + i;
			go_displayScore.GetComponent<Text> ().text = _Score.ToString ();
	}
	public int getScore(){
		return _Score;
	}

}
