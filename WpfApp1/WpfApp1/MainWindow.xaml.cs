using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Windows;
using System.Windows.Controls;

#nullable disable

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<KhachHang> list = new List<KhachHang>();

        public MainWindow()
        {
            InitializeComponent();

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
        }


        private void On_Them(object sender, RoutedEventArgs e)
        {
        }

        private void On_Window2(object sender, RoutedEventArgs e)
        {

        }


        private void OnThem(object sender, RoutedEventArgs e)
        {
            if(tbMa.Text == "")
            {
                error("chua nhap ma ");
                return;
            }

            if (tbSoLuong.Text == "")
            {
                error("chua nhap so luong ");
                return;
            }

            if (tbDonGia.Text == "")
            {
                error("chua nhap don gia ");
                return;
            }

            var ma = tbMa.Text;
            if(list.Find(kh => kh.MaKH == ma) != null)
            {
                error("trung ma");
                return;
            }

            var ngay = dpDate.SelectedDate.Value;

            if (ngay > DateTime.Now)
            {
                error("ngay phai nho hon hoac bang hien tai");
                return;
            }

            if(!float.TryParse(tbDonGia.Text, out float dg) || !int.TryParse(tbSoLuong.Text, out int sl))
            {
                error("ko hop le, phai la so");
                return;
            }

            var khach = new KhachHang(ma, ngay, sl, dg);

            list.Add(khach);

            grid.ItemsSource = null;
            grid.ItemsSource = list;

        }

        private void Sua(object sender, RoutedEventArgs e)
        {
            var w2 = new Window1();

            var q = grid.SelectedItems;

            w2.w2(q);
            w2.Show();
        }

        void error(string message)
        {
            MessageBox.Show(message, "loi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public record class KhachHang(string MaKH, DateTime NgayMua, int SoLuongMua, float DonGIa)
    {
        public float ThanhTien => SoLuongMua * DonGIa;
    }
}
