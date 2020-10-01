using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verketetelisten
{
    class Program
    {
        static void Main(string[] args)
        {
            Pup pup = null;
            while (true)
            {
                Console.WriteLine("Auswahlmenue Verkettete Liste");
                Console.WriteLine("------------------");
                Console.WriteLine("1 = Daten anzeigen");
                Console.WriteLine("2 = Neu am ende");
                Console.WriteLine("3 = Neu nach element");
                Console.WriteLine("4 = Datensatz löschen");
                Console.WriteLine("5 = Komplet löchen");
                Console.WriteLine("6 = Liste sort name");
                Console.WriteLine("7 = Liste in Datei Speichern");
                Console.WriteLine("8 = Liste aus Datei Laden");
                Console.WriteLine("9 = Bildschirm lerren");
                Console.WriteLine("10 = Testdaten generieren");
                Console.WriteLine("0 = Ende");
                Console.WriteLine("-----------------");
                Console.WriteLine("auswahl:");
                int input = -1;
                while (!int.TryParse(Console.ReadLine(), out input)) ;
                switch (input)
                {
                    case 1:
                        showList(pup);
                        break;
                    case 2:
                        Pup newElement = addEnd(ref pup);
                        Console.WriteLine("Bitte geben sie eine Nummer fuer das neuangelegte Object ein:");
                        while (!int.TryParse(Console.ReadLine(), out input)) ;
                        newElement.num = input;
                        Console.WriteLine("Bitte geben sie einen Namen für das Neue Object ein:");
                        newElement.name = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Nach welcher nummer soll das neue Element eingefügt werden");
                        while (!int.TryParse(Console.ReadLine(), out input)) ;
                        Pup curr = addBeforeElement(ref pup, searchByNum(pup, input));
                        if (curr == null)
                            Console.WriteLine("Fehler beim erzeugen des Objectes");
                        else
                            Console.WriteLine("Bitte geben sie eine Nummer fuer das neuangelegte Object ein:");
                        while (!int.TryParse(Console.ReadLine(), out input)) ;
                        curr.num = input;
                        Console.WriteLine("Bitte geben sie einen Namen für das Neue Object ein:");
                        curr.name = Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("Welcher Datensatz soll gelöscht werden(nummer)");
                        while (!int.TryParse(Console.ReadLine(), out input)) ;
                        deleteElement(ref pup, searchByNum(pup, input));
                        break;
                    case 5:
                        pup = null;
                        break;
                    case 6:
                        sortByName(ref pup);
                        break;
                    case 7:
                        Console.WriteLine("Diese Funktion ist bisher nicht erstellt");
                        break;
                    case 8:
                        Console.WriteLine("Diese Funktion ist bisher nicht erstellt");
                        break;
                    case 9:
                        Console.Clear();
                        break;
                    case 10:
                        Pup p1 = new Pup();
                        Pup p2 = new Pup();
                        Pup p3 = new Pup();
                        Pup p4 = new Pup();
                        p1.num = 1;
                        p2.num = 2;
                        p3.num = 3;
                        p4.num = 4;
                        p1.name = "1";
                        p2.name = "2";
                        p3.name = "3";
                        p4.name = "4";
                        pup = p1;
                        p1.next = p2;
                        p2.next = p3;
                        p3.next = p4;
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Fehlerhafte eingabe");
                        break;
                }
            }
        }

        public class Pup
        {
            public int num;
            public string name;

            public Pup next = null;
        }

        static void switchRef(ref Pup p0, ref Pup p1, ref Pup p2)
        {
            Pup temp = p1;

            p1 = p1.next;
            temp.next = p1.next;
            p1.next = temp;
            p0 = p1;
        }

        static void sortByName(ref Pup start)
        {
            Pup curr = start;
            Pup temp;
            if (start.num < start.next.num)
            {
                temp = start;
                start = start.next;
                temp.next = start.next;
                start.next = temp;
            }
            if (curr.next != null || curr.next.next != null)
            {
                while (curr.next != null || curr.next.next != null)
                {
                    //Anker umhängen + Tauschen der Erstenbeiden Objekte
                    if (start.num < start.next.num)
                    {
                        temp = start;
                        start = start.next;
                        temp.next = start.next;
                        start.next = temp;
                    }
                    if (curr.next != null)
                        if (curr.next.next != null && curr.next.num < curr.next.next.num)
                        {
                            switchRef(ref curr, ref curr.next, ref curr.next.next);
                            curr = start;
                        }
                        else
                            if (curr.next.next != null)
                            curr = curr.next;
                        else
                            break;

                }
            }

        }

        static Pup addEnd(ref Pup start)
        {
            if (start == null)
            {
                start = new Pup();
                return start;
            }
            else
            {
                Pup curr = findLast(start);
                curr.next = new Pup();
                return curr.next;
            }
        }
        static Pup findLast(Pup start)
        {
            while (start.next != null)
                start = start.next;
            return start;
        }
        static Pup addBeforeElement(ref Pup start, Pup createAfter)
        {
            Pup newElement;
            if (createAfter == null || start == null)
            {
                newElement = addEnd(ref start);
                return newElement;
            }
            newElement = new Pup();
            Pup curr = start;
            while (curr.next != null && curr.next != createAfter)
            {
                curr = curr.next;
            }

            newElement.next = curr.next;
            curr.next = newElement;
            return newElement;
        }
        static void deleteElement(ref Pup start, Pup toDel)
        {
            if (start == toDel)
            {
                start = start.next;
                return;
            }
            Pup curr = start;
            while (curr.next != null)
            {
                if (curr.next == toDel)
                {
                    curr.next = toDel.next;
                    return;
                }
                else
                    curr = curr.next;
            }
            return;
        }
        static Pup searchByNum(Pup start, int snum)
        {
            while (start != null)
                if (start.num == snum)
                    return start;
                else
                    start = start.next;
            return null;
        }
        static void showList(Pup start)
        {
            if (start == null)
                return;
            Console.WriteLine("----");
            while (start != null)
            {
                Console.WriteLine(start.num + " " + start.name);
                start = start.next;
            }
            Console.WriteLine("----");
        }
    }


}
