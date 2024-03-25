using System.Xml;
using System.Xml.Linq;

namespace EasterEgg
{
    public static class XMLHelper
    {
        public static void CreateXML(Character character) /*Crear un XML amb un personatge*/
        {
            XDocument xmlDoc = new XDocument(
                new XElement("characters",
                 new XElement("character",
                    new XAttribute("name", character.Name),
                    new XElement("level", character.Level),
                    new XElement("hp", character.HP),
                    new XElement("attack", character.Attack),
                    new XElement("defense", character.Defense)
                )));
            string xmlFilePath = "characters.xml";
            xmlDoc.Save(xmlFilePath);
        }

        public static void AddCharacter(Character character) /*Afegir un personatge a un XML*/
        {
            XDocument xmlDoc = XDocument.Load("characters.xml");

            if (xmlDoc.Element("characters") == null)
            {
                xmlDoc.Add(new XElement("characters"));
            }

            xmlDoc.Element("characters").Add( 
                new XElement("character",
                new XAttribute("name", character.Name),
                new XElement("level", character.Level),
                new XElement("hp", character.HP),
                new XElement("attack", character.Attack),
                new XElement("defense", character.Defense)));
            xmlDoc.Save("characters.xml");
        }
        public static List<Character> ReadAllCharacters() /*Llegir tots els personatges d'un XML*/
        {
            string xmlFilePath = "characters.xml";
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var characters = from character in xmlDoc.Descendants("character")
                                         select new Character((string)character.Attribute("name"), 
                                         (int)character.Element("hp"), 
                                         (int)character.Element("attack"),
                                         (int)character.Element("defense"),
                                         (int)character.Element("level"));
            return characters.ToList();
        }

        public static Character GetCharacter(string name) /*Obtenir un personatge d'un XML*/
        {
            List<Character> characters = ReadAllCharacters();
            return characters.FirstOrDefault(character => character.Name == name);
        }

        public static Character GetAleatoryCharacter() /*Obtenir un personatge aleatori d'un XML*/
        {
            List<Character> characters = ReadAllCharacters();
            Random random = new Random();
            return characters[random.Next(characters.Count)];
        }

        public static void ReadAndPrintXML() /*Llegir i imprimir tots els personatges d'un XML*/
        {
            string xmlFilePath = "characters.xml";
            XDocument xmlDoc = XDocument.Load(xmlFilePath);

            var characters = from character in xmlDoc.Descendants("character")
                             select new
                             {
                                 Name = (string)character.Attribute("name"),
                                 Level = (int)character.Element("level"),
                                 HP = (int)character.Element("hp"),
                                 Attack = (int)character.Element("attack"),
                                 Defense = (int)character.Element("defense")

                             };

            foreach (var character in characters)
            {
                Console.WriteLine($"Nom: {character.Name}");
                Console.WriteLine($"Nivell: {character.Level}");
                Console.WriteLine($"Punts de Vida: {character.HP}");
                Console.WriteLine($"Atac: {character.Attack}");
                Console.WriteLine($"Defensa: {character.Defense}");
                Console.WriteLine();
            }
        }

        public static void UpdateXML(Character characterToEdit) /*Actualitzar un personatge d'un XML*/
        {
            string xmlFilePath = "characters.xml";
            List<Character> characters = ReadAllCharacters();
            foreach (Character character in characters)
            {
                if (character.Name == characterToEdit.Name)
                {
                    character.Level = characterToEdit.Level;
                    character.HP = characterToEdit.HP;
                    character.Attack = characterToEdit.Attack;
                    character.Defense = characterToEdit.Defense;
                }
            }

            XDocument xmlDoc = new XDocument(
                      new XElement("characters",
                      from character in characters
                      select new XElement("character",
                      new XAttribute("name", character.Name),
                      new XElement("level", character.Level),
                      new XElement("hp", character.HP),
                      new XElement("attack", character.Attack),
                      new XElement("defense", character.Defense))));
            xmlDoc.Save(xmlFilePath);
        }
        public static void Fight(Character usercharacter) /*Lluitar contra un personatge aleatori d'un XML*/
        {
            Character enemy = GetAleatoryCharacter();
            Console.WriteLine("Enemic:");
            Console.WriteLine($"Nom: {enemy.Name}");
            Console.WriteLine($"Nivell: {enemy.Level}");
            Console.WriteLine($"Punts de Vida: {enemy.HP}");
            Console.WriteLine($"Atac: {enemy.Attack}");
            Console.WriteLine($"Defensa: {enemy.Defense}");
            Console.WriteLine();
            
            int usertmphp = usercharacter.HP;
            int enemytmphp = enemy.HP;
            int userDamage = usercharacter.Attack - enemy.Defense == 0 ? 1 : usercharacter.Attack - enemy.Defense;
            int enemyDamage = enemy.Attack - usercharacter.Defense == 0 ? 1 : enemy.Attack - usercharacter.Defense;

            while (usertmphp > 0 && enemytmphp > 0)
            {
                Console.WriteLine("El enemic t'ataca i et fa " + enemyDamage + " de dany");
                usertmphp -= enemyDamage;
                Console.WriteLine("Et queden " + usertmphp + " punts de vida");

                Console.WriteLine("Atacas al enemic i li fas " + userDamage + " de dany");
                enemytmphp -= userDamage;
                Console.WriteLine("Al enemic li queden " + enemytmphp + " punts de vida");
                Console.ReadKey();
                Console.WriteLine();
            }

            if (usertmphp <= 0 && enemytmphp <= 0)
            {
                Console.WriteLine("Empat");
            }
            else if (usertmphp <= 0)
            {
                Console.WriteLine("Has perdut");
            }
            else
            {
                Console.WriteLine("Has guanyat");
            }

        }
    }
}