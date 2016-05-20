using UnityEngine;
using System.Collections;

public class logger : MonoBehaviour {

	string[] queue;
	int r = -1,f = -1,limit = 10;

	// Use this for initialization
	void Start () {
	
		init ();
	}

	bool is_queue_full()
	{
		return f == (r + 1) % limit;
	}

	bool is_queue_empty()
	{
		return (f + 1) % limit == r % limit;
	}

	public void push(string target , string source)
	{
		if (is_queue_full ()) {
			//Debug.Log ("full "+r+" : "+f);
			return;
		}
		r = (r + 1) % limit;
		queue [r] = target + " <" + source + ">";
	}

	public void pop()
	{
		if (is_queue_empty ()) {
			//Debug.Log ("empty " + r + " : " + f);
			return;
		}
		f = (f + 1) % limit;
		Debug.Log ("#" + f + " " + queue [f]);
	}

	void init()
	{
		queue = new string[limit];
		int i;
		for (i = 0; i < limit; i += 1) 
		{
			queue [i] = "";
		}

		push ("logger system is rdy", "logger");
	}
	
	// Update is called once per frame
	void Update () {
	
		pop ();
	}
}
