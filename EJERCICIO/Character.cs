namespace EasterEgg
{
    public class Character
    {
        public string? Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public Character(string? name, int hp, int atac, int defense, int level = 0)
        {
            Name = name;
            Level = level;
            HP = hp;
            Attack = atac;
            Defense = defense;
        }
    }
}