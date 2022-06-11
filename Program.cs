using System;

namespace TelefonRehberi
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Yeni bir console uygulaması açarak telefon rehberi uygulaması yazınız. Uygulamada olması gereken özellikler aşağıdaki gibidir.



            Telefon Numarası Kaydet
            Telefon Numarası Sil
            Telefon Numarası Güncelle
            Rehber Listeleme (A-Z, Z-A seçimli)
            Rehberde Arama


            Açıklama:



            Başlangıç olarak 5 kişinin numarasını varsayılan olarak ekleyiniz.


            Uygulama ilk başladığında kullanıcıya yapmak istediği işlem seçtirilir.*/
            Flag:

            Console.WriteLine("Lütfen Yapmak İstediğiniz İşlemi Seçiniz :)");
            Console.WriteLine("*******************************************");
            Console.WriteLine(" ");
            Console.WriteLine("(1) Yeni Numara Kaydetmek");
            Console.WriteLine("(2) Var Olan Numarayı Silmek");
            Console.WriteLine("(3) Var Olan Numarayı Güncelleme");
            Console.WriteLine("(4) Rehberi Listelemek");
            Console.WriteLine("(5) Reherde Arama Yapmak");

            int number;

            bool success = int.TryParse(Console.ReadLine(), out number);
            if (success && number>0 && number<6)
            {


                    if (number == 1)
                    {
                        SaveNumber();
                    }
                    else if (number == 2)
                    {
                        DeleteNumber();
                    }
                    else if (number == 3)
                    {
                        UpdateNumber();
                    }
                    else if (number == 4)
                    {
                        PrintNumberList();
                    }
                    else if (number == 5)
                    {
                        SearchNumber();
                    }


            
            }
            else
            {
                Console.WriteLine("Lütfen Geçerli Bir Seçim Yapınız");
            }

        }

        public static void PrintNumberList()
        {
            Console.WriteLine("Telefon Rehberi");
            Console.WriteLine("****************************");
            for (int i = 0; i < PhoneBookListModel.phoneBookNumberList.Count; i++)
            {
                Console.WriteLine("isim: {0}", PhoneBookListModel.phoneBookNumberList[i].Name);
                Console.WriteLine("Soyisim: {0}", PhoneBookListModel.phoneBookNumberList[i].Surname);
                Console.WriteLine("Telefon Numarası: {0}", PhoneBookListModel.phoneBookNumberList[i].Number);
                Console.WriteLine("-");
            }
        }

        //Rehbere yeni kullanıcı kaydeden metodumuz.
        public static void SaveNumber()
        {
            Console.WriteLine(" Lütfen isim giriniz             : ");
            string name = Console.ReadLine();
            Console.WriteLine(" Lütfen soyisim giriniz          : ");
            string surname = Console.ReadLine();
            Console.WriteLine(" Lütfen telefon numarası giriniz : ");
            string number = Console.ReadLine();
            PhoneBookListModel.phoneBookNumberList.Add(new PhoneBookNumberModel(name, surname, number));

        }

        //Rehberden kullanıcı silen metodumuz.
        public static void DeleteNumber()
        {
            Console.WriteLine("Lütfen numarasını silmek istediğiniz kişinin adını ya da soyadını giriniz:");
            string nameOrSurname = Console.ReadLine();
            PhoneBookNumberModel phoneNumber = Search(nameOrSurname);

            if (phoneNumber != null)
            {
                Console.WriteLine("{0} isimli kişi rehberden silinmek üzere, onaylıyor musunuz ?(y/n)", nameOrSurname);
                string s = Console.ReadLine();
                if (s.ToLower() == "y")
                {
                    PhoneBookListModel.phoneBookNumberList.Remove(phoneNumber);
                    Console.WriteLine("Kayıt rehberden başarıyla silindi!");
                }
                else if (s.ToLower() == "n")
                {
                    Console.WriteLine("Kayıt silme onaylanmadı!");
                    StartMethots();
                }
                else
                {
                    Console.WriteLine("Yanlış bir giriş yaptınız!");
                    StartMethots();
                }


            }
            else
            {
                NotSearchDelete();
            }



        }
        public static PhoneBookNumberModel Search(string s)
        {
            bool isExist = false;
            foreach (var item in PhoneBookListModel.phoneBookNumberList)
            {
                if (item.Name == s || item.Surname == s)
                {
                    isExist = true;
                    if (isExist)
                    {
                        return item;
                    }
                }
            }
            return null;
        }
        public static void NotSearchDelete()
        {
            Console.WriteLine("Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.");
            Console.WriteLine("* Silmeyi sonlandırmak için    : (1)");
            Console.WriteLine(" * Yeniden denemek için              : (2)");
            string number = Console.ReadLine();
            if (number == "1")
            {
                Console.WriteLine("Silme işlemi sonlandırılıyor!");
                StartMethots();
            }
            else
            {
                DeleteNumber();
            }
        }

        public static void NotSearchUpdate()
        {
            Console.WriteLine("Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.");
            Console.WriteLine("* Güncellemeyi sonlandırmak için    : (1)");
            Console.WriteLine(" * Yeniden denemek için              : (2)");
            string number = Console.ReadLine();
            if (number == "1")
            {
                Console.WriteLine("Güncelleme sonlandırılıyor!");
                StartMethots();
            }
            else
            {
                UpdateNumber();
            }
        }

        public static void UpdateNumber()
        {
            Console.WriteLine("Lütfen güncellemek istediğiniz kişinin adını veya soyadını giriniz : ");
            PhoneBookNumberModel phoneNumber = Search(Console.ReadLine());
            string[] items =
            {
                "Lütfen İsim Giriniz        : ",
                "Lütfen Soy İsim Giriniz    : ",
                "Lütfen Numara Giriniz      : "
            };
            int itemsLength = items.Length;
            string[] updatePhoneNumberModel = new string[itemsLength];
            if (phoneNumber != null)
            {
                Console.WriteLine("{0} isimli kişi güncellenmek üzere, onaylıyor musunuz ?(y/n)", phoneNumber.Name);
                string s = Console.ReadLine();
                if (s.ToLower() == "y")
                {
                    for (int i = 0; i < itemsLength; i++)
                    {
                        Console.WriteLine(items[i]);
                        items[i] = Console.ReadLine();
                    }

                    phoneNumber.Name = items[0];
                    phoneNumber.Surname = items[1];
                    phoneNumber.Number = items[2];
                    printRecord(phoneNumber);
                    Console.WriteLine("Kayıt güncelleme başarıyla tamamlandı!");

                }
                else if (s.ToLower() == "n")
                {
                    Console.WriteLine("Kayıt güncelleme onaylanmadı!");
                    goto Flag;
                }
                else
                {
                    Console.WriteLine("Yanlış bir giriş yaptınız!");
                    goto Flag;
                }
            }
            else
            {
                NotSearchUpdate();
            }
        }


        public static void printRecord(PhoneBookNumberModel phoneNumbers)
        {
            Console.WriteLine("****************************************");
            Console.WriteLine("İsim         :   {{{0}}}", phoneNumbers.Name);
            Console.WriteLine("Soyisim      :   {{{0}}}", phoneNumbers.Surname);
            Console.WriteLine("Numara       :   {{{0}}}", phoneNumbers.Number);
            Console.WriteLine("****************************************");
        }

        public static void SearchNumber()
        {
            Console.WriteLine("Arama yapmak istediğiniz tipi seçiniz.");
            Console.WriteLine("***********************************************");
            Console.WriteLine("İsim veya soyisime göre arama yapmak için: (1)");
            Console.WriteLine("Telefon numarasına göre arama yapmak için: (2)");
            int select = int.Parse(Console.ReadLine());
            if (select == 1)
            {
                Console.WriteLine("Lütfen arama yapmak istediğiniz kişinin adını ya da soyadını giriniz:");
                string name = Console.ReadLine();
                foreach (var item in PhoneBookListModel.phoneBookNumberList)
                {
                    if (item.Name.ToLower() == name.ToLower() || item.Surname.ToLower() == name.ToLower())
                    {
                        Console.WriteLine("isim: {0}", item.Name);
                        Console.WriteLine("Soyisim: {0}", item.Surname);
                        Console.WriteLine("Telefon Numarası: {0}", item.Number);
                        Console.WriteLine("-");
                    }
                }
                Console.WriteLine("Arama İşlemi Bitti, Çıkılıyor...");
            }
            else if (select == 2)
            {
                Console.WriteLine("Lütfen arama yapmak istediğiniz kişinin telefon numarasını giriniz:");
                string no = Console.ReadLine();
                foreach (var item in PhoneBookListModel.phoneBookNumberList)
                {
                    if (item.Number == no)
                    {
                        Console.WriteLine("isim: {0}", item.Name);
                        Console.WriteLine("Soyisim: {0}", item.Surname);
                        Console.WriteLine("Telefon Numarası: {0}", item.Number);
                        Console.WriteLine("-");
                    }
                }
                Console.WriteLine("Arama İşlemi Bitti, Çıkılıyor...");
            }
            else
            {
                Console.WriteLine("Hatalı Seçim, Çıkılıyor...");
            }

    }
}
