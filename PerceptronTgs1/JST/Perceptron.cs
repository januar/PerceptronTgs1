using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JST
{
    class Perceptron
    {
        public List<Pattern> ListPattern = new List<Pattern>();
        string patternPath = "F:\\Kuliah\\JST\\Tugas\\Januar\\PerceptronTgs1\\PerceptronTgs1\\Pattern\\";

        int alpa = 1;
        float threshold = 0.5f;

        public float[] wakhir;

        public Perceptron()
        {
            ListPattern = new List<Pattern>();

            ListPattern.Add(new Pattern(1, patternPath + "pattern1.txt"));
            ListPattern.Add(new Pattern(-1, patternPath + "pattern2.txt"));
            ListPattern.Add(new Pattern(-1, patternPath + "pattern3.txt"));
            ListPattern.Add(new Pattern(1, patternPath + "pattern4.txt"));
            ListPattern.Add(new Pattern(-1, patternPath + "pattern5.txt"));
            ListPattern.Add(new Pattern(-1, patternPath + "pattern6.txt"));

            wakhir = new float[63];
        }

        public void Training()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(SetHeaderTable());
            int countEpoch = 1;
            while(isLearn())
            {
                Console.WriteLine("\n\nEpoch Ke " + countEpoch);
                sb.Append("\nEpoch " + countEpoch + "\n");
                foreach(var item in ListPattern)
                {
                    for (int i = 0; i < item.x.Length; i++)
                    {
                        //menghitung nilai net
                        item.net = item.net + (item.x[i] * wakhir[i]);
                        sb.Append(item.x[i] + ",");
                    }
                    // menambahkan f.net dengan nilai bobot bias
                    item.net = item.net + item.b;
                    sb.Append(1 + "," + item.t + "," + item.net + ",");

                    //menghitung nilai y atau f(net)
                    if (item.net > threshold)
                    {
                        item.fnet = 1;

                    }
                    else if (item.net >= 0 && item.net <= threshold)
                    {
                        item.fnet = 0;

                    }
                    else
                    {
                        item.fnet = -1;

                    }
                    sb.Append(item.fnet + ",");
                    //menentukan apabila output tidak sesuai target maka nilai variabel belajar diset true
                    if (item.fnet != item.t)
                    {
                        HitungPerubahanBobot(item, alpa, sb);
                    }
                    sb.Append("\n");
                }

                Console.WriteLine("\nNilai Bias tiap Patern");
                for (int i = 0; i < 6; i++)
                {
                    //Menampilkan nilai Bias tiap pattern
                    Console.WriteLine("Pattern" + (i + 1) + " : " + ListPattern[i].b);
                }

                Console.WriteLine("\nNilai Net tiap Patern");
                for (int i = 0; i < 6; i++)
                {
                    //Menampilkan nilai Net tiap pattern
                    Console.WriteLine("Pattern" + (i + 1) + " : " + ListPattern[i].net);
                }

                Console.WriteLine("\nNilai y atau f(net) tiap pattern");
                for (int i = 0; i < 6; i++)
                {
                    //Menampilkan nilai y tiap pattern
                    Console.WriteLine("Pattern" + (i + 1) + " : " + ListPattern[i].fnet);
                }
                Console.WriteLine("nilai target = | 1 | -1 | -1 | 1 | -1 | -1 |");

                countEpoch++;
            }

            WriteToFile(sb);
        }

        public bool isLearn()
        {
            foreach (var item in ListPattern)
            {
                if (item.t != item.fnet)
                    return true;
            }
            return false;
        }

        public void HitungPerubahanBobot(Pattern item, float alpha, StringBuilder sb)
        {
            string delta = "";
            string bobotAkhir = "";
            for (int i = 0; i < item.x.Length; i++)
            {
                float deltaW = item.x[i] * item.t * alpha;
                wakhir[i] = wakhir[i] + deltaW;
                delta += deltaW + ",";
                bobotAkhir += wakhir[i] + ",";
            }
            float deltaWBias = 1 * item.t * alpha;
            item.b = item.b + deltaWBias; //menghitung perubahan bobot bias

            delta += deltaWBias + ",";
            bobotAkhir += item.b + ",";
            sb.Append(delta + bobotAkhir);
        }

        public string SetHeaderTable()
        {
            string item = "";
            string deltaW = "";
            string newW = "";
            for (int i = 1; i <= 63; i++)
            {
                item += "x" + i + ",";
                deltaW += "dw" + i + ",";
                newW += "w" + i + ",";
            }
            item += 1 + ",";
            deltaW += "db,";
            newW += "b,";
            return (item + "t,net,y=f(net)," + deltaW + newW);
        }

        public void WriteToFile(StringBuilder sb)
        {
            try
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("PerceptronTgs1.csv"))
                {
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception ex) { }
        }
    }
}
