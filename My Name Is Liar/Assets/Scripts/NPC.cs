using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	//the bigger the value, the more they believe in that opinion
	public float opinion1;
	public float opinion2;
	public float persuasion; //maximum power of their persuasion compared to the player's (1)

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeOpinion (int opinion, float value) { //call this function when their belief on either opinion has strengthened or weakened

		if (opinion == 1) //player 1 made them believe their opinion more
			opinion1 += value;
		
		else if (opinion == 2) //player 2 made them believe their opinion more
			opinion2 += value;
		
		else if (opinion == -1) { //player 1 made them believe their opinion less
			if (opinion1 >= value)
				opinion1 -= value;
			else
				opinion1 = 0f;
			
		} else if (opinion == -2) { //player 2 made them believe their opinion less
			if (opinion2 >= value)
				opinion2 -= value;
			else
				opinion2 = 0;
		}

	}

	void ConvinceNPC (GameObject otherNPC) { //call this function when this NPC tries to sway another NPC's views

		if (otherNPC.GetComponent<NPC>() != null) {

			int opinion;

			//find out which player they are siding with
			if (opinion1 > opinion2)
				opinion = 1;
			else if (opinion1 < opinion2)
				opinion = 2;
			else //completely undecided
			opinion = 0;

			if (opinion != 0) {

				float value = 0;

				//generate value to show how strongly they believe their opinion
				//0 for completely neutral, persuasion variable for strongly siding with one side
				if (opinion == 1)
					value = ((opinion1 / (opinion1 + opinion2)) - 0.5f) * 2f * persuasion;
				else if (opinion == 2)
					value = ((opinion2 / (opinion1 + opinion2)) - 0.5f) * 2f * persuasion;

				otherNPC.GetComponent<NPC> ().ChangeOpinion (opinion, value);

			}

		}

	}
}
