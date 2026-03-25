namespace Decorator.Artifact
{
    public class HealthAmulet : InventoryDecorator
    {
        public HealthAmulet(IHero hero) : base(hero) { }

        public override string ItemName => "Амулет здоров'я";
        public override string ItemDescription => "+30 HP";
        public override int Health => _hero.Health + 30;
    }
}