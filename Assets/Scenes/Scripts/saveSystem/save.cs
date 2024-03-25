using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the file that contains all the variables that will be serialized and saved to a file
/// Can be considered as a save file structure or format.
/// </summary>

[System.Serializable]

public class Save
{
    public List<Vector3> zombiePos = new List<Vector3>();
    public List<Vector3> AmmoPos = new List<Vector3>();
    public List<Vector3> HealthPackPos = new List<Vector3>();
    public List<Vector3> AmmoBasePos = new List<Vector3>();
    public List<Vector3> SpeedUpBasePos = new List<Vector3>();
    public List<Vector3> AmmoPlusBasePos = new List<Vector3>();
    public List<Vector3> NormalBasePos = new List<Vector3>();
    public Vector3 playerPos = new Vector3();
    public int score = 0;
    public int bulletNum = 0;
    public int HP = 0;
    public int SP = 0;
    public int existingEnemy = 0;
}
