using System;
using System.Windows.Forms;
using DictionaryUI; // Dùng namespace chứa Form1

namespace DictionaryApp // Thay thế bằng Namespace gốc của dự án bạn
{
    static class Program
    {
        /// <summary>
        /// Điểm khởi tạo chính cho ứng dụng.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Dòng này khởi chạy Form chính của bạn
            Application.Run(new Form1()); 
        }
    }
}