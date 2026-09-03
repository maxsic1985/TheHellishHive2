using System;

namespace Services
{
    public static class SkillServices
        {
            public static bool GetSkilСhance()
            {
                var shansPercent = new Random().Next(0, 100);
                return shansPercent >= 50 ? true : false;
            }
        }
    
}