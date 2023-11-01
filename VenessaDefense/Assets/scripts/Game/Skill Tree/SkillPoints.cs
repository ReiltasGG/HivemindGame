using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPoints : MonoBehaviour
{
    private int NumberOfSkillPoints = 0;

    public int GetSkillPoints() { return NumberOfSkillPoints; }
    public void SpendSkillPoints(int skillPointsSpent) {  NumberOfSkillPoints -= skillPointsSpent; }
    public void GainSkillPoints(int skillPointsGained) { NumberOfSkillPoints += skillPointsGained; }
}
