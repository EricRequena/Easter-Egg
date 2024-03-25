using System;
using System.Xml.Linq;
namespace EasterEgg
{
    public class Program
    {
        public static void Main()
        {
            const string Menu = "Què vols fer?\n1. Crear un personatge.\n2. Editar un personatge existent.\n3. Lluitar.\n4. Veure informació dels personatges.\n0. Sortir\n";
            const string MSGName = "Nom: ";
            const string MSGLevel = "Nivell: ";
            const string MSGLife = "Punts de vida: ";
            const string MSGAttack = "Atac: ";
            const string MSGDefense = "Defensa: ";

            string name;
            int level, life, attack, defense, option;



            do
            {
                Console.WriteLine(Menu);
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 0:
                        Console.WriteLine("Chaito <3");
                        break;
                    case 1:
                        Console.WriteLine(MSGName);
                        name = Console.ReadLine();
                        Console.WriteLine(MSGLevel);
                        level = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(MSGLife);
                        life = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(MSGAttack);
                        attack = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(MSGDefense);
                        defense = Convert.ToInt32(Console.ReadLine());
                        Character character = new Character(name, life, attack, defense, level);
                        try /*Intentem llegir el fitxer, si no existeix el creem*/
                        {
                            XDocument.Load("characters.xml");
                            XMLHelper.AddCharacter(character);
                        } catch (Exception e)
                        {
                            XMLHelper.CreateXML(character);
                        }
                        Console.Clear();
                        break;
                    case 2:
                        Console.WriteLine("Quin personatge vols editar?");
                        name = Console.ReadLine();
                        Console.WriteLine("Quin camp vols canviar?");
                        Console.WriteLine("Opcions:\nLevel\nHp\nAttack\nDefense");
                        string field = Console.ReadLine();
                        Console.WriteLine("Per quin valor?");
                        int value = Convert.ToInt32(Console.ReadLine());
                        Character characterToEdit = XMLHelper.GetCharacter(name);
                        switch (field.ToLower())
                        {
                            case "level":
                                characterToEdit.Level = value;
                                break;
                            case "hp":
                                characterToEdit.HP = value;
                                break;
                            case "attack":
                                characterToEdit.Attack = value;
                                break;
                            case "defense":
                                characterToEdit.Defense = value;
                                break;
                            default:
                                Console.WriteLine("No existeix aquest camp, no es modificara res");
                                break;
                        }
                        XMLHelper.UpdateXML(characterToEdit);

                        break;
                    case 3:
                        Console.WriteLine("Amb quin personatge vols lluitar?");
                        name = Console.ReadLine();
                        Character userCharacter = XMLHelper.GetCharacter(name);
                        XMLHelper.Fight(userCharacter);
                        Console.Clear();
                        break;
                    case 4:
                        XMLHelper.ReadAndPrintXML();
                        Console.ReadKey();
                        Console.Clear();

                        break;
                    default: break;
                }
            } while (option != 0);



        }
    }
}