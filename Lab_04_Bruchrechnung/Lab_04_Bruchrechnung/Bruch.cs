using System;

namespace Lab_04_Bruchrechnung
{
    class Bruch 
    {
        private int zaehler;
        private int nenner;

        public Bruch(int zaehler, int nenner)
        {
            this.zaehler = zaehler;
            this.nenner = nenner;
        }

        public int getZaehler()
        {
            return zaehler;
        }

        public override string ToString()
        {
            return $"{zaehler} / {nenner}";
        }

        public int getNenner()
        {
           return nenner;
        }

        public void setZaehler(int zaehler)
        {
            this.zaehler = zaehler;
        }

        public void setNenner(int nenner)
        {
            if(nenner == 0)
            {
                throw new Exception();
            }
            this.nenner = nenner;
        }

        public void Kuerzen()
        {
            // 28-35

            int kleinster = Math.Min(zaehler, nenner);
            for (int i = kleinster; i > 1; i--)
            {
                if(zaehler % i == 0 && nenner % i == 0)
                {
                    zaehler /= i;
                    nenner /= i;
                    break;
                }
            }
        }

        public static Bruch Parse(string str)
        {
            string[] teile = str.Split('/');
            int zaehler = int.Parse(teile[0]);
            int nenner = int.Parse(teile[1]);
            return new Bruch(zaehler, nenner);
        }

        public void Add(Bruch b)
        {
            int n = this.nenner * b.getNenner();
            int z = this.zaehler * b.getNenner() + b.getZaehler() * this.nenner;
            this.zaehler = z;
            this.nenner = n;
            Kuerzen();
        }

        public void Subtract(Bruch b)
        {
            int n = this.nenner * b.getNenner();
            int z = this.zaehler * b.getNenner() - b.getZaehler() * this.nenner;
            this.zaehler = z;
            this.nenner = n;
            Kuerzen();
        }   

        public void Multiply(Bruch b)
        {
            int n = this.nenner * b.getNenner();
            int z = this.zaehler * b.getZaehler();
            this.zaehler = z;
            this.nenner = n;
            Kuerzen();
        }

        public void Divide(Bruch b)
        {
            int n = this.nenner * b.getZaehler();
            int z = this.zaehler * b.getNenner();
            this.zaehler = z;
            this.nenner = n;
            Kuerzen();
        }
    }
}
