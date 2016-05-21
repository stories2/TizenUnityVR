using UnityEngine;
using System.Collections;

public class main_proc : MonoBehaviour {

	public Camera main_camera,sub_camera;
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
			view.set_camera (main_camera,0);
			view.set_camera (sub_camera,1);
		}
		//process

		//get user x y z position
		if (gyroscope.is_platform_window ())
			vec3 = gyroscope.return_now_xyz ();
		else
			vec3 = gyroscope.return_now_vector ();

		//rotate calculation
		int i;
		for (i = 0; i < 2; i += 1) {
			if (gyroscope.is_platform_window ()) {
				view.set_camera_rotate (calculate_rotate (gyroscope.return_prev_xyz (), gyroscope.return_now_xyz ()), i);
			} 
			else {
				view.set_camera_rotate (calculate_rotate (level_upper (gyroscope.return_prev_vector (), 100), level_upper (gyroscope.return_now_vector (), 100)), i);
			}
		}

		//i/o
		if (gyroscope.is_platform_window () != true) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.Quit ();
			}
		}
	}

	Vector3 level_upper(Vector3 target, int range)
	{
		return new Vector3 (target.x * range, target.y * range, target.z * range);
	}

	void OnGUI()
	{
		GUI.Label (new Rect (0, 0, Screen.width, 20), "" + vec3.x + " " + vec3.y + " " + vec3.z);
		//GUI.Label (new Rect (0, 20, Screen.width, 20), "" + Input.acceleration.x + " " + Input.acceleration.y + " " + Input.acceleration.z);
	}

	Vector3 calculate_rotate(Vector3 prev_vec3, Vector3 now_vec3)
	{
		Vector3 vec3 = new Vector3 ();
		if (gyroscope.is_platform_window ()) {
			vec3 = new Vector3 (-(now_vec3.y - prev_vec3.y), now_vec3.x - prev_vec3.x , 0);
			//log.push ("p " + prev_vec3.x + " " + prev_vec3.y + " " + prev_vec3.z + " n " + now_vec3.x + " " + now_vec3.y + " " + now_vec3.z + " d " + vec3.x + " " + vec3.y + " " + vec3.z, "main_proc");
		} 
		else {
			
			vec3 = new Vector3 (now_vec3.z - prev_vec3.z, now_vec3.x - prev_vec3.x, get_angle (now_vec3.x - prev_vec3.x, now_vec3.y - prev_vec3.y));
		}
		return vec3;
	}

	float get_angle(float a,float b)
	{
		if (abs (a) != a)
			return -(abs (a) + abs (b));
		return abs (a) + abs (b);
	}

	float abs(float target)
	{
		if (target < 0)
			return -target;
		return target;
	}

	void delay(int timer)
	{
		while(timer>0)
			timer -= 1;
	}
}
