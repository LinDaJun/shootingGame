using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	public GameObject prefab;

	[Header("最大數量")]
	[Range(0,20)]
	public int max_count ;

	private List<GameObject> _availableObjectsList = new List<GameObject>();

	//儲存原物件池是誰
	private Transform parent_pool;

	public void Awake() {
		parent_pool = this.transform;

		for (int i = 0; i < max_count; i++) {
			GameObject go = Instantiate <GameObject> (prefab, this.transform );
			go.name = prefab.tag + i.ToString ();
			_availableObjectsList.Add (go);
			go.SetActive (false);
		}
	}

	public GameObject getObject(Transform tf){
		lock (_availableObjectsList) {

			int lastIndex = _availableObjectsList.Count -1;
			GameObject go = null;

			if (lastIndex >= 0) {
				
				go = _availableObjectsList [lastIndex];
				_availableObjectsList.RemoveAt (lastIndex);
				go.SetActive (true);

			} else {
				
				go  = Instantiate <GameObject>( prefab,tf);
			}
				
			go.transform.SetParent (tf.parent , false);
			go.transform.position = tf.position;
			print ("go.pos =" + go.transform.position.ToString ());
			return go;
		}
	}

	public void releaseToPool (GameObject go){
		lock (_availableObjectsList) {
			_availableObjectsList.Add (go);
			go.SetActive (false);
			go.transform.SetParent (parent_pool);
		}
	}
}


