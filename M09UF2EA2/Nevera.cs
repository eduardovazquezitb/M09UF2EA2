

using System;
using System.Collections.Generic;

namespace M09UF2EA2
{
    public enum BirraType
    {
        Alhambra,
        Mil_Nueve,
        Guiness,
        Estrella_Galicia
    }

    public class NeveraIsFullException : Exception
    {

        public NeveraIsFullException(string message) : base(message) {}
    }

    public class MassaBirresException : Exception
    {
        public MassaBirresException(string message) : base(message) { }
    }

    public class NeverIsEmptyException : Exception
    {
        public NeverIsEmptyException(string message) : base(message) { }
    }

    public class NotEnoughBirresException : Exception
    {
        public NotEnoughBirresException(string message) : base(message) { }
    }

    class Nevera
    {
        private readonly List<BirraType> _birres;

        public List<BirraType> Birres { get { return _birres; } }
        private readonly int _capacitat;
        public int Capacitat { get { return _capacitat; } }
        private readonly Random random;

        public Nevera(int capacitat)
        {
            _capacitat = capacitat;
            _birres = new List<BirraType>();
            random = new Random();
        }

        void FicarBirraRandom()
        {
            if(_birres.Count < _capacitat)
            {
                int rand = random.Next(4);
                _birres.Add((BirraType)rand);
            }
            else
            {
                throw new NeveraIsFullException("La nevera està plena!");
            }
        }

        void FicarBirresRandom(int quantes)
        {
            if (quantes + _birres.Count <= _capacitat)
                for (int i = 0; i < quantes; i++)
                    FicarBirraRandom();
            else
                throw new MassaBirresException("No hi caben tantes cerveses!");
        }

        void BeureCervesaRandom(string personName)
        {
            if (_birres.Count > 0)
            {
                int quina = random.Next(0, _birres.Count);
                Console.WriteLine(personName + " es beu una " + _birres[quina].ToString() + ".");
                _birres.RemoveAt(quina);
            }
            else
                throw new NeverIsEmptyException("No queden cerveses!");
        }

        void BeureCervesesRandom(int quantes, string personName)
        {
            if (_birres.Count >= quantes)
                for (int i = 0; i < quantes; i++)
                    BeureCervesaRandom(personName);
            else
                throw new NotEnoughBirresException("No hi ha prou cerveses!");
        }

        public void OmplirNevera(string personName)
        {
            int quantes = random.Next(0, 7);
            try
            { 
                FicarBirresRandom(quantes);
                Console.WriteLine(personName + " ha ficat un total de " + quantes + " cerves" + (quantes == 1 ? "a" : "es") + ".");
            }
            catch(Exception e) {}
        }

        public void BeureCervesa(string personName)
        {
            int quantes = random.Next(0, 7);
            if (quantes > _birres.Count)
                quantes = _birres.Count;
            try
            {
                BeureCervesesRandom(quantes, personName);                
            }
            catch(Exception e) { }
        }
    }
}
