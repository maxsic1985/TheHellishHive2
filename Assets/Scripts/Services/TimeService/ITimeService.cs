﻿using System;



namespace HellishHive2
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        float InGameTime { get; }
        DateTime UtcNow { get; }

        void Pause();
        void Resume();
        
    }
}