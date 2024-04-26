using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Player
    {
        int level;
        int clear;
        string job;
        float attack;
        int plusAttack;
        int defence;
        int plusDefence;
        int hp;
        float gold;
        string atkString;
        string defString;

        public Player(int level, string job, float attack, int defence, int hp, float gold)
        {
            this.level = level;
            this.job = job;
            this.attack = attack;
            this.defence = defence;
            this.hp = hp;
            this.gold = gold;
            plusAttack = 0;
            plusDefence = 0;
            clear = 0;
        }
        public void print()
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("상태 보기"); //글자 색 변경하기
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {level.ToString("00")}");
            Console.WriteLine($"Chad( {job} )");
            if (plusAttack != 0)
            {
                Console.WriteLine(atkString);
            }
            else
            {
                Console.WriteLine($"공격력 : {attack}");
            }
            if (plusDefence != 0)
            {
                Console.WriteLine(defString);
            }
            else
            {
                Console.WriteLine($"방어력 : {defence}");
            }

            Console.WriteLine($"체 력 : {hp}");
            Console.WriteLine($"Gold : {gold} G\n");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }

        public void Wear(string kind, int ability)
        {
            if (kind == "무기")
            {
                plusAttack += ability;
                attack += ability;
                atkString = ($"공격력 : {attack} ({plusAttack})");
            }
            else
            {
                plusDefence += ability;
                defence += ability;
                defString = ($"방어력 : {defence} ({plusDefence})");
            }
        }
        public void Release(string kind, int ability)
        {
            if (kind == "무기")
            {
                plusAttack -= ability;
                attack -= ability;
                atkString = ($"공격력 : {attack} ({plusAttack})");
            }
            else
            {
                plusDefence -= ability;
                defence -= ability;
                defString = ($"방어력 : {defence} ({plusDefence})");
            }
        }
        public float GetGold()
        {
            return gold;
        }
        public void SetGold(float gold)
        {
            this.gold = gold;
        }
        public int GetDefence()
        {
            return defence;
        }
        public int GetHp()
        {
            return hp;
        }
        public void SetHp(int h)
        {
            hp = h;
        }
        public float GetAttack()
        {
            return attack;
        }
        public void PlayerUpdate()
        {
            clear++;
            level++;
            attack += 0.5f;
            defence += 1;
        }
    }
}
