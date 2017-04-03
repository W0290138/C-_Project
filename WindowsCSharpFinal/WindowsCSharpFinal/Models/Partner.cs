using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsCSharpFinal.Models
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        private int _Happiness;
        public int Happiness
        {
            get
            {
                return _Happiness;
            }
            set
            {
                _Happiness = value;
                if (_Happiness > 100)
                    _Happiness = 100;
                else if (_Happiness < 0)
                    _Happiness = 0;
            }
        }
    }
}
