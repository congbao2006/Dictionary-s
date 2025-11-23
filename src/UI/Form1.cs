using System;
using System.Windows.Forms;
using Dictionary.Services;
using System.Linq;

namespace DictionaryUI
{
    public partial class Form1 : Form
    {
        // Khai báo 2 đường dẫn file
        private string synAntPath = "/Users/bao/Desktop/Dictionary-main/SynAntWordData.txt"; // Giả định
        private string mainDictPath = "/Users/bao/Desktop/Dictionary-main/MeaningWordData.txt"; // Giả định

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

            // Lấy danh sách từ từ DictionaryService
            foreach (var word in DictionaryService.dictionary.Keys.OrderBy(w => w))
            {
                Button btn = new Button();
                btn.Text = word;
                btn.Width = flowWords.Width - 25;
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
            word = word.Trim().ToLower(); // Chuẩn hóa từ khóa

            // 1. Kiểm tra từ có tồn tại trong từ điển chính không
            if (!DictionaryService.dictionary.ContainsKey(word))
            {
                lblWord.Text = $"Từ: {word}";
                lblMeaning.Text = "Không tìm thấy định nghĩa cho từ này.";
                lblSyn.Text = "Synonyms: N/A";
                lblAnt.Text = "Antonyms: N/A";
                return;
            }

            // 2. Lấy Định nghĩa (Meaning, Description, Example)
            string meaning = DictionaryService.Search(word);

            // 3. Lấy Từ đồng/trái nghĩa
            string syn = SynAntDictionary.SearchSyn(word);
            string ant = SynAntDictionary.SearchAnt(word);

            // 4. Hiển thị lên Labels
            lblWord.Text = word;
            
            // Xóa prefix "Từ: ..." từ kết quả trả về của DictionaryService.Search 
            // và thay thế các ký tự xuống dòng
            string cleanMeaning = meaning.Replace($"Từ: {word}\n", "").Replace("\n", Environment.NewLine);
            lblMeaning.Text = "Meaning:\n" + cleanMeaning;

            lblSyn.Text = "Synonyms: " + syn;
            lblAnt.Text = "Antonyms: " + ant;
        }
        
        // PHƯƠNG THỨC XỬ LÝ SỰ KIỆN TÌM KIẾM MỚI
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchWord = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchWord))
            {
                ShowMeaning(searchWord);
            }
        }
    }
}
