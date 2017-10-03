using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class changScene : MonoBehaviour {
	public string gameScene;

	public void GameStart(){
		SceneManager.LoadScene(gameScene);
	}
}
