﻿using UnityEngine;

namespace HellishHive2
{
    public struct SoundMusicSourceComponent
    {
        public AudioSource Source;
        public AudioClip[] Tracks;
        public int PlayedTrack;
        
        public  int FirstTrackNumber;
    }
}