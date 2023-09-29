using UnityEngine;


[CreateAssetMenu(fileName = "NewData", menuName = "Custom/MyScriptableObject")]
public class TaskVariables : ScriptableObject
{
    [Header("AnomalySpawner")]
    public float spawnerDifficultyIncrease1;
    public float spawnerDifficultyIncrease2;

    [Header("SwitchTask")]
    public float maxTime; //The time allocated for all inputs to be inputted correctly before restarting
    public float difficulty1Time; //The amount of game time passed before the task becomes harder
    public int switchScore; //The amount of score rewarded once completing this task

    [Header("PlugTask")]
    public int plugScore; //The amount of score rewarded once completing this task

    [Header("WireTask")]
    public int wireScore;
}
