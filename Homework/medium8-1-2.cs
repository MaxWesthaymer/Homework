using System;
using System.Collections.Generic;

namespace Task
{
    class Object
    {
        public Object(int x, int y, bool isAlive)
        {
            X = x;
            Y = y;
            IsAlive = isAlive;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public bool IsAlive { get; set; }
    }
    class Program
    {
        public static void Main()
        {
            Object[] objects = { new Object(5, 5, true),
                new Object(10, 10, true), 
                new Object(15, 15, true)
            };

            Random random = new Random();

            while (true)
            {
                for (int current = 0; current < objects.Length; current++)
                {
                    for (int next = 0; next < objects.Length; next++)
                    {
                        if (next <= current)
                        {
                            continue;
                        }
                        if (objects[current].X == objects[next].X && objects[current].Y == objects[next].Y)
                        {
                            objects[current].IsAlive = false;
                            objects[next].IsAlive = false;
                        }
                    }
                    objects[current].X += random.Next(-1, 1);
                    objects[current].Y += random.Next(-1, 1);

                    if (objects[current].X < 0)
                        objects[current].X = 0;

                    if (objects[current].Y < 0)
                        objects[current].Y = 0;
                    
                    if (objects[current].IsAlive)
                    {
                        Console.SetCursorPosition(objects[current].X, objects[current].Y);
                        Console.Write($"{current + 1}");
                    }
                }
            }
        }
    }
}