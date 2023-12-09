using HellishHive2;
using UnityEngine;

namespace Views
{
    public class InjectActor : MonoBehaviour
    {
        private void Awake()
        {
            IActor actor = GetComponent<IActor>();

            IHaveActor[] needActor = GetComponentsInChildren<IHaveActor>(true);

            foreach (IHaveActor na in needActor)
            {
                na.Actor = actor;
            }
        }
    }
}