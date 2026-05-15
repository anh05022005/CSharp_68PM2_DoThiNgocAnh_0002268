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
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text;
            string password = txt_password.Text;
            if(username =="0002268@st.huce.edu.vn" && password == "0002268")
            {
                MessageBox.Show("đăng nhập thành công !");
            }
            else
            {
                MessageBox.Show("đăng nhập thất bại !");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
