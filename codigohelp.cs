using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class codigo_help_xd : MonoBehaviour {
	int xx=0;
	// Use this for initialization
	Text texto=GameObject.FindGameObjectWithTag("text_error").GetComponent<Text>();
	void Start () {
	texto.text="1.3";
	}
	void Update(){
	if(xx<1){
		Debug.Log("xesmenor a 1");
		xx=2;
	}
	
	
	}
}
