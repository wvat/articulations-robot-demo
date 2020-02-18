﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDirection { None = 0, Positive = 1, Negative = -1 };

public class ArticulationJointController : MonoBehaviour
{
    public RotationDirection rotationState = RotationDirection.None;
    public float speed = 500.0f;

    private ArticulationBody articulation;


    // LIFE CYCLE

    void Start()
    {
        articulation = GetComponent<ArticulationBody>();
    }

    void FixedUpdate() 
    {
        if (rotationState != RotationDirection.None) {
            float rotationChange = (float)rotationState * speed * Time.fixedDeltaTime;
            float rotationGoal = CurrentPrimaryAxisRotation() + rotationChange;
            RotateTo(rotationGoal);
        }


    }

    // CONTROL

    public void SetArticulationToEnabled(bool enabled)
    {
        articulation.enabled = enabled;
    }

    public void ForceToRotation(float rotation)
    {
        // set target
        RotateTo(rotation);
        /*
        // force position
        float rotationRads = Mathf.Deg2Rad * rotation;
        ArticulationReducedSpace newPosition = new ArticulationReducedSpace(rotationRads);
        articulation.jointPosition = newPosition;

        // force velocity to zero
        ArticulationReducedSpace newVelocity = new ArticulationReducedSpace(0.0f);
        articulation.jointVelocity = newVelocity;
        */
    }


    // MOVEMENT HELPERS

    float CurrentPrimaryAxisRotation()
    {
        /*
        float currentRotationRads = articulation.jointPosition[0];
        float currentRotation = Mathf.Rad2Deg * currentRotationRads;
        
        //Debug.Log("target: " + articulation.xDrive.target.ToString("F1") + ", current: " + currentRotation.ToString("F1"));

        return currentRotation;
        */
        return articulation.xDrive.target;
    }

    void RotateTo(float primaryAxisRotation)
    {
        var drive = articulation.xDrive;
        drive.target = primaryAxisRotation;
        articulation.xDrive = drive;
    }




}
