namespace Decorator.Artifact
{
    public class ArcaneOrb : InventoryDecorator
    {
        public ArcaneOrb(IHero hero) : base(hero) { }

        public override string ItemName => "Таємнича сфера";
        public override string ItemDescription => "+25 MGK, +10 HP";
        public override int MagicPower => _hero.MagicPower + 25;
        public override int Health => _hero.Health + 10;
    }
}