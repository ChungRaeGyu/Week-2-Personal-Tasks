using System;
using System.Drawing;

namespace ConsoleApp1
{   
    class struct equipment
    {

    }
    class Program
    {
        static void Main(String[] args)
        {
            bool gameOver = true; //Update
            int num; //숫자를 받기위한 변수

            //캐릭터 정보
            int level = 01;
            string job = "전사";
            int attack = 10;
            int defence = 5;
            int hp = 100;
            int gold = 1500;
            

            while (gameOver)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\r\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\r\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("원하시는 행동을 입력해주세요");
                while (true)
                {
                    num = int.Parse(Console.ReadLine());
                    if (num >= 1 && num <= 3)
                    {
                        //세부 메뉴
                        switch (num)
                        {
                            case 1:
                                Console.WriteLine("\n");
                                Console.WriteLine("상태 보기"); //글자 색 변경하기
                                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                                Console.WriteLine($"Lv. {level}");
                                Console.WriteLine($"Chad( {job} )");
                                Console.WriteLine($"공격력 : {attack}");
                                Console.WriteLine($"방어력 : {defence}");
                                Console.WriteLine($"체 력 : {hp}");
                                Console.WriteLine($"Gold : {gold} G\n");
                                Console.WriteLine("0. 나가기\n");
                                Console.WriteLine("원하시는 행동을 입력해주세요.");
                                num = int.Parse(Console.ReadLine());
                                if (num == 0)
                                {
                                    Console.WriteLine("\n");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("잘못된 입력입니다.");
                                    return;
                                }
                            case 2:
                                Console.WriteLine("\n");
                                Console.WriteLine("인벤토리"); //글자 색 변경하기
                                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                                Console.WriteLine("[아이템 목록]\n");
                                Console.WriteLine("1. 장착 관리");
                                Console.WriteLine("0. 나가기\n");
                                Console.WriteLine("원하시는 행동을 입력해주세요.");
                                num = int.Parse(Console.ReadLine());
                                if (num == 0)
                                {
                                    Console.WriteLine("\n");
                                    break;
                                }
                                else if (num == 1)
                                {
                                    Console.WriteLine("\n");
                                    Console.WriteLine("인벤토리 - 장착관리"); //글자 색 변경하기
                                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                                    Console.WriteLine("[아이템 목록]");
                                    Console.WriteLine("- 1. 장착 관리");
                                    Console.WriteLine("0. 나가기\n");
                                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                                }
                                else
                                {
                                    Console.WriteLine("잘못된 입력입니다.");
                                    return;
                                }
                            case 3:break;
                        }
                    }
                    else
                    {
                        Console.WriteLine(("잘못된 입력입니다."));
                    }
                    break;
                }
            }
        }
        
    }

}


