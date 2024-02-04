using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] SplineComputer _SplineComputer;

    public SplineComputer SplineComputer => _SplineComputer;
}
