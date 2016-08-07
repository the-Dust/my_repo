using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

public class Star
{
    public int x, y, vx, vy;
    
    public Star (int x, int y, int vx, int vy)
    {
        this.x = x;
        this.y = y;
        this.vx = vx;
        this.vy = vy;
    }
        public void move ()
    {
        x += vx;
        y += vy;
        if (x>=76 || x<=1) vx = -vx;
        if (y>=20 || y<=1) vy = -vy;
    }
        public void show ()
        {
            Console.SetCursorPosition(x, y);
            Console.Write("-=*=-");
        }
}

class Program
{
    static void Main()
    {
        Star star = new Star(10, 10, 1, 1);
        Star star2 = new Star(10, 10, 2, 1);
        do
        {
            star.move();
            star.show();
            star2.move();
            star2.show();
            System.Threading.Thread.Sleep(100);
            Console.Clear();

        }
        while (!Console.KeyAvailable);
        Console.ReadKey();

    }
}
