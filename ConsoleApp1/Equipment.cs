using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Equipment
    {
        string name;  //이름
        int ability;  //능력치
        string kind;  //종류
        bool iswear;  //착용 여부
        string description; //설명
        int price;
        bool isBuy;
        public Equipment(string name, int ab, string kind, bool wear, string de, int price, bool isBuy)
        {
            this.name = name;
            this.ability = ab;
            this.kind = kind;
            this.iswear = wear;
            this.description = de;
            this.price = price;
            this.isBuy = isBuy;
        }
        public void PrintStorage(int num, bool check)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write("- ");
            if (check)
            {
                Console.Write(num);
            }
            Console.ResetColor();
            if (iswear)
            {
                Console.Write($"[E]{name,-10}");
            }
            else
            {
                Console.Write($"{name,-10}");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" | ");
            Console.ResetColor();
            if (kind == "무기")
            {
                Console.Write("공격력 ");
            }
            else
            {
                Console.Write("방어력 ");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"+{ability,-2}");
            Console.Write(" | ");
            Console.ResetColor();
            Console.Write(description);
        }
        public void PrintStore(int num, bool check)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintStorage(num, check);
            if (isBuy)
            {
                Console.ResetColor();
                Console.Write("구매완료");
            }
            else
            {
                Console.Write(price);
                Console.ResetColor();
                Console.Write(" G");
            }

        }
        public void PrintSale(int num, bool check)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintStorage(num, check);
            Console.Write(price * 0.85f);
            Console.ResetColor();
            Console.Write(" G");
        }
        public bool IsBuyCheck()
        {
            return isBuy;
        }
        public void IsBuyController()
        {
            isBuy = !isBuy;
        }
        public void IsWearController()
        {
            iswear = !iswear;
        }
        public bool GetWear()
        {
            return iswear;
        }
        public int GetAbility()
        {
            return ability;
        }
        public string GetKind()
        {
            return kind;
        }
        public bool GetIsBuy()
        {
            return isBuy;
        }
        public int Getprice()
        {
            return price;
        }
    }
}
