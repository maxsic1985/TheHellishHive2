using UnityStandardAssets.Characters.FirstPerson;

namespace Services
{
    public static class ControlsService
    {
        
        public static void LockControls(FirstPersonController _fps)
        {
            _fps.m_WalkSpeed = 0;
            _fps.m_RunSpeed = 0; //скорость хотьбы 0
            _fps.m_MouseLook.XSensitivity = 0;
            _fps.m_MouseLook.YSensitivity = 0;
            _fps.m_MouseLook.SetCursorLock(false);
         
        } 
        
        public static void UnLockControls(FirstPersonController _fps)
        {
            _fps.m_MouseLook.XSensitivity = 1.5f;
            _fps.m_MouseLook.YSensitivity = 1.5f;
            _fps.m_WalkSpeed = 2.0f;
            _fps.m_RunSpeed = 1.8f; //скорость хотьбы  норма
            _fps.m_MouseLook.SetCursorLock(true);
        }
    }
}