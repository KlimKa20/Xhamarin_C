using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace App8
{
	public class HomePageViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<newtrain> People { get; private set; }

		public ICommand OutputAgeCommand { get; private set; }

		public string SelectedItemText { get; private set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public HomePageViewModel ()
		{
            string pain = "https://rasp.rw.by/ru/route/?from=Минск-Пассажирский&from_exp=2100001&from_esr=&to=Гомель&to_exp=2100100&to_esr=&date=2019-10-02";
            string id, type, starttime, startdate, finishtime, finishdate, totaltime, typeplace, count, price, Check = "В наличии";
            ObservableCollection<TYPEplace> trainss;
            while (true)
            {
                Check = "В наличии";
                id = find(ref pain, "<small class=\"train_id\">", "</small>");
                if (id == "-1")
                    break;
                type = find(ref pain, "<div class=\"train_description\">", "</div> ");
                starttime = find(ref pain, "<b class=\"train_start-time\">", "</b>");
                startdate = "2019-10-02";
                finishtime = find(ref pain, "<b class=\"train_end-time\">", "</b>");
                finishdate = find(ref pain, "&date=", "\" class=\"");
                totaltime = find(ref pain, "<span class=\"train_time-total\">", "</span>");
                trainss = new ObservableCollection<TYPEplace>();
                while (true)
                {

                    count = "0";
                    price = "0";
                    typeplace = find(ref pain, "<li class=\"train_note\">", "</li>");
                    if (typeplace == "-1")
                        break;
                    count = find(ref pain, "&car_type=", "</a>");
                    price = find(ref pain, "class=\"train_price\"><span>", "<span>");
                    trainss.Add(new TYPEplace { Typeplace = typeplace, Count = count, Price = price });
                }
                string[] rt = starttime.Split(new char[] { ':' });

                People.Add(new newtrain { Id = id, Type = type, Starttime = starttime, Startdate = startdate, Finishdate = finishdate, Finishtime = finishtime, Totaltime = totaltime,/* Trains = trainss,*/ check = Check });
            }
            //         People = new ObservableCollection<newtrain>() ;
            OutputAgeCommand = new Command<newtrain>(OutputAge);
        }
        static string find(ref string pain, string strbegin, string strend)
        {
            string temp = "";
            int n = pain.IndexOf(strbegin);
            if (n > pain.IndexOf("</tr><!-- // b-train -->") && strend == "</li>")
            {
                string b = "</tr><!-- // b-train -->";
                pain = pain.Remove(pain.IndexOf(b), b.Length);
                return "-1";
            }
            if (n == -1)
                return "-1";
            if (strbegin == "&car_type=")
                pain = pain.Remove(0, n + strbegin.Length + 3);
            else
                pain = pain.Remove(0, n + strbegin.Length);
            n = 0;
            while (pain[n] != strend[0])
            {
                temp += pain[n];
                n++;
            }
            pain = pain.Remove(n, strend.Length);
            return temp;
        }

        void OutputAge(newtrain person)
        {
            SelectedItemText = string.Format("{0} is {1} years old.", person.Id, person.Startdate);
            OnPropertyChanged("SelectedItemText");
        }

        protected virtual void OnPropertyChanged (string propertyName)
		{
			var changed = PropertyChanged;
			if (changed != null)
            {
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}
	}
}
