using System;
using System.Windows.Forms;
using Dicktionary.Services;
using System.Linq; // Cần dùng cho các Dictionary Keys

namespace DictionaryUI
{
    public partial class Form1 : Form
    {
        // Khai báo 2 đường dẫn file
        private string synAntPath = "Dictionary-main/src/Data/SynAntWordData.txt";
        private string mainDictPath = "Dictionary-main/src/Data/MeaningWordData.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load từ điển chính (Meaning)
            DictionaryService.LoadFromFile(mainDictPath);

            // Load từ điển đồng/trái nghĩa
            SynAntDictionary.LoadFromFile(synAntPath);

            LoadWordButtons();
        }

        private void LoadWordButtons()
        {
            flowWords.Controls.Clear();

            // Lấy danh sách từ từ DictionaryService (đảm bảo hiển thị từ có định nghĩa)
            foreach (var word in DictionaryService.dictionary.Keys.OrderBy(w => w))
            {
                Button btn = new Button();
                btn.Text = word;
                btn.Width = flowWords.Width - 25;   // full width button
                btn.Height = 40;
                btn.Margin = new Padding(5);

                btn.Click += (s, e) =>
                {
                    ShowMeaning(word);
                };

                flowWords.Controls.Add(btn);
            }
        }

        private void ShowMeaning(string word)
        {
            // 1. Lấy Định nghĩa (Meaning, Description, Example)
            string meaning = DictionaryService.Search(word.ToLower()); // Gọi service chính

            // 2. Lấy Từ đồng/trái nghĩa
            string syn = SynAntDictionary.SearchSyn(word.ToLower());
            string ant = SynAntDictionary.SearchAnt(word.ToLower());

            // 3. Hiển thị lên Labels
            lblWord.Text = word;
            
            // Xóa prefix "Từ: ..." từ kết quả trả về của DictionaryService.Search 
            // và thay thế các ký tự xuống dòng
            string cleanMeaning = meaning.Replace($"Từ: {word}\n", "").Replace("\n", Environment.NewLine);
            lblMeaning.Text = "Meaning:\n" + cleanMeaning; 

            lblSyn.Text = "Synonyms: " + syn;
            lblAnt.Text = "Antonyms: " + ant;
        }
    }
}