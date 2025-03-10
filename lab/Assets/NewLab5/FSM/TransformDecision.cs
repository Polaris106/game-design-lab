
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "PluggableSM/Decisions/Transform")]
public class TransformDecision : Decision
{
    public StateTransformMap[] map;

    public override bool Decide(StateController controller)
    {
        MarioStateController m = (MarioStateController)controller;
        // we assume that the state is named (string matched) after one of possible values in MarioState
        // convert between current state name into MarioState enum value using custom class EnumExtension
        // you are free to modify this to your own use
        MarioState toCompareState = EnumExtension.ParseEnum<MarioState>(m.currentState.name);
        Debug.Log("Current state is " + toCompareState);
        Debug.Log("Current PowerupType is " + m.currentPowerupType);
        // loop through state transform and see if it matches the current transformation we are looking for
        for (int i = 0; i < map.Length; i++)
        {
            if (toCompareState == map[i].fromState && m.currentPowerupType == map[i].powerupCollected)
            {
                Debug.Log("Transforming from " + toCompareState + " due to " + map[i].powerupCollected);
                return true;
            }

            else {
                //Debug.Log("toCompareState is " + map[i].fromState + ", map[i].fromState " + map[i].fromState + ", m.currentPowerupType is " + m.currentPowerupType + ", powerupCollected is " + map[i].powerupCollected);
            } 
        }

        return false;

    }
}

[System.Serializable]
public struct StateTransformMap
{
    public MarioState fromState;
    public PowerupType powerupCollected;
}
