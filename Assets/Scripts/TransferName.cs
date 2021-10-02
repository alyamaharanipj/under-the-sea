using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransferName : Utilities
{
    public Text nama;
    //public Text ngaran;

    public int scoreHighScore = Deserialize<int>(Application.streamingAssetsPath + "/XML/Highscores.xml");
            //variable highscore
    public string nameHighScore = Deserialize<string>(Application.streamingAssetsPath + "/XML/Username.xml");

   

void Start()
    {
        nama.text =  nameHighScore  + " : " +  scoreHighScore;
    //    ngaran.text = nameHighScore;
	}
}
