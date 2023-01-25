using Hero;
using UI;

namespace Data.PayLoads
{
    public struct GameWorldPayload
    {
        public HeroRoot Hero;
        public Hud Hud;
        public Shields.Shield Shield;
    }
}