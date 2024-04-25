using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;

namespace ConsoleApp1
{   
    struct equipment
    {
        string name;  //이름
        int ability;  //능력치
        string kind;  //종류
        bool iswear;  //착용 여부
        string description; //설명
        int price;
        bool isBuy;
        public void setting(string name, int ab, string kind,bool wear,string de,int price,bool isBuy)
        {
            this.name = name;
            this.ability = ab;
            this.kind = kind;
            this.iswear = wear; 
            this.description = de;
            this.price = price;
            this.isBuy = isBuy;
        }
        public void PrintStorage(int num,bool check) {
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
        public void PrintStore(int num,bool check)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintStorage(num,check);
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
            Console.Write(price*0.85f);
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
        public bool IsWear()
        {
            iswear = !iswear;
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
    struct Player
    {
        string level;
        string job;
        int attack;
        int plusAttack;
        int defence;
        int plusDefence;
        int hp;
        int gold;
        string atkString;
        string defString;

        public void setting(string level,string job,int attack, int defence, int hp, int gold)
        {
            this.level = level;
            this.job = job;
            this.attack = attack;
            this.defence = defence;
            this.hp = hp;
            this.gold = gold;
            plusAttack = 0;
            plusDefence = 0;
        }
        public void print()
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("상태 보기"); //글자 색 변경하기
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {level}");
            Console.WriteLine($"Chad( {job} )");
            if (plusAttack!=0)
            {
                Console.WriteLine(atkString);
            }
            else
            {
                Console.WriteLine($"공격력 : {attack}");
            }
            if (plusDefence!=0)
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

        public void Wear(string kind,int ability)
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
        public int GetGold()
        {
            return gold;
        }
        public void SetGold(int gold)
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
        public int GetAttack()
        {
            return attack;
        }
        

    }
    struct Dongeon
    {
        int count;
        string name;
        int def;
        int gold;
        public int reHp;
        public int reGold;
        bool clear;
        public void Setting(int count,string name, int def,int gold)
        {
            this.count = count;
            this.name = name;
            this.def = def;
            this.gold = gold;
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

        public Dongeon DongeonPlay(int pDef,int hp,int pAtk)
        {
            Random rand = new Random();
            if (def > pDef)
            {
                if (rand.Next(1, 11) <= 4)//던전 실패 확률 40%
                {
                    clear = false;
                    return new Dongeon { reHp=hp/2,reGold=0};
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
               return new Dongeon { reHp = DecreaseHp(pDef, hp), reGold = CompensationGold(pAtk) } ;
            }
        }
        public void DongeonClearPrint(int prevHp,int nowHp,int prevGold,int nowGold)
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
        int DecreaseHp(int pDef,int hp)
        {
            int difference = pDef - def;
            Random rand = new();
            //기본 체력 감소
            return rand.Next(20-difference, 35-difference);
        }
        int CompensationGold(int pAtk)
        {
            Random rand = new();
            return rand.Next(gold+gold*pAtk/100, gold+gold*pAtk*2/100);
        }
        public bool IsClear()
        {
            return clear;
        }
    }
    class Program
    {
        static void Main(String[] args)
        {
            #region 변수 및 아이템,플레이어 기본값
            int eWeapon = -1;   //현재 장착한 장비관리
            int eDef = -1;      //현재 장착한 장비 관리
            
            //플레이어
            Player player = new();
            player.setting("01", "전사", 10, 5, 100, 1500);

            //장비
            equipment[] eqs= new equipment[7];
            eqs[0].setting("수련자 갑옷", 5, "방어구", false, "수련에 도움을 주는 갑옷입니다.",1000,false);
            eqs[1].setting("무쇠갑옷", 9, "방어구", false, "무쇠로 만들어져 튼튼한 갑옷입니다.",2000,true);
            eqs[2].setting("스파르타의 갑옷", 15, "방어구", false, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false);
            eqs[3].setting("낡은 검", 2, "무기", false, "쉽게 볼 수 있는 낡은 검 입니다.", 600, false);
            eqs[4].setting("청동 도끼",5, "무기", false, "어디선가 사용됐던거 같은 도끼입니다.", 1500, false);
            eqs[5].setting("스파르타의 창", 7, "무기", false, "스파르타의 전사들이 사용했다는 전설의 창입니다.",3500,true);
            eqs[6].setting("황성빈의 방망이", 15, "무기", false, "롯데자이언츠의 황성빈의 방망이다.", 3500, false);

            //던전
            Dongeon[] dg = new Dongeon[3];
            dg[0].Setting(1, "쉬운 던전", 5,1000);
            dg[1].Setting(2, "일반 던전", 11,1700);
            dg[2].Setting(3, "어려운 던전", 17,2500);
            bool gameOver = true; //Update
            int num=-1; //숫자를 받기위한 변수
            bool wear; //장착관리를 종료하기 위한 변수
            bool inventroy;
            //캐릭터 정보
            #endregion //변수관리

            while (gameOver)
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\r\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\r\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전입장");
                Console.WriteLine("원하시는 행동을 입력해주세요");
                while (true)
                {
                    try
                    {
                        num = int.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.Write(("숫자를 입력해 주세요."));
                    }
                        //문자나 숫자 이런것들이 나오면 다시 시작
                    if (num >= 1 && num <= 4)
                    {
                        #region 세부메뉴
                        switch (num)
                        {
                            case 1:
                                #region 상태창
                                player.print();
                                while (num!=0)
                                {
                                    try
                                    {
                                        num = int.Parse(Console.ReadLine());
                                    }
                                    catch (Exception)
                                    {
                                        Console.Write(("숫자를 입력해 주세요."));
                                    }
                                    if (num == 0)
                                    {
                                        Console.WriteLine("\n");

                                    }
                                    else
                                    {
                                        Console.WriteLine("잘못된 입력입니다.\n");
                                    }
                                }
                                break;
                            #endregion
                            case 2:
                                #region 인벤토리
                                inventroy = true;
                                while (inventroy)
                                {
                                    Console.WriteLine("---------------------------------------------------------");
                                    Console.WriteLine();
                                    Console.WriteLine("인벤토리"); //글자 색 변경하기
                                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                                    Console.WriteLine("[아이템 목록]\n");
                                    Console.WriteLine("1. 장착 관리");
                                    Console.WriteLine("0. 나가기\n");
                                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                                    while (true)
                                    {
                                        try
                                        {
                                            num = int.Parse(Console.ReadLine());
                                        }
                                        catch (Exception)
                                        {
                                            Console.Write(("숫자를 입력해 주세요."));
                                        };
                                        if (num == 0)
                                        {
                                            Console.WriteLine("\n");
                                            inventroy = false;
                                            break;
                                        }
                                        else if (num == 1)
                                        {
                                            wear = true;
                                            #region 장착관리
                                            while (wear)
                                            {
                                                Console.WriteLine("---------------------------------------------------------");
                                                Console.WriteLine("");
                                                Console.WriteLine("인벤토리 - 장착관리"); //글자 색 변경하기
                                                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                                                Console.WriteLine("[아이템 목록]");
                                                int eqCount = 0;
                                                int checkNum = 0;
                                                int[] ints = new int[eqs.Length];
                                                foreach (equipment eq in eqs)
                                                {
                                                    if (eq.IsBuyCheck())
                                                    {
                                                        eqCount++;
                                                        eq.PrintStorage(eqCount,true);
                                                        Console.WriteLine();
                                                        ints[eqCount - 1] = checkNum;
                                                        //여기서 장착하면 checkNum을 어디 저장을 하나 해놓고
                                                    }
                                                    checkNum++;
                                                }
                                                Console.WriteLine("0. 나가기\n");
                                                Console.WriteLine("원하시는 행동을 입력해주세요.");
                                                while (true)
                                                {
                                                    try
                                                    {
                                                        num = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.Write(("숫자를 입력해 주세요."));
                                                    }
                                                    if (num == 0)
                                                    {
                                                        Console.WriteLine("\n");
                                                        wear = !wear;
                                                        break;
                                                    }
                                                    else if (0 < num && num <= eqCount)
                                                    {
                                                        if (eqs[ints[num - 1]].IsWear())//장착
                                                        {
                                                            //이전에 장착한 아이템이 있는지 확인
                                                            if (eqs[ints[num - 1]].GetKind() == "무기")
                                                            {
                                                                if (eWeapon > -1)//장착한 무기가 있을 때
                                                                {
                                                                    player.Release(eqs[eWeapon].GetKind(), eqs[eWeapon].GetAbility());
                                                                    eqs[eWeapon].IsWear();
                                                                }
                                                                eWeapon = ints[num - 1];
                                                            }
                                                            else
                                                            {
                                                                if (eDef > -1)
                                                                {
                                                                    player.Release(eqs[eDef].GetKind(), eqs[eDef].GetAbility());
                                                                    eqs[eDef].IsWear();
                                                                }
                                                                eDef = ints[num - 1];
                                                            }
                                                                //장착한 아이템 설정
                                                                player.Wear(eqs[ints[num - 1]].GetKind(), eqs[ints[num - 1]].GetAbility());
                                                        }
                                                        else
                                                        {
                                                            //방어력이 빠진다.
                                                            player.Release(eqs[ints[num - 1]].GetKind(), eqs[ints[num - 1]].GetAbility());
                                                            if (eqs[ints[num - 1]].GetKind() == "무기")
                                                            {
                                                                eWeapon = -1;
                                                            }
                                                            else
                                                            {
                                                                eDef = -1;
                                                            }
                                                        }
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("잘못된 입력입니다.");
                                                    }
                                                }
                                            }

                                            #endregion
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("잘못된 입력입니다.\n");
                                        }                                        
                                    }
                                }
                                break;
                                
                            #endregion
                            case 3:
                                #region 상점
                                bool store = true;
                                bool Buy;
                                while (store)
                                {
                                    Buy = true;
                                    Console.WriteLine("---------------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    
                                    Console.WriteLine("상점");
                                    Console.ResetColor();
                                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                                    Console.WriteLine("[보유 골드]");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write($"{player.GetGold()}");
                                    Console.ResetColor();
                                    Console.WriteLine(" G\n\n");
                                    Console.WriteLine("[아이템 목록]");
                                    int eqCount1 = 0;
                                    foreach (equipment eq in eqs)
                                    {
                                        eqCount1++;
                                        eq.PrintStore(eqCount1, false); //false값을 통해 목록의 숫자를 보이게 합니다.
                                        Console.WriteLine("\n");
                                    }
                                    Console.WriteLine("1. 아이템 구매");
                                    Console.WriteLine("2. 아이템 판매");
                                    Console.WriteLine("0. 나가기\n");
                                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                                    while (true) {
                                        try
                                        {
                                            num = int.Parse(Console.ReadLine());
                                        }
                                        catch (Exception)
                                        {
                                            Console.Write(("숫자를 입력해 주세요."));
                                        }
                                        if (num == 0)
                                        {
                                            Console.WriteLine();
                                            store = false;
                                            break;
                                        }

                                        else if (num == 1)
                                        {
                                            #region 아이템 구매

                                            while (Buy)
                                            {
                                                Console.WriteLine("---------------------------------------------------------");
                                                Console.ForegroundColor = ConsoleColor.Red;

                                                Console.WriteLine("상점 - 아이템 구매");
                                                Console.ResetColor();
                                                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                                                Console.WriteLine("[보유 골드]");
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write($"{player.GetGold()}");
                                                Console.ResetColor();
                                                Console.WriteLine(" G\n\n");
                                                Console.WriteLine("[아이템 목록]");
                                                int eqCount2 = 0;
                                                foreach (equipment eq in eqs)
                                                {
                                                    eqCount2++;
                                                    eq.PrintStore(eqCount2, true); //false값을 통해 목록의 숫자를 보이게 합니다.
                                                    Console.WriteLine("\n");
                                                }
                                                Console.WriteLine("0. 나가기\n");
                                                Console.WriteLine("원하시는 행동을 입력해주세요.");
                                                while (true)
                                                {
                                                    try
                                                    {
                                                        num = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.Write(("숫자를 입력해 주세요."));
                                                    }
                                                    if (num == 0)
                                                    {
                                                        Console.WriteLine();
                                                        Buy = false;
                                                        break;
                                                    }
                                                    else if (0 < num && num <=eqCount2)
                                                    {
                                                        if (eqs[num - 1].GetIsBuy())
                                                        {
                                                            Console.WriteLine("이미 구매한 아이템입니다.");
                                                        }
                                                        else if (eqs[num - 1].Getprice() > player.GetGold())
                                                        {
                                                            Console.WriteLine("Gold가 부족합니다.");
                                                        }
                                                        else
                                                        {
                                                            eqs[num - 1].IsBuyController();
                                                            player.SetGold(player.GetGold() - eqs[num - 1].Getprice());
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("잘못된 입력입니다.");
                                                    }
                                                }

                                            }
                                            #endregion
                                            break;
                                        }
                                        else if (num == 2)
                                        {
                                            #region 아이템 판매

                                            while (Buy)
                                            {
                                                Console.WriteLine("---------------------------------------------------------");
                                                Console.ForegroundColor = ConsoleColor.Red;

                                                Console.WriteLine("상점 - 아이템 판매");
                                                Console.ResetColor();
                                                Console.WriteLine("아이템을 판매 할 수 있습니다..\n");

                                                Console.WriteLine("[보유 골드]");
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.Write($"{player.GetGold()}");
                                                Console.ResetColor();
                                                Console.WriteLine(" G\n\n");
                                                Console.WriteLine("[아이템 목록]");
                                                int eqCount3 = 0;
                                                int checkNum = 0;
                                                int[] ints = new int[eqs.Length];
                                                foreach (equipment eq in eqs)
                                                {
                                                    if (eq.IsBuyCheck())
                                                    {
                                                        eqCount3++;
                                                        eq.PrintSale(eqCount3, true);
                                                        Console.WriteLine();
                                                        ints[eqCount3 - 1] = checkNum;

                                                    }
                                                    checkNum++;
                                                }
                                                Console.WriteLine("0. 나가기\n");
                                                Console.WriteLine("원하시는 행동을 입력해주세요.");
                                                while (true)
                                                {
                                                    try
                                                    {
                                                        num = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.Write(("숫자를 입력해 주세요."));
                                                    }
                                                    if (num == 0)
                                                    {
                                                        Console.WriteLine();
                                                        Buy = false;
                                                        break;
                                                    }
                                                    else if (0 < num && num <=eqCount3)
                                                    { 
                                                        eqs[ints[num - 1]].IsBuyController();
                                                        int sale = eqs[ints[num - 1]].Getprice();
                                                        player.SetGold(player.GetGold()+(sale*85/100));
                                                        eqs[ints[num - 1]].IsWear();
                                                        player.Release(eqs[ints[num - 1]].GetKind(), eqs[ints[num-1]].GetAbility());
                                                        if (eqs[ints[num - 1]].GetKind() == "무기")
                                                        {
                                                            eWeapon = -1;
                                                        }
                                                        else
                                                        {
                                                            eDef = -1;
                                                        }
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("잘못된 입력입니다.");
                                                    }
                                                }

                                            }
                                            #endregion
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("잘못된 입력입니다.");
                                        }
                                    }
                                    
                                }
                            break;
                            #endregion //상점
                            case 4:
                                #region 던전입장
                                bool dungeon = true;
                                while (dungeon)
                                {
                                    Console.WriteLine("---------------------------------------------------------");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("던전입장");
                                    Console.ResetColor();
                                    Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

                                    for(int i=0; i < dg.Length; i++)
                                    {
                                        dg[i].DongeonPrint();
                                    }
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("0. ");
                                    Console.ResetColor();
                                    Console.WriteLine("나가기");
                                    Console.WriteLine("원하시는 행동을 입력해주세요");
                                    while (true)
                                    {
                                        try
                                        {
                                            num = int.Parse(Console.ReadLine());
                                        }
                                        catch (Exception)
                                        {
                                            Console.WriteLine("숫자를 입력하세요");
                                        }
                                        if(num == 0)
                                        {
                                            dungeon = false;
                                            break;
                                        }else if (0 < num && num <= 3)
                                        {
                                            int hp = player.GetHp();
                                            int gold = player.GetGold();
                                            Dongeon a = dg[num - 1].DongeonPlay(player.GetDefence(), hp, player.GetAttack());
                                            player.SetHp(hp-a.reHp);
                                            player.SetGold(gold+a.reGold);
                                            if (dg[num - 1].IsClear())
                                            {
                                                int aa=-1;
                                                dg[num - 1].DongeonClearPrint(hp, player.GetHp(), gold, player.GetGold());
                                                while (true)
                                                {
                                                    try
                                                    {
                                                        aa = int.Parse(Console.ReadLine());
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.WriteLine("숫자를 입력해 주세요");
                                                    }
                                                    if (aa == 0)
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("잘못 입력 하셨습니다.");
                                                    }
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("---------------------------------------------------------");
                                                Console.WriteLine("실패~~~~~~");
                                                Console.WriteLine($"현재 체력 : {player.GetHp()}");
                                                Console.WriteLine("---------------------------------------------------------");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("잘못된 입력입니다.");
                                        }
                                    }
                                }
                                break;
                                #endregion //던전입장
                        }
                        #endregion
                        break;
                    }
                    else
                    {
                        Console.WriteLine(("잘못된 입력입니다.\n"));
                        
                    }
                }
            }
        }
    }
}

