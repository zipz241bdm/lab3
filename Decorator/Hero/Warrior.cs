namespace Decorator.Hero
{
    public class Warrior : IHero
    {
        public string Name => "Воїн";
        public int Health => 150;
        public int Attack => 30;
        public int Defense => 20;
        public int MagicPower => 0;

        public string GetDescription() =>
            $"{Name} - HP:{Health}  ATK:{Attack}  DEF:{Defense}  MGK:{MagicPower}";
    }
}