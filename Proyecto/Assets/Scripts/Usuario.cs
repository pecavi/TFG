using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Usuario {

	public static string id,username, email;

	public static long maxScore1, maxScore2, maxScore3;

	public static long getScore(int gameMode){
		switch(gameMode){
			case 1: return maxScore1; break;
			case 2: return maxScore2; break;
			case 3: return maxScore3; break;
			default: return 0; break;
		}
	}

	public static void setScore(int gameMode, long score){
		switch(gameMode){
			case 1: maxScore1=score; break;
			case 2:  maxScore2=score; break;
			case 3:  maxScore3=score; break;
			default: return; break;
		}
	}

}
