using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspinhosManager : MonoBehaviour {

    private SliderJoint2D slider;
    private JointMotor2D motor;

	void Start () {

        slider = GetComponent<SliderJoint2D>();
        motor = slider.motor;

	}
	
	
	void Update () {

        if (slider.limitState == JointLimitState2D.UpperLimit) {

            motor.motorSpeed = Random.Range(-1, -5);
            slider.motor = motor;
        }

        if (slider.limitState == JointLimitState2D.LowerLimit) {

            motor.motorSpeed = Random.Range(1, 5);
            slider.motor = motor;
        }

	}
}
