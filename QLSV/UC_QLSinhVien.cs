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
        string _selectedMaSV;
        List<tbl_sinhvien> _allData;
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
        private void btn_them_Click(object sender, EventArgs e)
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
            try
            {
                _allData = db.tbl_sinhviens
                             .OrderBy(sv => sv.id)
                             .ToList();
                dgv_DSSV.DataSource = _allData
                    .Select(sv => new
                    {
                        sv.id,
                        sv.hoten,
                        sv.gioitinh,
                        sv.ngaysinh,
                        sv.malop
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyPaging()
        {

        }

        public void LoadDSLH4CBX()
        {
            List<tbl_lophoc> dSLH = db.tbl_lophocs.ToList();
            cbx_lop.DataSource = dSLH;
            cbx_lop.DisplayMember = "tenlop";
            cbx_lop.ValueMember = "malop";
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {

        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {

        }

        private void btn_lammoi_Click(object sender, EventArgs e)
        {

        }

        private void dgv_DSSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var row = dgv_DSSV.Rows[e.RowIndex];

            _selectedMaSV = row.Cells["id"].Value.ToString();
            txtMSSV.Text = _selectedMaSV;
            txtHoTen.Text = row.Cells["hoten"].Value.ToString();
            cboGioiTinh.Text = row.Cells["gioitinh"].Value.ToString();

            txtMSSV.Enabled = false;

            string malop = row.Cells["malop"].Value?.ToString().Trim();
            if (!string.IsNullOrEmpty(malop))
                cbx_lop.SelectedValue = malop;
            else if (cbx_lop.Items.Count > 0)
                cbx_lop.SelectedIndex = 0;

            if (row.Cells["ngaysinh"].Value is DateTime dt)
                dtpNgaySinh.Value = dt;
        }
    }
}
