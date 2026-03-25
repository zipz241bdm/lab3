namespace Decorator.Artifact
{
    public class BlessedShield : InventoryDecorator
    {
        public BlessedShield(IHero hero) : base(hero) { }

        public override string ItemName => "Благословенний щит";
        public override string ItemDescription => "+30 DEF, +10 HP";
        public override int Defense => _hero.Defense + 30;
        public override int Health => _hero.Health + 10;
    }
}