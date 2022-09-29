using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleApplication4
{

    //part 1
    public class Vector
    {
        double _x;
        double _y;
        double _z;

        public Vector(double x = 0, double y = 0, double z = 0)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public override string ToString()
        {
            return $"({_x}, {_y}, {_z})";
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1._x + v2._x, v1._y + v2._y, v1._z + v2._z);
        }
        public static Vector operator *(Vector v1, Vector v2)
        {
            return new Vector(v1._x * v2._x, v1._y * v2._y, v1._z * v2._z);
        }
        public static Vector operator *(Vector v1, double x)
        {
            return new Vector(v1._x * x, v1._y * x, v1._z * x);
        }

        public double Length()
        {
            return Math.Sqrt(_x * _x + _y * _y + _z * _z);
        }

        public static bool operator >(Vector v1, Vector v2)
        {
            return (v1.Length() > v2.Length());
        }
        public static bool operator <(Vector v1, Vector v2)
        {
            return (v1.Length() < v2.Length());
        }
    }

    //part 2
    public class Car : IEquatable<Car>
    {
        public string Name { get; set; }
        public string Engine { get; set; }
        public double MaxSpeed { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public string Info()
        {
            return Name + " " + Engine;
        }

        public bool Equals(Car? other)
        {
            return other.Name == this.Name;
        }
    }

    public class CarsCatalog
    {
        Car[] _cars;
        public CarsCatalog(Car[] cars) => _cars = cars;
        public string this[int index]
        {
            get => index.ToString() + " " + _cars[index].Info();
        }

        public int size() => _cars.Length;
    }

    //part 3

    class Currency
    {
        public float Value { get; set; }
    }

    class CurrencyUSD : Currency
    {
        float _toeur = 2;
        float _torub = 4;
        public float ToEUR { get => base.Value * _toeur; set => _toeur = value; }
        public float ToRUB { get => base.Value * _torub; set => _torub = value; }
        
        public CurrencyUSD(float value)
        {
            base.Value = value;
        }

        public override string ToString()
        {
            return base.Value.ToString();
        }

        public static implicit operator CurrencyUSD(CurrencyEUR obj)
        {
            return new CurrencyUSD(obj.ToUSD);
        }

        public static explicit operator CurrencyRUB(CurrencyUSD obj)
        {
            return new CurrencyRUB(obj.ToRUB);
        }
    }
    class CurrencyEUR : Currency
    {

        float _tousd = 0.5f;
        float _torub = 2;
        public float ToUSD { get => base.Value * _tousd; set => _tousd = value; }
        public float ToRUB { get => base.Value * _torub; set => _torub = value; }
        public CurrencyEUR(float value)
        {
            base.Value = value;
        }

        public override string ToString()
        {
            return base.Value.ToString();
        }

        public static implicit operator CurrencyEUR(CurrencyUSD obj)
        {
            return new CurrencyEUR(obj.ToEUR);
        }

        public static implicit operator CurrencyEUR(CurrencyRUB obj)
        {
            return new CurrencyUSD(obj.ToEUR);
        }
    }
    class CurrencyRUB : Currency
    {

        float _toeur = 0.5f;
        float _tousd = 0.25f;
        public float ToEUR { get => base.Value * _toeur; set => _toeur = value; }
        public float ToUSD { get => base.Value * _tousd; set => _tousd = value; }
        public CurrencyRUB(float value)
        {
            base.Value = value;
        }
        public override string ToString()
        {
            return base.Value.ToString();
        }

        public static implicit operator CurrencyRUB(CurrencyEUR obj)
        {
            return new CurrencyRUB(obj.ToRUB);
        }

        public static explicit operator CurrencyUSD(CurrencyRUB obj)
        {
            return new CurrencyUSD(obj.ToUSD);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //part 1
            Console.WriteLine("Part 1:");

            Vector v1 = new Vector(1, 2, 3);
            Vector v2 = new Vector(6, 3, 1);

            Console.WriteLine($"v1: {v1}");
            Console.WriteLine($"v2: {v2}");
            Console.WriteLine($"v1 + v2: {v1 + v2}");
            Console.WriteLine($"v1 * v2: {v1 * v2}");
            Console.WriteLine($"v1 * 2: {v1 * 2}");
            Console.WriteLine($"Lenght v1: {v1.Length()}");
            Console.WriteLine($"Lenght v2: {v2.Length()}");
            Console.WriteLine($"v1 > v2: {v1 > v2}");
            Console.WriteLine($"v1 < v2: {v1 < v2}");

            //part 2
            Console.WriteLine("\nPart 2:");
            Car c1 = new Car();
            c1.Name = "Name";
            c1.Engine = "Asus";
            c1.MaxSpeed = 600;
            Car c2 = new Car();
            c2.Name = "Name";
            c2.Engine = "Sisu";
            Car c3 = new Car();
            c3.Name = "New_Name";
            c3.Engine = "Ford";
            Console.WriteLine($"c1: {c1}");
            Console.WriteLine($"c2: {c2}");
            Console.WriteLine($"c3: {c3}");

            Console.WriteLine($"c1 = c2: {c1.Equals(c2)}");
            Console.WriteLine($"c1 = c3: {c1.Equals(c3)}");

            Car[] cars = { c1, c2, c3 };
            CarsCatalog catalog = new CarsCatalog(cars);
            Console.WriteLine("Catalog: ");
            for (int i = 0; i < catalog.size(); ++i)
            {
                Console.WriteLine(catalog[i]);
            }

            //part 3
            Console.WriteLine("\nPart 3:");
            CurrencyUSD us = new CurrencyUSD(10);
            CurrencyEUR eu = us;
            CurrencyRUB ru = (CurrencyRUB)us;
            Console.WriteLine($"usd: {us}");
            Console.WriteLine($"eur: {eu}");
            Console.WriteLine($"rub: {ru}");
            Console.Read();
        }
    }
}
