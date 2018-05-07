using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DemoGameOver : MonoBehaviour {

	public GUISkin skin;
	private float timer;
	private bool _replay;
	
	void Update(){
		if(!_replay){
			timer += Time.deltaTime;
			if(timer > 3){
				_replay = true;
			}
		}
	}

	void OnGUI(){
		if(_replay){
			GUI.skin = skin;
			if(GUI.Button(new Rect(Screen.width/2-25,Screen.height/1.5f,50,30),"Replay")){
				// Before --> Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadScene("Fucking unity");
			}
		}
	}
}
