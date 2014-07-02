using UnityEngine;
using System.Collections;

public class Movimiento_Auto : MonoBehaviour {

	public WheelCollider Llanta1, Llanta2, Llanta3, Llanta4;
	public Transform Neumatico1, Neumatico2, Neumatico3, Neumatico4;
	public int Aceleracion = 25; 
	public int desAceleracion = 50;
	public float Velocidad;
	public int VelocidadMaxima = 220;
	public Texture Velocimetro;
	public Texture Aguja;
	int cuentaAngulo = 0;
	
	void Start () {
		transform.rigidbody.centerOfMass = new Vector3 (0, -1f, 0);
	}
	
	void Update () {
		Neumatico1.localEulerAngles = new Vector3 ((cuentaAngulo * Llanta1.rpm / 60 * 360 * Time.deltaTime), -Llanta3.steerAngle*2, 0);
		Neumatico2.localEulerAngles = new Vector3 ((cuentaAngulo * Llanta2.rpm / 60 * 360 * Time.deltaTime), -Llanta4.steerAngle*2, 0);
		cuentaAngulo++;
		if (cuentaAngulo > 360) {
			cuentaAngulo = 0;
		}
		
		//Girar Neumativos
		Neumatico3.Rotate (new Vector3 (Llanta3.rpm / 60 * 360 * Time.deltaTime, 0, 0));
		Neumatico4.Rotate (new Vector3 (Llanta4.rpm / 60 * 360 * Time.deltaTime, 0, 0));
		
		Velocidad = (2 * Mathf.PI * Llanta1.radius) * Llanta1.rpm * 60 / 1000;
		Velocidad = Mathf.Round (Velocidad);
		
	}
	
	void FixedUpdate () {
		if (Mathf.Abs(Velocidad) < VelocidadMaxima) {
			//Aceleracion del Auto
			Llanta1.motorTorque = Aceleracion * NuevosAxis ("Vertical");
			Llanta2.motorTorque = Aceleracion * NuevosAxis ("Vertical");
		} else {
			Llanta1.motorTorque = 0;
			Llanta2.motorTorque = 0;
		}
		if (NuevosAxis ("Vertical") == 0) {
			Llanta1.brakeTorque = desAceleracion;
			Llanta2.brakeTorque = desAceleracion;
		} else {
			Llanta1.brakeTorque = 0;
			Llanta2.brakeTorque = 0;
		}
		//Rotacion del Auto
		Llanta3.steerAngle = -10 * NuevosAxis ("Horizontal");
		Llanta4.steerAngle = -10 * NuevosAxis ("Horizontal");
	}
	
	
	float NuevosAxis(string Direccion){
		return (Input.GetAxis (Direccion));
	}
	
	void OnGUI(){
		GUI.DrawTexture (new Rect (Screen.width-200, Screen.height -145, 200, 200), Velocimetro);
		float Angulo = (Mathf.Abs(Velocidad) * 320 / 320 )-30;
		GUIUtility.RotateAroundPivot (Angulo, new Vector2(Screen.width -102,Screen.height -50));
		GUI.DrawTexture (new Rect (Screen.width-202, Screen.height -150, 200, 200), Aguja);
	}

}
