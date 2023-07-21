using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace ProjeDeneme2
{


    
   
   

    class Program
    {
        static void Main()
        {

            //kök öge yani ana ögemi(ana öğe başlangıç tagimizdir.xmlde ana öge bulunmak zorundadır.) fonksyonuma yazdırarak işleme başlıyorum.
            //işlemi konsola yazarken aynı zamanca txt ascii formatında txt dosyasına yazdırıyorum.
            //dosyalarımı tanıttım ve yükleme işlemini gerçekleştirdim.

            string outputFile = "farukOral.txt";
            string output = "farukOral.txt";
            File.WriteAllText(outputFile, string.Empty);


           
            string livePath = "GetLiveSportsLive.xml";
            string testPath = "GetLiveSportsTest.xml";

            XDocument liveDoc = XDocument.Load(livePath);
            XDocument testDoc = XDocument.Load(testPath);



           
         

            CompareXml(liveDoc.Root, testDoc.Root, output);

            Console.WriteLine("Karşılaştırma sonucu dosyaya yazıldı: " + output);


            Console.ReadLine();
        }


        static void CompareXml(XElement liveElement, XElement testElement, string outputFile)
        {
            
            //tag isimlerini buldum
            if (liveElement.Name != testElement.Name)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Farklı etiket adı : {0} - {1}", liveElement.Name, testElement.Name);
                string outputString = string.Format("Farklı etiket adı : {0} - {1}", liveElement.Name, testElement.Name);

                
                StringBuilder asciiValues = new StringBuilder();             
                foreach (char c in outputString)
                {
                    int asciiValue = (int)c;
                    asciiValues.Append(asciiValue + " ");
                }
                File.AppendAllText(outputFile, asciiValues.ToString());

            }



            //burada taglerin içerikleri buldum
            if (liveElement.Value != testElement.Value)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Farklı içerik : {0} - {1}", liveElement.Value, testElement.Value);
                string outputString = string.Format("Farklı içerik : {0} - {1}", liveElement.Value, testElement.Value);

               
                StringBuilder asciiValues = new StringBuilder();
                foreach (char c in outputString)
                {
                    int asciiValue = (int)c;
                    asciiValues.Append(asciiValue + " ");
                }
                File.AppendAllText(outputFile, asciiValues.ToString());
                
            }
         


            //burada nitelikleri yani tag içinde yazılan özellikleri bulup liste haline getirdim. özellik sayıları farklı ise farklı özellik sayısı olduğunu belirtiyor. 
            //eğer aynı ise bu sefer özellikleri kontrol ederek farklı özellikler var mı diye kontrol ediyor.
            var liveAttributes = liveElement.Attributes().ToList();
            var testAttributes = testElement.Attributes().ToList();
            
            if (liveAttributes.Count != testAttributes.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Farklı nitelik sayısı: {0} - {1}", liveAttributes.Count, testAttributes.Count);
                string outputString = string.Format("Farklı nitelik sayısı: {0} - {1}", liveAttributes.Count, testAttributes.Count);

                StringBuilder asciiValues = new StringBuilder();
                foreach (char c in outputString)
                {
                    int asciiValue = (int)c;
                    asciiValues.Append(asciiValue + " ");
                }

                File.AppendAllText(outputFile, asciiValues.ToString());

               


            }
            else
            {
                for (int i = 0; i < liveAttributes.Count; i++)
                {
                    if (liveAttributes[i].Value != testAttributes[i].Value)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Farklı nitelik : {0} - {1}", liveAttributes[i].Value, testAttributes[i].Value);
                        string outputString = string.Format("Farklı nitelik : {0} - {1}", liveAttributes[i].Value, testAttributes[i].Value);

                        StringBuilder asciiValues = new StringBuilder();
                        foreach (char c in outputString)
                        {
                            int asciiValue = (int)c;
                            asciiValues.Append(asciiValue + " ");
                        }

                        File.AppendAllText(outputFile, asciiValues.ToString());

                       


                    }
                 
                }
            }

            //burada alt ögeler yani tag içindeki tagleri buldum. ve değişkene atadım
            var liveAltOge = liveElement.Elements().ToList();
            var testAltOge = testElement.Elements().ToList();
            
            
            if (liveAltOge.Count != testAltOge.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("farklı alt öge sayisi: {0}-{1}",liveAltOge.Count, testAltOge.Count);
                string outputString = string.Format("farklı alt öge sayisi: {0}-{1}", liveAltOge.Count, testAltOge.Count);

                StringBuilder asciiValues = new StringBuilder();
                foreach (char c in outputString)
                {
                    int asciiValue = (int)c;
                    asciiValues.Append(asciiValue + " ");
                }

                File.AppendAllText(outputFile, asciiValues.ToString());

            } 
            else
            {
                for (int i = 0; i < liveAltOge.Count; i++)
                {
                    CompareXml(liveAltOge[i], testAltOge[i],outputFile);

                }

            }
            
            

        }

    }



}