using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

namespace Models
{
	public class SSDirector : System.Object
	{
		private static SSDirector _instance;
		public Controller CurrentSceneController { get; set; }

		public static SSDirector GetInstance()
		{
			if (_instance == null)
			{
				_instance = new SSDirector();
			}
			return _instance;
		}
	}

	public interface ISceneController
	{
		void loadResources();
		void loadCharacter();
	}
	public interface IUserAction
	{
		void ClickBoat();
		void ClickCha(ChaCon characterCtrl);
		void restart();
	}
	public class ChaCon
	{
		GameObject character;
		Move move;
		Click click;
		int characterType;  // 0->priest, 1->devil
		bool isonboat;
		CoastCon CoastCon;

		public ChaCon(string name)
		{
			if (name == "priest")
			{
				character = Object.Instantiate(Resources.Load("Prefabs/Priests", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
				characterType = 0;
			}
			else
			{
				character = Object.Instantiate(Resources.Load("Prefabs/Devils", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
				characterType = 1;
			}
			move = character.AddComponent(typeof(Move)) as Move;
			click = character.AddComponent(typeof(Click)) as Click;
			click.setController(this);
		}

		public void setName(string name) { character.name = name; }

		public void setPosition(Vector3 pos) { character.transform.position = pos; }

		public void moveToPosition(Vector3 dest) { move.setDest(dest); }

		public int getType() { return characterType; }

		public string getName() { return character.name; }

		public void getOnBoat(BoatCon boat)
		{
			CoastCon = null;
			character.transform.parent = boat.getGameobj().transform;
			isonboat = true;
		}

		public void getOnCoast(CoastCon coast)
		{
			CoastCon = coast;
			character.transform.parent = null;
			isonboat = false;
		}
		public bool isOnBoat() { return isonboat; }

		public CoastCon getCoastCon() { return CoastCon; }

		public void reset()
		{
			move.reset();
			CoastCon = (SSDirector.GetInstance().CurrentSceneController as Controller).fromCoast;
			getOnCoast(CoastCon);
			setPosition(CoastCon.getEmptyPosition());
			CoastCon.getOnCoast(this);
		}
	}

	public class CoastCon
	{
		GameObject coast;
		Vector3 from_pos = new Vector3(9, 1, 0);
		Vector3 to_pos = new Vector3(-9, 1, 0);
		Vector3[] positions;
		int status; // to->-1, from->1

		ChaCon[] passenger;

		public CoastCon(string _status)
		{
			positions = new Vector3[] {new Vector3(6.5F,2.25F,0), new Vector3(7.5F,2.25F,0), new Vector3(8.5F,2.25F,0),
				new Vector3(9.5F,2.25F,0), new Vector3(10.5F,2.25F,0), new Vector3(11.5F,2.25F,0)};

			passenger = new ChaCon[6];

			if (_status == "from")
			{
				coast = Object.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject)), from_pos, Quaternion.identity, null) as GameObject;
				coast.name = "from";
				status = 1;
			}
			else
			{
				coast = Object.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject)), to_pos, Quaternion.identity, null) as GameObject;
				coast.name = "to";
				status = -1;
			}
		}

		public int getEmptyIndex()
		{
			for (int i = 0; i < passenger.Length; i++)
			{
				if (passenger[i] == null) return i;
			}
			return -1;
		}

		public Vector3 getEmptyPosition()
		{
			Vector3 pos = positions[getEmptyIndex()];
			pos.x *= status;
			return pos;
		}

		public void getOnCoast(ChaCon cha)
		{
			int index = getEmptyIndex();
			passenger[index] = cha;
		}

		public ChaCon getOffCoast(string name)
		{   // 0->priest, 1->devil
			for (int i = 0; i < passenger.Length; i++)
			{
				if (passenger[i] != null && passenger[i].getName() == name)
				{
					ChaCon cha = passenger[i];
					passenger[i] = null;
					return cha;
				}
			}
			return null;
		}

		public int getStatus() { return status; }

		public int[] getCharacterNum()
		{
			int[] count = { 0, 0 };
			for (int i = 0; i < passenger.Length; i++)
			{
				if (passenger[i] == null) continue;
				count[passenger[i].getType()]++;
			}
			return count;
		}

		public void reset() { passenger = new ChaCon[6]; }
	}

	public class BoatCon
	{
		GameObject boat;
		Move move;
		Vector3 fromPosition = new Vector3(5, 1, 0);
		Vector3 toPosition = new Vector3(-5, 1, 0);
		Vector3[] from_positions;
		Vector3[] to_positions;

		int status; // to->-1; from->1
		ChaCon[] passenger = new ChaCon[2];

		public BoatCon()
		{
			status = 1;
			from_positions = new Vector3[] { new Vector3(4.5F, 1.5F, 0), new Vector3(5.5F, 1.5F, 0) };
			to_positions = new Vector3[] { new Vector3(-5.5F, 1.5F, 0), new Vector3(-4.5F, 1.5F, 0) };

			boat = Object.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject)), fromPosition, Quaternion.identity, null) as GameObject;
			boat.name = "boat";

			move = boat.AddComponent(typeof(Move)) as Move;
			boat.AddComponent(typeof(Click));
		}


		public void Move()
		{
			if (status == -1) move.setDest(fromPosition);
			else move.setDest(toPosition);
			status = -status;
		}

		public int getEmptyIndex()
		{
			for (int i = 0; i < passenger.Length; i++)
				if (passenger[i] == null) return i;
			return -1;
		}

		public bool isEmpty()
		{
			for (int i = 0; i < passenger.Length; i++)
				if (passenger[i] != null) return false;
			return true;
		}

		public Vector3 getEmptyPosition()
		{
			Vector3 pos;
			int emptyIndex = getEmptyIndex();
			if (status == -1) pos = to_positions[emptyIndex];
			else pos = from_positions[emptyIndex];
			return pos;
		}

		public void getOnBoat(ChaCon cha)
		{
			int index = getEmptyIndex();
			passenger[index] = cha;
		}

		public ChaCon getOffBoat(string name)
		{
			for (int i = 0; i < passenger.Length; i++)
			{
				if (passenger[i] != null && passenger[i].getName() == name)
				{
					ChaCon cha = passenger[i];
					passenger[i] = null;
					return cha;
				}
			}
			return null;
		}

		public GameObject getGameobj() { return boat; }

		public int getStatus() { return status; }

		public int[] getCharacterNum()
		{
			int[] count = { 0, 0 };
			for (int i = 0; i < passenger.Length; i++)
			{
				if (passenger[i] == null) continue;
				count[passenger[i].getType()]++;
			}
			return count;
		}

		public void reset()
		{
			move.reset();
			if (status == -1) Move();
			passenger = new ChaCon[2];
		}
	}
}