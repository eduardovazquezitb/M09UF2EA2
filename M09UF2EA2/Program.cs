using System;
using System.Collections.Generic;
using System.Threading;

namespace M09UF2EA2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Exercici1();
            Exercici2();
        }

        static void Exercici2()
        {
            Nevera nevera = new Nevera(9);
            Action[] actions = new Action[]
            {
                () => nevera.OmplirNevera("Anitta"),
                () => nevera.BeureCervesa("Bad Bunny"),
                () => nevera.BeureCervesa("Lil Nas X"),
                () => nevera.BeureCervesa("Manuel Turizo")
            };
            List<Thread> threads = new List<Thread>();
            int currentThread = 0;
            foreach (var action in actions)
            {
                int current = currentThread++;
                Thread thread = new Thread(() =>
                {
                    if (current != 0)
                        threads[current - 1].Join();
                    action();
                });
                threads.Add(thread);
                thread.Start();
            }
        }

        static void Exercici1()
        {
            string[] cadenes = new[]{
                "Una vegada hi havia un gat...",
                "En un lugar de la Mancha...",
                "Once upon a time in the west..."
            };
            List<Thread> threads = new List<Thread>();
            int currentThread = 0;
            foreach (var cadena in cadenes)
            {
                int current = currentThread++;
                Thread thread = new Thread(() =>
                {
                    if (current != 0)
                        threads[current - 1].Join();
                    WriteSentence(cadena, 1);
                });
                threads.Add(thread);
                thread.Start();
            }

            WaitForThreads(threads);
        }

        static void WriteSentence(string cadena, int timeBetweenWords)
        {
            string[] cadenaSplit = cadena.Split(" ");
            for (int i = 0; i < cadenaSplit.Length; i++)
            {
                if (i != 0)
                {
                    Console.Write(" ");
                    Thread.Sleep(timeBetweenWords * 1000);
                }
                Console.Write(cadenaSplit[i]);
            }
            Console.Write("\n");
        }

        static void WaitForThreads(List<Thread> threads)
        {
            bool haAcabat = false;
            Thread finishingThread = new Thread(() =>
            {
                foreach (var thread in threads)
                    thread.Join();
                haAcabat = true;
            });
            finishingThread.Start();

            while (!haAcabat) ;
        }
    }
}
