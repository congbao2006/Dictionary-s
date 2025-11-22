namespace DictionaryUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.FlowLayoutPanel flowWords;
        private System.Windows.Forms.Panel panelMeaning;
        private System.Windows.Forms.Label lblSyn;
        private System.Windows.Forms.Label lblAnt;
        private System.Windows.Forms.Label lblWord;
        private System.Windows.Forms.Label lblMeaning;
        
        // KHAI BÁO MỚI
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flowWords = new System.Windows.Forms.FlowLayoutPanel();
            this.panelMeaning = new System.Windows.Forms.Panel();
            this.lblWord = new System.Windows.Forms.Label();
            this.lblMeaning = new System.Windows.Forms.Label();
            this.lblSyn = new System.Windows.Forms.Label();
            this.lblAnt = new System.Windows.Forms.Label();
            
            // KHỞI TẠO MỚI
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            
            this.panelMeaning.SuspendLayout();
            this.SuspendLayout();

            // flowWords
            this.flowWords.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowWords.Width = 250;
            this.flowWords.AutoScroll = true;
            this.flowWords.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowWords.WrapContents = false;     // 1 column only

            // txtSearch (Thanh tìm kiếm)
            this.txtSearch.Location = new System.Drawing.Point(20, 20);
            this.txtSearch.Size = new System.Drawing.Size(400, 26);
            
            // btnSearch (Nút Tìm kiếm)
            this.btnSearch.Location = new System.Drawing.Point(430, 20);
            this.btnSearch.Size = new System.Drawing.Size(100, 26);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click); // Gắn sự kiện

            // panelMeaning
            this.panelMeaning.Dock = System.Windows.Forms.DockStyle.Fill;
            
            // THÊM CÁC CONTROL MỚI
            this.panelMeaning.Controls.Add(this.txtSearch);
            this.panelMeaning.Controls.Add(this.btnSearch);
            
            // CÁC CONTROL CŨ
            this.panelMeaning.Controls.Add(this.lblWord);
            this.panelMeaning.Controls.Add(this.lblMeaning);
            this.panelMeaning.Controls.Add(this.lblSyn);
            this.panelMeaning.Controls.Add(this.lblAnt);

            // lblWord (Đẩy xuống dưới thanh Search)
            this.lblWord.AutoSize = true;
            this.lblWord.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWord.Location = new System.Drawing.Point(20, 70);
            this.lblWord.Text = "";

            // lblMeaning
            this.lblMeaning.AutoSize = true;
            this.lblMeaning.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblMeaning.Location = new System.Drawing.Point(20, 120); // Đẩy xuống hàng 120
            this.lblMeaning.Text = "Meaning (Definition, Description, Example):";
            this.lblMeaning.MaximumSize = new System.Drawing.Size(550, 0); // Giới hạn chiều rộng
            
            // lblSyn
            this.lblSyn.AutoSize = true;
            this.lblSyn.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSyn.Location = new System.Drawing.Point(20, 300); // Đẩy xuống hàng 300
            this.lblSyn.Text = "Synonyms:";

            // lblAnt
            this.lblAnt.AutoSize = true;
            this.lblAnt.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblAnt.Location = new System.Drawing.Point(20, 340); // Đẩy xuống hàng 340
            this.lblAnt.Text = "Antonyms:";

            // Form1
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelMeaning);
            this.Controls.Add(this.flowWords);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Text = "Dictionary";
            this.panelMeaning.ResumeLayout(false);
            this.panelMeaning.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}