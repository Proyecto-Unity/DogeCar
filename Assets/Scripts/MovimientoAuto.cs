using UnityEngine;
using System.Collections;

public class MovimientoAuto : MonoBehaviour {
	public WheelCollider Llanta1, Llanta2, Llanta3, Llanta4;
	public Transform Neumatico1, Neumatico2;
	public int Aceleracion = 100; 
	public int desAceleracion = 80;
	public float Velocidad;
	public int VelocidadMaxima = 200;

	void Start () {
	
		transform.rigidbody.centerOfMass = new Vector3 (0, -1f, 0);

	}

	void Update () {
		Neumatico1.localEulerAngles = new Vector3 (0, -Llanta3.steerAngle*2, 0);
		Neumatico2.localEulerAngles = new Vector3 (0, -Llanta4.steerAngle*2, 0);

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

}
