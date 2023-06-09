﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>StageForce</c> adds force to the object. The movement and rotation models
/// are separated so that the force works properly. Movement is given to self and
/// rotation is given to the child which is the transform "Model" field.
/// </summary>
public class StageForce : MonoBehaviour
{
    [Header("Force Properties")]
    [Tooltip("The model to rotate")]
    public Transform Model; // The model of the force object
                            // that will be rotated
    public float Gravity; // The downward speed of the force

    [Tooltip("The direction of the force in x and z axis. " +
             "Vec2.x = x direction, Vec2.y = z direction.")]
    public Vector2 Direction; // The direction of the force
                              // in x and z axis

    public Vector3 RotationSpeed; // The rotational speed
                                  // and rotational direction
                                  // of the force

    private Vector3 _originalPosition; // Storing the original
                                       // position of the
                                       // breakable stage

    private Quaternion _originalRotation; // Storing the original
                                          // rotation of the
                                          // breakable stage

    private float _fps; // Storing the Time.deltaTime
    
    private bool _isActivated = false; // Flag for activating
                                       // the force

    // This flag checks if the stage object has reached the
    // fall limit
    private bool _hasReachedFallLimit
    { get { return transform.position.y < 
                   GameData.Instance.FallHeightLimit; } }

    // Start is called before the first frame update
    void Start()
    {
        /* Hint: If reset does not work then store the original
         *       rotation and position at start and the use the
         *       original values to replace the current values
         *       in the ResetForce() method.
         */

        // Storing the original position
        _originalPosition = transform.localPosition;

        // Storing the original rotation
        _originalRotation = Model.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Condition to activate force
        if (_isActivated && !_hasReachedFallLimit)
        {
            _fps = Time.deltaTime * GameData.Instance.SimulationSpeed;

            // Moving the stage
            transform.Translate(Direction.x * _fps, 
                                Gravity * _fps, 
                                Direction.y * _fps);

            // Rotating the stage
            Model.Rotate(RotationSpeed * _fps);
        }
    }

    /// <summary>
    /// This method activates the force
    /// </summary>
    public void ActivateForce() { _isActivated = true; }

    /// <summary>
    /// This method resets the force stage.
    /// </summary>
    public void ResetForce()
    {
        _isActivated = false; // Deactivating force

        // Resetting position
        transform.localPosition = _originalPosition;

        // Resetting model rotation
        Model.localRotation = _originalRotation;
    }
}
