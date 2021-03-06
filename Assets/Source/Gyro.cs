﻿using UnityEngine;
using System.Collections;

public class Gyro : MonoBehaviour {

	float[] mouse;
	Gyroscope gyroscope;
	int xyz = 3;
	logger log;
	Vector3[] accel;

	// Use this for initialization
	void Start () {

		init ();
	}
	
	// Update is called once per frame
	void Update () {
	
		try
		{/*
			int i;
			string str = "";
			for(i=1;i<=3;i+=1)
			{
				get_mouse_pos(mouse,i);
				str = str + mouse[i] + ",";
			}
			log.push(str,"Gyro");*/
			int i;
			for(i=4;i<=6;i+=1)
			{
				mouse[i-xyz] = set_mouse_pos(mouse, i);
				get_mouse_pos(mouse, i);
			}
			accel[0] = set_vec(accel[1]);
			accel[1] = get_vec();
		}
		catch(UnityException e) 
		{
			log.push ("Error " + e, "Gyro");
		}
	}

	Vector3 get_vec()
	{
		return new Vector3 (Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
	}

	Vector3 set_vec(Vector3 vec)
	{
		return vec;
	}

	public Vector3 return_init_vector()
	{
		return accel [2];
	}

	public Vector3 return_now_vector()
	{
		return accel [1];
	}

	public Vector3 return_prev_vector()
	{
		return accel [0];
	}

	public Vector3 return_init_xyz()
	{
		return new Vector3 (mouse [7], mouse [8], mouse [9]);
	}

	public Vector3 return_prev_xyz()
	{
		return new Vector3 (mouse [1], mouse [2], mouse [3]);
	}

	public Vector3 return_now_xyz()
	{
		return new Vector3 (mouse [4], mouse [5], mouse [6]);
	}

	public void set_logger(logger log)
	{
		this.log = log;
		log.push ("logger system is rdy", "Gyro");
	}

	void init()
	{
		Input.gyro.enabled = true;
		gyroscope = Input.gyro;
		gyroscope.enabled = true;

		//1~3 : buffered mouse position
		//4~6 : now mouse position
		mouse = new float[10];
		accel = new Vector3[3];
		int i;
		for (i = 4; i <= 6; i += 1) {
			get_mouse_pos (mouse, i);
			mouse [i + xyz] = set_mouse_pos (mouse, i);
			mouse[i-xyz] = set_mouse_pos (mouse, i - xyz);
		}
		for (i = 0; i < 3; i += 1) {
			accel [i] = new Vector3 ();
		}
		accel[2] = get_vec();
	}

	void get_mouse_pos(float[] pos,int target)
	{
		if (is_platform_window ()) {
			if (target % xyz == 1) {
				pos [target] = Input.mousePosition.x;
			} 
			else if (target % xyz == 2) {
				pos [target] = Input.mousePosition.y;
			} 
			else {
				pos [target] = 0;
			}
		} 
		else if (is_platform_tizen ()) {
			if (target % xyz == 1) 
			{
				pos [target] = (float)Input.acceleration.x;
			} 
			else if (target % xyz == 2) 
			{
				pos [target] = (float)Input.acceleration.y;
			} 
			else 
			{
				pos [target] = (float)Input.acceleration.z;
			}
		}	
		else 
		{
			if (target % xyz == 1) 
			{
				pos [target] = Input.gyro.rotationRateUnbiased.x;
			} 
			else if (target % xyz == 2) 
			{
				pos [target] = Input.gyro.rotationRateUnbiased.y;
			} 
			else 
			{
				pos [target] = Input.gyro.rotationRateUnbiased.z;
			}
		}
	}

	float set_mouse_pos(float[] src, int target)
	{
		return src [target];
	}

	public bool is_platform_window()
	{
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsWebPlayer) {
			return true;
		}
		return false;
	}

	bool is_platform_tizen()
	{
		if (Application.platform == RuntimePlatform.TizenPlayer)
			return true;
		return false;
	}
}
