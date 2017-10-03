using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndCanvas : MonoBehaviour {
	public GameObject go_reBtn;
	public GameObject go_BackBtn;

	public string titleScene ;
	public string playingScene ;

	void Awake (){
		this.gameObject.SetActive (false);
	}
	// Use this for initialization
	void Start () {
		Time.timeScale = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void restartGame(){
		SceneManager.LoadScene(playingScene);
	}

	public void backTitle(){
		SceneManager.LoadScene(titleScene);
	}
}
