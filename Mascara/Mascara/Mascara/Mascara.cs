using System.Collections.Generic;
using Xamarin.Forms;

namespace Mascara.Mascara
{
    public class MascaraBehavior : Behavior<Entry>
    {
        private string _Mascara = "";
        public string Mascara
        {
            get => _Mascara;
            set
            {
                _Mascara = value;
                SetPositions();
            }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        IDictionary<int, char> _Posicoes;

        void SetPositions()
        {
            if (string.IsNullOrEmpty(Mascara))
            {
                _Posicoes = null;
                return;
            }

            var list = new Dictionary<int, char>();

            for (var i = 0; i < Mascara.Length; i++)
                if (Mascara[i] != 'X')
                    list.Add(i, Mascara[i]);

            _Posicoes = list;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;

            var text = entry.Text;

            if (string.IsNullOrWhiteSpace(text) || _Posicoes == null)
                return;

            if (text.Length > _Mascara.Length)
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            foreach (var position in _Posicoes)
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();

                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }

            if (entry.Text != text)
                entry.Text = text;
        }
    }
}