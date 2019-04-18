using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.PretplacenKlasa
{
    public class Pretplata<T> : INotifyPropertyChanged
    {
        public void PretplatiSeNaPromenuSvojstva<T>(ref T polje, T value, [CallerMemberName] string promenjenoPolje = "")
        {
            polje = value;
            this.PropertyChanged(this, new PropertyChangedEventArgs(promenjenoPolje));
        }
        public event PropertyChangedEventHandler PropertyChanged =  delegate { };
    }
}
