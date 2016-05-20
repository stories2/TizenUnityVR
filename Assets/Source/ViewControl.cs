using UnityEngine;
using System.Collections;

public class ViewControl : MonoBehaviour {

	logger log;
	Camera camera;
	Gyro gyroscope;
	Vector3 vec3;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void set_camera_rotate(Vector3 vec3)
	{
		camera.transform.Rotate (vec3);
	}

	public void set_logger(logger log)
	{
		this.log = log;
		log.push ("logger system is rdy", "ViewControl");
	}

	public void set_camera(Camera camera)
	{
		this.camera = camera;
		camera.transform.Rotate (new Vector3 (0, 0, 0));
	}
}
