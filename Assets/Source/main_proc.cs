using UnityEngine;
using System.Collections;

public class main_proc : MonoBehaviour {

	public Camera camera;
	public ViewControl view;
	public Gyro gyroscope;
	public logger log;
	bool r_flag;
	Vector3 vec3;

	// Use this for initialization
	void Start () {

		view = null;
		gyroscope = null;
		log = null;
		r_flag = true;
		Input.gyro.enabled = true;

		log = gameObject.AddComponent<logger> ();
		gyroscope = gameObject.AddComponent<Gyro> ();
		view = gameObject.AddComponent<ViewControl> ();

		log.push ("init ok", "main_proc");
	}
	
	// Update is called once per frame
	void Update () {
	
		if (r_flag == true) {
			r_flag = false;
			view.set_logger (log);
			gyroscope.set_logger (log);
			view.set_camera (camera);
		}
		vec3 = gyroscope.return_now_vector ();

		//i/o
		if (gyroscope.is_platform_window () != true) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.Quit ();
			}
		}
	}

	void OnGUI()
	{
		GUI.Label (new Rect (0, 0, Screen.width, 20), "" + vec3.x + " " + vec3.y + " " + vec3.z);
		//GUI.Label (new Rect (0, 20, Screen.width, 20), "" + Input.acceleration.x + " " + Input.acceleration.y + " " + Input.acceleration.z);
	}

	void delay(int timer)
	{
		while(timer>0)
			timer -= 1;
	}
}
