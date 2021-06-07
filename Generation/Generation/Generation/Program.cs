using System;
using System.IO;

namespace Generation
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            StreamReader sr;
            StreamWriter sw;
            string line;
            string[] lines;
            if (s == "1")
            {
                sr = new StreamReader("countries1.txt");
                sw = new StreamWriter("dirquery.txt", false);
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length < 31)
                        sw.WriteLine(String.Format("INSERT INTO countries(country) VALUES ('{0}');", line));
                }
                sr.Close();
                sr = new StreamReader("cities1.txt");
                string city;
                int country = 0;
                while ((city = sr.ReadLine()) != null)
                {
                    country++;
                    sw.WriteLine(String.Format("INSERT INTO cities(city, c_id) VALUES ('{0}',{1});", city, country));
                    if (country == 204)
                        country = 0;
                }
                sr.Close();
                sr = new StreamReader("authors.txt");
                while ((line = sr.ReadLine()) != null)
                {
                    sw.WriteLine(String.Format("INSERT INTO authors(author) VALUES ('{0}');", line));
                }
                sr.Close();
                sr = new StreamReader("exhibit_types1.txt");
                while ((line = sr.ReadLine()) != null)
                {
                    sw.WriteLine(String.Format("INSERT INTO exhibit_types(type) VALUES ('{0}');", line));
                }
                sr.Close();
                sr = new StreamReader("exhibition_types1.txt");
                while ((line = sr.ReadLine()) != null)
                {
                    sw.WriteLine(String.Format("INSERT INTO exhibition_types(type) VALUES ('{0}');", line));
                }
                sr.Close();
                sr = new StreamReader("exhibition_aims.txt");
                while ((line = sr.ReadLine()) != null)
                {
                    sw.WriteLine(String.Format("INSERT INTO exhibition_aims(aim) VALUES ('{0}');", line));
                }
                sr.Close();
                sr = new StreamReader("organizers.txt");
                while ((line = sr.ReadLine()) != null)
                {
                    sw.WriteLine(String.Format("INSERT INTO organizers(organizer) VALUES ('{0}');", line));
                }
                sr.Close();
                sw.Close();
            }
            else if (s == "2")
            {
                int count = 0;
                Random rand = new Random();
                string name;
                sr = new StreamReader("museums.txt");
                sw = new StreamWriter("museumsquery.txt", false);
                for(count=0;count<2000;count++)
                {
                    if ((name = sr.ReadLine()) == null) 
                    {
                        sr.BaseStream.Position = 0;
                        name = sr.ReadLine();
                    }
                    else if(name.Length>50)
                    {
                        count--;
                        continue;
                    }
                    line=String.Format("INSERT INTO museums(museum, c_id,year,phone,private) VALUES ('{0}',{1},{2},'{3}',{4});",
                        name, rand.Next(1, 551), rand.Next(1701, 2021), rand.Next(100000000, 1000000000), Convert.ToBoolean(rand.Next(0, 2)));
                    sw.WriteLine(line);
                }
                sr.Close();
                sw.Close();
                sr = new StreamReader("exhibits1.txt");
                sw = new StreamWriter("exhibitsquery.txt", false);
                StreamReader comsr = new StreamReader("comments.txt");
                string comment;
                for (count = 0; count < 3000; count++)
                {
                    if ((name = sr.ReadLine()) == null)
                    {
                        sr.BaseStream.Position = 0;
                        name = sr.ReadLine();
                    }
                    else if (name.Length > 50)
                    {
                        count--;
                        continue;
                    }
                    if ((comment = comsr.ReadLine()) == null)
                    {
                        comsr.BaseStream.Position = 0;
                        comment = comsr.ReadLine();
                    }
                    line = String.Format("INSERT INTO exhibits(exhibit, tp_id, auth_id, agecreate, " +
                    "transfer, money, comment, restoration, mus_id) VALUES ('{0}',{1},{2},{3},{4},{5},'{6}',{7},{8});",
                    name, rand.Next(1,7), rand.Next(1, 124), rand.Next(15, 21), rand.Next(1901, 2022), rand.Next(1001, 999999999),
                    comment, rand.Next(1901, 2022), rand.Next(1, 2001)) ;
                    sw.WriteLine(line);
                }
                sr.Close();
                sw.Close();
                DateTime start = new DateTime(1990, 1, 2);
                DateTime end = new DateTime(1990, 1, 2);
                int range = (DateTime.Today - start).Days;
                sr = new StreamReader("exhibitions.txt");
                sw = new StreamWriter("exhibitionsquery.txt", false);
                for (count = 0; count < 2000; count++)
                {
                    if ((name = sr.ReadLine()) == null)
                    {
                        sr.BaseStream.Position = 0;
                        name = sr.ReadLine();
                    }
                    do
                    {
                        start = new DateTime(1990, 1, 2);
                        end = new DateTime(1990, 1, 2);
                        double days = rand.Next(range);
                        start = start.AddDays(days);
                        end = end.AddDays(days + rand.Next(1,32));
                    } while (end > DateTime.Today);
                    line = String.Format("INSERT INTO exhibitions(title, c_id, date_start, date_finish, account, tp_id, org_id," +
                    "a_id, interpreters, stand, transportation, marketing) VALUES('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},{11});",
                    name, rand.Next(1, 551), start.ToShortDateString(), end.ToShortDateString(), rand.Next(0,1000000), rand.Next(1,9)
                    , rand.Next(1, 499), rand.Next(1, 51), rand.Next(10001, 10000001), rand.Next(1001, 10000001), 
                    rand.Next(20001, 5000000), rand.Next(10001, 5000000));
                    sw.WriteLine(line);
                }
                sr.Close();
                sw.Close();
                sw = new StreamWriter("partquery.txt", false);
                for (count = 0; count < 3000; count++)
                {
                    line = String.Format("INSERT INTO participation(ext_id, exn_id) VALUES ({0},{1});",
                    rand.Next(1,3001), rand.Next(1, 2001));
                    sw.WriteLine(line);
                }
                sr.Close();
                sw.Close();
            }
            else if (s == "3")
            {

            }
        }
    }
}
