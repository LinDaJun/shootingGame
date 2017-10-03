using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;  //json讀檔用
class Data{
	public string name;
	public int score;
	public Data(string _name , int _score){
		name = _name;
		score = _score;
	}
}

public class RankScript : MonoBehaviour {
	
	const int RANK_AMOUNT = 5;
	string[] rank_Str = { "1st", "2nd" , "3rd", "4th", "5th"};

	//rank panel的陣列  
	public GameObject[] rank_panel = new GameObject[RANK_AMOUNT]; 
	List<Data> rankData = new List<Data>();
	string jsonFilePath = null;

	//就我的理解是只要遊戲啟動就會執行awake()，
	//遊戲開始就會去執行排行榜的初始化，
	//遊戲結束後，叫出endcanvas時就不用等待刷新了。

	void Awake(){
		jsonFilePath = System.IO.Path.Combine (Application.streamingAssetsPath, "RankFile");
		load();
	}

	//載入RANK資料 並儲存進ranlData
	void load(){
		if ( File.Exists (jsonFilePath) ) {
			print ("File exists");
		
			StreamReader file = new System.IO.StreamReader (jsonFilePath);
			string line;
			int i = 0;

			while((line = file.ReadLine()) != null){
				rankData.Add (JsonUtility.FromJson<Data> (line));
				i++;
			}

		} else {
			print ("File doesn't exists");
		}
	}

	void displayRank(){

		for (int rank = 0; rank < RANK_AMOUNT; rank++) {
			
			InputField name = rank_panel[rank].transform.GetChild (1).gameObject.GetComponent<InputField> ();
			name.text = rankData [rank].name;

			Text score = rank_panel [rank].transform.GetChild (2).gameObject.GetComponent<Text> ();
			string score_str = rankData [rank].score.ToString ();
			score.text = (score_str.Equals("0")) ? "" : score_str;
		}
	}


	public void insertRank(int score){
		
		for(int i = 0 ;i < RANK_AMOUNT ; i++){
			if (score >= rankData [i].score) {
				rankData.Insert (i, new Data ("", score ));
				turnToInput (i);
				break;
			}
		}

		displayRank ();
	}


	void turnToInput(int i ){
		
		GameObject go = rank_panel[i].transform.GetChild (1).gameObject;

		InputField ipf = go.GetComponent<InputField> ();
		ipf.transition = Selectable.Transition.ColorTint;
		ipf.readOnly = false; 			//取消唯獨
		ipf.caretBlinkRate = 0.85f;		//光標閃爍頻率
		ipf.customCaretColor = false;	//取消自製光標顏色
		ipf.Select();
		ipf.ActivateInputField ();

		ipf.onEndEdit.AddListener(
			delegate {
				turnToReadOnly(go);
				inputName(go ,i);
				save();
			}
		);

		Color clr = new Color ();
		//將hex顏色轉換出來
		ColorUtility.TryParseHtmlString ("#A8CEFF64", out clr);
		ipf.selectionColor = clr;		//設定光標顏色

		ColorUtility.TryParseHtmlString ("#FFFFFF5D", out clr);
		go.GetComponent<Image> ().color = clr; //設定背景顏色，凸顯其需要輸入

	}

	void turnToReadOnly(GameObject go){

		InputField ipf = go.GetComponent<InputField> ();
		ipf.transition = Selectable.Transition.None;
		ipf.readOnly = true;
		ipf.caretBlinkRate = 0;			//光標閃爍頻率
		ipf.customCaretColor = true;	//取消自製光標顏色

		Color clr = new Color ();
		//將hex顏色轉換出來
		ColorUtility.TryParseHtmlString ("#A8CEFF00", out clr);
		ipf.selectionColor = clr;		//設定光標顏色

		ColorUtility.TryParseHtmlString ("#FFFFFF00", out clr);
		go.GetComponent<Image> ().color = clr; //設定背景顏色，凸顯其需要輸入
	}


	void inputName(GameObject go, int rank){
		string str = go.GetComponent<InputField> ().text;
		rankData [rank].name = str;

	}

	void save(){
		print ("save");

		string Data_json;
		StreamWriter file;

		if (File.Exists (jsonFilePath)) {//如果存在就刪掉，重新寫一份XD
			File.Delete(jsonFilePath);
			print ("Delete 嚕~");
		}
		file = File.CreateText (jsonFilePath);

		for (int i = 0; i < rankData.Count; i ++) {
			
			//轉成json格式
			Data_json = JsonUtility.ToJson (rankData[i]);

			print ("Json"+Data_json);

			//直接存入一行json
			file.WriteLine (Data_json);
		}

		file.Close ();			//關閉串流
		rankData.Clear ();		//清空rankData的list，避免load的時候重複append進去。
	}
}
