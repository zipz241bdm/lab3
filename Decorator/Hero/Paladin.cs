namespace Decorator.Hero
{
    public class Paladin : IHero
    {
        public string Name => "Паладін";
        public int Health => 120;
        public int Attack => 20;
        public int Defense => 25;
        public int MagicPower => 15;

        public string GetDescription() =>
            $"{Name} - HP:{Health}  ATK:{Attack}  DEF:{Defense}  MGK:{MagicPower}";
    }
}