using System;

namespace Controller.Maths
{
    public struct Vector2
    {
        int x;
        int y;
        public  Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            a.x = a.x + b.x;
            a.y = a.y + b.y;
            return a;
        }
    }
}