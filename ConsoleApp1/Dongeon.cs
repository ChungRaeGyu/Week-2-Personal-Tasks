using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Dongeon
    {
        int count;
        string name;
        int def;
        float gold;
        public int reHp;
        public float reGold;
        bool clear;
        public Dongeon(int count, string name, int def, float gold)
        {
            this.count = count;
            this.name = name;
            this.def = def;
            this.gold = gold;
        }
        public Dongeon()
        {

        }
        public void DongeonPrint()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{count}. ");
            Console.ResetColor();
            Console.Write($"{name,-7}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("|");
            Console.ResetColor();
            Console.Write("방어력");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" {def} ");
            Console.ResetColor();
            Console.WriteLine("이상 권장");
        }

        public Dongeon DongeonPlay(int pDef, int hp, float pAtk)
        {
            Random rand = new Random();
            if (def > pDef)
            {
                if (rand.Next(1, 11) <= 4)//던전 실패 확률 40%
                {
                    clear = false;
                    return new Dongeon { reHp = hp / 2, reGold = 0 };
                }
                else
                {
                    clear = true;
                    return new Dongeon { reHp = DecreaseHp(pDef, hp), reGold = CompensationGold(pAtk) };
                }
            }
            else
            {
                clear = true;
                return new Dongeon { reHp = DecreaseHp(pDef, hp), reGold = CompensationGold(pAtk) };
            }
        }
        public void DongeonClearPrint(int prevHp, int nowHp, float prevGold, float nowGold)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n던전 클리어");
            Console.ResetColor();
            Console.WriteLine("축하합니다!!");
            Console.WriteLine("쉬운 던전을 클리어 하였습니다.\n");

            Console.WriteLine("[탐험 결과]");
            Console.Write("체력 ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{prevHp} -> {nowHp}");
            Console.ResetColor();
            Console.Write("Gold ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{prevGold}");
            Console.ResetColor();
            Console.Write(" G ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"-> {nowGold}");
            Console.ResetColor();
            Console.WriteLine(" G \n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("0. ");
            Console.ResetColor();
            Console.WriteLine("나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }
        int DecreaseHp(int pDef, int hp)
        {
            int difference = pDef - def;
            Random rand = new();
            //기본 체력 감소
            return rand.Next(20 - difference, 35 - difference);
        }
        float CompensationGold(float pAtk)
        {
            Random rand = new();
            return rand.Next(Convert.ToInt32(gold + gold * pAtk / 100), Convert.ToInt32(gold + gold * pAtk * 2 / 100));
        }
        public bool IsClear()
        {
            return clear;
        }
    }
}
