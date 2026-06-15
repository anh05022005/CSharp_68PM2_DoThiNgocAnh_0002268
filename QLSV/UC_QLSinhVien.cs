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
        int _currentPage = 1;
        int _pageSize = 3;
        int _totalPages = 1;
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
                ApplyPaging();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyPaging()
        {
            _totalPages = Math.Max(1, (int)Math.Ceiling(_allData.Count / (double)_pageSize));
            if (_currentPage < 1) _currentPage = 1;
            if (_currentPage > _totalPages) _currentPage = _totalPages;

            dgv_DSSV.DataSource = _allData
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .Select(sv => new
                {
                    sv.id,
                    sv.hoten,
                    sv.gioitinh,
                    sv.ngaysinh,
                    sv.malop
                })
                .ToList();

            lb_trang_banghi.Text = _currentPage + "/" + _totalPages + "   |   " + _allData.Count + " bản ghi";
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
            if (string.IsNullOrEmpty(_selectedMaSV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var sv = db.tbl_sinhviens.FirstOrDefault(x => x.id == _selectedMaSV);
            if (sv == null) { MessageBox.Show("Không tìm thấy sinh viên!"); return; }

            sv.hoten = txtHoTen.Text.Trim();
            sv.ngaysinh = dtpNgaySinh.Value.Date;
            sv.gioitinh = cboGioiTinh.Text;
            sv.malop = cbx_lop.SelectedValue?.ToString()?.Trim();

            try
            {
                db.SubmitChanges();
                MessageBox.Show("Cập nhật thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMaSV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "Bạn có chắc muốn xóa sinh viên '" + txtHoTen.Text + "'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            var sv = db.tbl_sinhviens.FirstOrDefault(x => x.id == _selectedMaSV);
            if (sv == null) { MessageBox.Show("Không tìm thấy sinh viên!"); return; }

            try
            {
                db.tbl_sinhviens.DeleteOnSubmit(sv);
                db.SubmitChanges();
                MessageBox.Show("Xóa thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtMSSV.Clear();
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = -1;
            cbx_lop.SelectedIndex = -1;

            _selectedMaSV = "";
            txtHoTen.Focus();
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

        private void btn_dau_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            ApplyPaging();
        }

        private void btn_truoc_Click(object sender, EventArgs e)
        {
            _currentPage--;
            ApplyPaging();
        }

        private void btn_sau_Click(object sender, EventArgs e)
        {
            _currentPage++;
            ApplyPaging();
        }

        private void btn_cuoi_Click(object sender, EventArgs e)
        {
            _currentPage = _totalPages;
            ApplyPaging();
        }

        private void btn_timKiem_Click(object sender, EventArgs e)
        {
            LoadSinhVienTheoTu(txt_timKiem.Text);
        }

        private void LoadSinhVienTheoTu(string tuKhoa)
        {
            string tk = tuKhoa.Trim();
            _allData = db.tbl_sinhviens
                          .Where(sv =>
                              sv.id.Contains(tk) ||
                              sv.hoten.Contains(tk) ||
                              sv.malop.Contains(tk))
                          .OrderBy(sv => sv.id)
                          .ToList();
            _currentPage = 1;
            ApplyPaging();
        }
    }
}
