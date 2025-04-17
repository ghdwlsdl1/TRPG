using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Choice : Stats
{
    //이름설정
    public bool nameChoice(bool nameError)
    {
        Console.WriteLine("원하시는 이름을 설정해 주세요 (10글자)");
        if (nameError)
        {
            Console.WriteLine("잘못된 입력입니다.\n");
        }
        string nameTxt = Console.ReadLine();
        if (nameTxt.Length <= 10 && nameTxt.Length > 0)
        {
            this.Name = nameTxt;
            return true;
        }
        else
        {
            return false;
        }
    }

    //직업선택
    public bool jobChoice(bool jobError)
    {
        Console.Clear();
        Console.WriteLine("플레이어 캐릭터를 생성하십시오.");
        Console.WriteLine("1. 전사");
        Console.WriteLine("2. 도적");
        Console.WriteLine("3. 마법사");
        Console.WriteLine("4. 야만인");
        Console.WriteLine("스탯은 1~6까지 랜덤 분배됩니다.");
        if (jobError)
        {
            Console.WriteLine("잘못된 입력입니다.\n");
        }
        string text = Console.ReadLine();

        if (text == "1" || text == "2" || text == "3" || text == "4")
        {
            JobNames = int.Parse(text); // 직업 저장

            // 1~6 사이 랜덤 스탯
            startStr = dice6();
            startDex = dice6();
            startInt = dice6();
            startCon = dice6();
            startWis = dice6();
            startLuk = dice6();

            //직업별 추가 스텟
            switch (text)
            {
                case "1": // 전사
                    startStr += 2; // 힘
                    startDex += 2; // 민첩
                    startCon += 3; // 체력
                    break;

                case "2": // 도적
                    startDex += 4; // 민첩
                    startStr += 1; // 힘
                    startCon += 2; // 체력
                    break;

                case "3": // 마법사
                    startInt += 3; // 지능
                    startWis += 3; // 마나
                    startCon += 1; // 체력
                    break;

                case "4": // 야만인
                    startStr += 2; // 힘
                    startCon += 5; // 체력
                    break;
            }
            UpdateStats();
            //현재 Hp설정
            Hp += HpMax;
            Mp += MpMax;

            return true;
        }
        else
        {
            return false;
        }
    }

    //====================게임 선택지====================
    //상태보기
    public bool systemWindows(bool windowsError)
    {
        UpdateStats();
        Console.Clear();
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        Console.WriteLine($"\nLv. {Level}");
        Console.WriteLine($"\n{Name}({Job[JobNames]})");
        Console.WriteLine($"\n\n체 력 {Hp} / {HpMax}    마 나  {Mp} / {MpMax}");
        Console.WriteLine($"\n힘       {Str}       민 첩    {Dex}");
        Console.WriteLine($"\n지 능    {Int}       운       {Luk}");
        Console.WriteLine($"\n건 강    {Con}       지혜     {Wis}");
        Console.WriteLine($"\n공격력   {Atk}       방어력   {Def}");
        Console.WriteLine($"\n\nGold   {Money}");
        Console.WriteLine("\n\n0.나가기");
        Console.WriteLine("\n원하시는 행동을 입력해주세요.");
        if (windowsError)
        {
            Console.WriteLine("잘못된 입력입니다.\n");
            windowsError = false;
        }

        string text1 = Console.ReadLine();
        if (text1 == "0")
        {
            Console.Clear();
            return true;
        }
        else
        {
            return false;
        }
    }

    //인벤토리
    public bool inventory(bool invenError, out bool invenNext)
    {
        Console.Clear();
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine("\n[아이템 목록]");
        PrintInventory();
        Console.WriteLine("\n1. 장착 관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine("\n원하시는 행동을 입력해주세요.");

        if (invenError)
        {
            Console.WriteLine("잘못된 입력입니다.\n");
            invenError = false;
        }

        string text1 = Console.ReadLine();
        if (text1 == "1") //다음으로
        {
            Console.Clear();
            invenNext = true; //넥스트 트루값 변경
            return true;
        }
        else if (text1 == "0") //나가기
        {
            Console.Clear();
            invenNext = false;
            return true;
        }
        else
        {
            invenNext = false;
            return false;
        }
    }

    //장착관리
    public bool inventoryEquip(bool invenEquipError)
    {
        Console.Clear();
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine("\n[아이템 목록]");
        PrintInventory();
        Console.WriteLine("\n장착하거나 뺄 번호를 입력해주세요. (예: W1, A2, R3)");
        Console.WriteLine("0. 나가기");
        Console.WriteLine("\n원하시는 행동을 입력해주세요.");
        if (invenEquipError)
        {
            Console.WriteLine("잘못된 입력입니다.\n");
            invenEquipError = false;
        }

        string text = Console.ReadLine();
        if (text == "0")
        {
            Console.Clear();
            return true;
        }

        if (text.Length >= 2)
        {
            char type = char.ToUpper(text[0]);// 대문자 변환 저장
            if (int.TryParse(text.Substring(1), out int number)) //두번쨰 문자 인트값으로 변환
            {
                switch (type)
                {
                    case 'W': // 무기
                        if (number > 0 && number < weapon.Length && weaponTf[number]) // 0보다 클경우(0은 탈출) / 배열 길이 확인 / 가지고 있는지
                        {
                            if (weaponEquip[number]) // 이미 장착되어 있으면 해제
                            {
                                weaponEquip[number] = false;
                            }
                            else // 장착되어 있지 않으면 장착
                            {
                                for (int i = 0; i < weaponEquip.Length; i++)
                                {
                                    weaponEquip[i] = false; // 모든 무기 해제
                                }
                                weaponEquip[number] = true; // 선택한 무기 장착
                            }
                            UpdateStats();
                            return true;
                        }
                        break;

                    case 'A': // 보조 장비
                        if (number > 0 && number < assist.Length && assistTf[number])
                        {
                            if (assistEquip[number])
                            {
                                assistEquip[number] = false;
                            }
                            else
                            {
                                for (int i = 0; i < assistEquip.Length; i++)
                                {
                                    assistEquip[i] = false;
                                }
                                assistEquip[number] = true;
                            }
                            UpdateStats();
                            return true;
                        }
                        break;

                    case 'R': // 갑옷
                        if (number > 0 && number < armor.Length && armorTf[number])
                        {
                            if (armorEquip[number]) 
                            {
                                armorEquip[number] = false;
                            }
                            else
                            {
                                for (int i = 0; i < armorEquip.Length; i++)
                                {
                                    armorEquip[i] = false;
                                }
                                armorEquip[number] = true;
                            }
                            UpdateStats();
                            return true;
                        }
                        break;
                }
            }
        }
        return false;
    }

    //인벤토리 아이탬
    public void PrintInventory()
    {
        // 무기 목록
        Console.WriteLine("\n[무기 목록]");
        for (int i = 1; i < weapon.Length; i++)
        {
            if (weaponTf[i])
            {
                Console.WriteLine($"W{i}.[{(weaponEquip[i] ? "e" : "")}] {weapon[i]}");
            }
        }

        // 보조 장비 목록
        Console.WriteLine("\n[보조 장비 목록]");
        for (int i = 1; i < assist.Length; i++)
        {
            if (assistTf[i])
            {
                Console.WriteLine($"A{i}.[{(assistEquip[i] ? "e" : "")}] {assist[i]}");
            }
        }

        // 갑옷 목록
        Console.WriteLine("\n[갑옷 목록]");
        for (int i = 1; i < armor.Length; i++)
        {
            if (armorTf[i])
            {
                Console.WriteLine($"R{i}.[{(armorEquip[i] ? "e" : "")}] {armor[i]}");
            }
        }
    }
    //상점
    public bool Store(ref bool StoreError, ref bool MoneyLack)
    {
        Console.Clear();
        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine("\n[아이템 목록]");
        printStore();
        Console.WriteLine($"\n\nGold   {Money}");
        Console.WriteLine("\n구입하거나 판매할 번호를 입력해주세요. (예: W1, A2, R3)");
        Console.WriteLine("판매 시 가격의 50%를 받습니다.");
        Console.WriteLine("\n\n0.나가기");
        Console.WriteLine("\n원하시는 행동을 입력해주세요.");

        if (MoneyLack)
        {
            Console.WriteLine("돈이 부족합니다.\n");
            MoneyLack = false;
        }
        else if (StoreError)
        {
            Console.WriteLine("잘못된 입력입니다.\n");
            StoreError = false;
        }

        string text = Console.ReadLine();
        if (text == "0")
        {
            Console.Clear();
            return true;
        }

        if (text.Length >= 2)
        {
            char type = char.ToUpper(text[0]);
            if (int.TryParse(text.Substring(1), out int number))
            {
                switch (type)
                {
                    case 'W': // 무기
                        if (number > 0 && number < weapon.Length)
                        {
                            if (!weaponTf[number])
                            {
                                if (Money >= weaponDeal[number])
                                {
                                    weaponTf[number] = !weaponTf[number]; // ! 보유값 반전
                                    Money -= weaponDeal[number]; // 금액차감
                                    return true;
                                }
                                else
                                {
                                    MoneyLack = true;
                                    return false;
                                }
                            }
                            if (weaponTf[number])
                            {
                                weaponTf[number] = !weaponTf[number];
                                Money += weaponDeal[number] * (85 / weaponDeal[number]);
                                return true;
                            }
                        }
                        break;

                    case 'A': // 보조 장비
                        if (number > 0 && number < assist.Length)
                        {
                            if (!assistTf[number])
                            {
                                if (Money >= assistDeal[number])
                                {
                                    assistTf[number] = !assistTf[number];
                                    Money -= assistDeal[number];
                                    return true;
                                }
                                else
                                {
                                    MoneyLack = true;
                                    return false;
                                }
                            }
                            if (assistTf[number])
                            {
                                assistTf[number] = !assistTf[number];
                                Money += assistDeal[number] * (85 / weaponDeal[number]);
                                return true;
                            }
                        }
                        break;

                    case 'R': // 갑옷
                        if (number > 0 && number < armor.Length)
                        {
                            if (!armorTf[number])
                            {
                                if (Money >= armorDeal[number])
                                {
                                    armorTf[number] = !armorTf[number];
                                    Money -= armorDeal[number];
                                    return true;
                                }
                                else
                                {
                                    MoneyLack = true;
                                    return false;
                                }
                            }
                            if (armorTf[number])
                            {
                                armorTf[number] = !armorTf[number];
                                Money += armorDeal[number] * (85 / armorDeal[number]);
                                return true;
                            }
                        }
                        break;
                }
            }
        }
        StoreError = true;
        return false;
    }
    //상점목록
    public void printStore()
    {
        // 무기 목록
        Console.WriteLine("\n[무기 목록]");
        for (int i = 1; i < weapon.Length; i++)
        {
            Console.WriteLine($"W{i}.[{(weaponTf[i] ? "소지중" : "없음")}] {weapon[i]} {weaponDeal[i]}Gold");
        }
        // 보조 장비 목록
        Console.WriteLine("\n[보조 장비 목록]");
        for (int i = 1; i < assist.Length; i++)
        {
            Console.WriteLine($"A{i}.[{(assistTf[i] ? "소지중" : "없음")}] {assist[i]} {assistDeal[i]}Gold");
        }
        // 갑옷 목록
        Console.WriteLine("\n[갑옷 목록]");
        for (int i = 1; i < armor.Length; i++)
        {
            Console.WriteLine($"R{i}.[{(armorTf[i] ? "소지중" : "없음")}] {armor[i]} {armorDeal[i]}Gold");
        }
    }

    // 휴식하기
    public bool hotel(ref bool hotelError, ref bool MoneyLack)
    {
        Console.Clear();
        Console.WriteLine("숙소에방문하였습니다");
        Console.WriteLine("500 GGold 를 내면 체력을 회복할 수 있습니다.");
        Console.WriteLine($"\n\nGold   {Money}");
        Console.WriteLine("\n1. 휴식하기");
        Console.WriteLine("0. 나가기");
        Console.WriteLine("\n원하시는 행동을 입력해주세요.");

        if (MoneyLack)
        {
            Console.WriteLine("돈이 부족합니다.\n");
            MoneyLack = false;
        }
        else if (hotelError)
        {
            Console.WriteLine("잘못된 입력입니다.\n");
            hotelError = false;
        }

        string text = Console.ReadLine();
        if (text == "0")
        {
            Console.Clear();
            return true;
        }
        if (text == "1")
        {
            if (Money >= 500)
            {
                Hp = HpMax;
                Mp = MpMax;
                Money -= 500; //
                return true;
            }
            else
            {
                MoneyLack = true;
                return false;
            }
        }
        hotelError = true;
        return false;
    }
    //던전
    //캐릭터 좌표
    public char[,] map;
    public int playerX = -1, playerY = -1;
    public int portalX = -2, portalY = -2;
    public int floor = 1; // 현재 층

    // 층에 따라 맵 크기를 계산하는 메서드
    int GetMapSize(int floor)
    {
        return 5 + (floor - 1) * 5;
    }

    public void Mainmap()
    {
        int size = GetMapSize(floor); // 현재 층의 맵 크기
        map = new char[size, size];

        // 지도 초기화: 빈 공간(' ')으로
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                map[y, x] = ' ';
            }
        }

        // 플레이어 위치 초기화
        if (playerX == -1 && playerY == -1)
        {
            playerX = random.Next(0, size);
            playerY = random.Next(0, size);
        }

        // 포탈 위치 초기화
        if (portalX == -2 && portalY == -2)
        {
            portalX = random.Next(0, size);
            portalY = random.Next(0, size);
        }

        // 지도에 표시
        map[portalY, portalX] = '■';
        map[playerY, playerX] = 'P';
    }

    public void PrintMap()
    {
        int size = map.GetLength(0);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Console.Write(map[y, x] + " ");
            }
            Console.WriteLine();
        }
    }

    // 이동
    public bool move(ref bool moveError, ref bool moveError2)
    {
        Console.Clear();

        Mainmap();  // 맵 초기화

        PrintMap(); // 맵 출력
        Console.WriteLine("이동 방향을 정해주세요.");
        Console.WriteLine("1. 위");
        Console.WriteLine("2. 아래");
        Console.WriteLine("3. 오른쪽");
        Console.WriteLine("4. 왼쪽");

        if (moveError)
        {
            Console.WriteLine("잘못된 입력입니다.\n");
            moveError = false;
        }
        else if (moveError2)
        {
            Console.WriteLine("이동할 수 없습니다.\n");
            moveError2 = false;
        }

        string text = Console.ReadLine();

        switch (text)
        {
            case "1":
                if (playerY > 0)
                {
                    playerY--;
                    return true;
                }
                else
                {
                    moveError2 = true;
                    return false;
                }

            case "2":
                if (playerY < 29)
                {
                    playerY++;
                    return true;
                }
                else
                {
                    moveError2 = true;
                    return false;
                }

            case "3":
                if (playerX < 29)
                {
                    playerX++;
                    return true;
                }
                else
                {
                    moveError2 = true;
                    return false;
                }

            case "4":
                if (playerX > 0)
                {
                    playerX--;
                    return true;
                }
                else
                {
                    moveError2 = true;
                    return false;
                }

            default:
                Console.Clear();
                moveError = true;
                return false;
        }
    }
}
