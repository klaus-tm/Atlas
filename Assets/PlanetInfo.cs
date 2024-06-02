// PlanetInfo.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetInfo", menuName = "ScriptableObjects/PlanetInfo", order = 1)]
public class PlanetInfo : ScriptableObject
{
    public string planetName;
    public string funFact;
}
