using System;
using System.Collections.ObjectModel;
using SystemLosowania.Models;
using Newtonsoft.Json.Linq;// Add this for File class
using Newtonsoft.Json;  

namespace SystemLosowania.Files
{
    public class OperacjeNaPliku
    {
        public string path { get; set; } = Path.Combine(FileSystem.Current.AppDataDirectory, "plikUczniowieKlasy.txt");
        public OperacjeNaPliku() { }

        public void Odczyt(ObservableCollection<ListaUczniów> ListaUczniów, SzczesliwyNumerek szczesliwyNumerek)
        {
            string line;
            int temp = 0;
            StreamReader reader = null;

            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path);
                    return;
                }

                reader = new StreamReader(path);
                while ((line = reader.ReadLine()) != null)
                {
                    var data = JObject.Parse(line);


                    if (temp == 0)
                    {
                        DateTime dataWylosowania = DateTime.Now;

                        if (DateTime.TryParse(data["Data"]?.ToString(), out DateTime d))
                        {
                            dataWylosowania = d;
                        }

                        int szczesliwyNumer = 1;

                        if (Int32.TryParse(data["SzczesliwyNumer"]?.ToString(), out int sn))
                        {
                            szczesliwyNumer = sn;
                        }

                        szczesliwyNumerek.DataWylosowania = dataWylosowania;
                        szczesliwyNumerek.Numerek = szczesliwyNumer;

                        temp = 1;
                    }
                    else
                    {
                        string imie = data["Imie"]?.ToString() ?? "";
                        string klasa = data["klasa"]?.ToString() ?? "";
                        string nazwisko = data["nazwisko"]?.ToString() ?? "";
                        int numer = 10;
                        bool czyZostalaOdpytana = false;
                        int liczbaOsobWylosowanychPoOdpytaniu = 0;

                        if (Int32.TryParse(data["Numer"]?.ToString(), out int n))
                        {
                            numer = n;
                        }

                        if (Boolean.TryParse(data["CzyZostalaOdpytana"]?.ToString(), out bool czo))
                        {
                            czyZostalaOdpytana = czo;
                        }

                        if (Int32.TryParse(data["LiczbaOsobWylosowanychPoOdpytaniu"]?.ToString(), out int lowpo))
                        {
                            liczbaOsobWylosowanychPoOdpytaniu = lowpo;
                        }

                       ListaUczniów uczen = new ListaUczniów
                       {
                           CzyZostałaOdpytana = czyZostalaOdpytana,
                           Imie = imie,
                           Klasa = klasa,
                           Nazwisko = nazwisko,
                           LiczbaOsobWylosowanychPoOdpytaniu = liczbaOsobWylosowanychPoOdpytaniu,
                           Numerek = numer,
                           Obecny = true
                       };

                        ListaUczniów.Add(uczen);
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or processing file: {ex.Message}");
            }
            finally
            {
                reader.Close();
            }

        }


        public  void Zapis(ObservableCollection<ListaUczniów> ListaUczniów, SzczesliwyNumerek szczesliwyNumerek)
        {
            try
            {
                List<string> elements = new List<string> { $"{{\"Data\":\"{szczesliwyNumerek.DataWylosowania.ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzz")}\", \"SzczesliwyNumer\":\"{szczesliwyNumerek.Numerek}\"}}" };

                foreach (var uczen in ListaUczniów)
                {
                    string uczenString = $@"{{""Imie"": ""{uczen.Imie}"", ""nazwisko"": ""{uczen.Nazwisko}"", ""klasa"": ""{uczen.Klasa}"", ""Numer"": ""{uczen.Numerek}"", ""CzyZostalaOdpytana"": ""{uczen.CzyZostałaOdpytana}"", ""LiczbaOsobWylosowanychPoOdpytaniu"": ""{uczen.LiczbaOsobWylosowanychPoOdpytaniu}""}}";
                    elements.Add(uczenString);
                }

                File.WriteAllLinesAsync(path, elements);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }
    }
}

