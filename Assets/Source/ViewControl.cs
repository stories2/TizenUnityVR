using UnityEngine;
using System.Collections;

public class ViewControl : MonoBehaviour {

	logger log;
	Camera[] camera;
	Vector3 vec3;

	// Use this for initialization
	void Start () {
		camera = new Camera[2];
	}
	
	// Update is called once per frame
	void Update () {
		set_camera_viewport (0.0F, 0.0F, 0.5F, 1F, 0);
		set_camera_viewport (0.5F, 0.0F, 0.5F, 1.0F, 1);
	}

	public Vector3 get_camera_rotate(int target)
	{
		return new Vector3 (camera [target].transform.rotation.x, camera [target].transform.rotation.y, camera [target].transform.rotation.z);
	}

	void set_camera_viewport(float x, float y, float width, float height, int target)
	{
		camera [target].rect = new Rect (x, y, width, height);
	}

	public void set_camera_rotate(Vector3 vec3, int target)
	{
		log.push ("" + vec3.x + " " + vec3.y + " " + vec3.z, "ViewControl");
		camera [target].transform.Rotate (-vec3.x, 0, 0, Space.World);
		camera [target].transform.Rotate (0, -vec3.y, 0, Space.Self);
	//	camera [target].transform.Rotate (0, 0, vec3.z);
	}

	public void set_logger(logger log)
	{
		this.log = log;
		log.push ("logger system is rdy", "ViewControl");
	}

	public void set_camera(Camera camera,int target)
	{
		this.camera [target] = camera;
		this.camera [target].transform.Rotate (new Vector3 (0, 0, 0));
	}
}
