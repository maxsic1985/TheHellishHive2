using LeopotamGroup.Globals;

namespace HellishHive2
{
    public class WrenchActor : Actor
    {
        private readonly IPoolService _poolService;

        public WrenchActor()
        {
            _poolService = Service<IPoolService>.Get();
        }
        public override void Handle()
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            _poolService.Return(gameObject);
        }
    }
}