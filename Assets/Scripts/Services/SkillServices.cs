using System;
using UnityEngine;
namespace Services
{
    public static class SkillServices
        {
            public static bool GetSkilСhance(int shans)
            {
                var shansPercent = new System.Random().Next(0, 100);
                Debug.Log("stunn"+shansPercent);
                return shansPercent >= shans ? true : false;
            }
        }
    
}