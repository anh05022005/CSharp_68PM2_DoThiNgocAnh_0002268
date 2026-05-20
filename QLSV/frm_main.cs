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
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }

        private void frm_main_Load(object sender, EventArgs e)
        {
            UC_QLSinhVien uC_QLSinhVien = new UC_QLSinhVien();
            pnl_main.Controls.Clear();
            pnl_main.Controls.Add(uC_QLSinhVien);
        }

        private void quảnLýLớpHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UC_QLLopHoc uC_QLLopHoc = new UC_QLLopHoc();
            pnl_main.Controls.Clear();
            pnl_main.Controls.Add(uC_QLLopHoc);
        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UC_QLSinhVien uC_QLSinhVien = new UC_QLSinhVien();
            pnl_main.Controls.Clear();
            pnl_main.Controls.Add(uC_QLSinhVien);
        }
    }
}
