using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	//the bigger the value, the more they believe in that opinion
	public float opinion1;
	public float opinion2;
	public float persuasion; //maximum power of their persuasion compared to the player's (1)

	//colors of player 1 and 2
	public Color col1;
	public Color col2;

	public float speed;
	private float xSpeed = 0f;
	private float ySpeed = 0f;
	private bool canMove = true;
	private int dir = 0; //0 = none, 1 = left, 2 = up, 3 = right, 4 = down

	public float tileSize = .16f;

	public float chatTime; //time NPCs spend talking to each other

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//start walking in new direction
		if (canMove && xSpeed == 0 && ySpeed == 0) {

			//figure out how far it can go in all directions
			float leftDist = Mathf.Infinity;
			float rightDist = Mathf.Infinity;
			float upDist = Mathf.Infinity;
			float downDist = Mathf.Infinity;

			RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - tileSize, transform.position.y), Vector2.left);
			RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + tileSize, transform.position.y), Vector2.right);
			RaycastHit2D upHit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y + tileSize), Vector2.up);
			RaycastHit2D downHit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y - tileSize), Vector2.down);

			if (leftHit)
				leftDist = leftHit.distance / tileSize;
			if (rightHit)
				rightDist = rightHit.distance / tileSize;
			if (upHit)
				upDist = upHit.distance / tileSize;
			if (downHit)
				downDist = downHit.distance / tileSize;

			//if a direction is not viable, make sure it's because a wall blocked their path, not a player or npc
			if (leftDist == 0 && (leftHit.transform.tag == "NPC" || leftHit.transform.tag == "Player"))
				leftDist++;
			if (rightDist == 0 && (rightHit.transform.tag == "NPC" || rightHit.transform.tag == "Player"))
				rightDist++;
			if (upDist == 0 && (upHit.transform.tag == "NPC" || upHit.transform.tag == "Player"))
				upDist++;
			if (downDist == 0 && (downHit.transform.tag == "NPC" || downHit.transform.tag == "Player"))
				downDist++;

			List<int> viableDirections = new List<int>();

			//populate the list of viable directions with directions that don't involve turning back or running into a wall
			if (leftDist > 0 && dir != 3)
				viableDirections.Add (1);
			if (rightDist > 0 && dir != 1)
				viableDirections.Add (3);
			if (upDist > 0 && dir != 4)
				viableDirections.Add (2);
			if (downDist > 0 && dir != 2)
				viableDirections.Add (4);

			//choose direction
			if (viableDirections.Count == 0) { //reached dead end, turn around
				if (dir == 1) //was going left, go right now
					dir = 3;
				else if (dir == 2) //was going up, go down now
					dir = 4;
				else if (dir == 3) //was going right, go left now
					dir = 1;
				else if (dir == 4) //was going down, go up now
					dir = 2;
				else //was going nowhere, still can't go anywhere
					dir = 0;
			}

			if (viableDirections.Count == 1) //only one way you can go
				dir = viableDirections[0];

			if (viableDirections.Count == 2) //fork in the road
				dir = viableDirections [Random.Range (0, 2)];

			else { //probably in the plaza

				//if directions were chosen randomly in the plaza
				//they'd just bumble around in place for a long time
				//which looks fake as fuuuuuuck
				//so if they're in the plaza, there's an 80% chance 
				//that they'll keep going the same direction
				//unless that's not a viable option
				float i = Random.Range (0, 1);
				if (i < 0.2f || !viableDirections.Contains (dir))
					dir = viableDirections [Random.Range (0, viableDirections.Count)];

			}

			//go in the chosen direction
			if (dir == 0) { //keep still
				xSpeed = 0f;
				ySpeed = 0f;
			} else if (dir == 1) { //go left
				xSpeed = -speed;
				ySpeed = 0f;
			} else if (dir == 2) { //go up
				xSpeed = 0f;
				ySpeed = speed;
			} else if (dir == 3) { //go right
				xSpeed = speed;
				ySpeed = 0f;
			} else if (dir == 4) { //go down
				xSpeed = 0f;
				ySpeed = -speed;
			}

			//INVOKE NEXT DIRECTION CHANGE AND SNAP TO CENTER OF TILE

		}
		
	}

	void OnCollisionEnter2D (Collision2D coll) {

		if (coll.gameObject.tag == "NPC") {

			ConvinceNPC (coll.gameObject);
			xSpeed = 0f;
			ySpeed = 0f;
			canMove = false;
			Invoke ("AllowMovement", chatTime);

		}

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

		//change their color
		float percent1 = opinion1 / (opinion1 + opinion2);
		float percent2 = opinion2 / (opinion1 + opinion2);
		GetComponent<SpriteRenderer> ().color = new Color (
			col1.r * percent1 + col2.r * percent2,
			col1.g * percent1 + col2.g * percent2,
			col1.b * percent1 + col2.b * percent2, 1f);

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

	void AllowMovement() {

		canMove = true;

	}
}
