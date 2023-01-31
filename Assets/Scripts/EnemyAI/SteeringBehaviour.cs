using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    public abstract (float[] danger, float[] interests)
        GetSteering(float[] danger, float[] interest, AIData aiData);


}
