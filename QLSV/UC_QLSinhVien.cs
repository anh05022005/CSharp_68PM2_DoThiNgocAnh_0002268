using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class UC_QLSinhVien : UserControl
    {
        databaseDataContext db = new databaseDataContext("Data Source=LAPTOP-I8G20P67\\DNA;Initial Catalog=quanlysv;User ID=sa;Password=Ngocanh2005@;TrustServerCertificate=True");
        public UC_QLSinhVien()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void UC_QLSinhVien_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadDSLH4CBX();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string mSSV = txtMSSV.Text;
            string hoTen = txtHoTen.Text;
            string gioiTinh = cboGioiTinh.Text;
            tbl_sinhvien sinhvien = new tbl_sinhvien();
            sinhvien.id = mSSV;
            sinhvien.hoten = hoTen;
            sinhvien.gioitinh = gioiTinh;
            sinhvien.ngaysinh = dtpNgaySinh.Value.Date;
            sinhvien.malop = cbx_lop.SelectedValue.ToString();
            try
            {
                db.tbl_sinhviens.InsertOnSubmit(sinhvien);
                db.SubmitChanges();
                MessageBox.Show("Thêm mới sinh viên thành công");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadData()
        {
            List<tbl_sinhvien> dSSV = db.tbl_sinhviens.ToList();
            dgv_DSSV.DataSource = dSSV;
        }

        public void LoadDSLH4CBX()
        {
            List<tbl_lophoc> dSLH = db.tbl_lophocs.ToList();
            cbx_lop.DataSource = dSLH;
            cbx_lop.DisplayMember = "tenlop";
            cbx_lop.ValueMember = "malop";
        }
    }
}
