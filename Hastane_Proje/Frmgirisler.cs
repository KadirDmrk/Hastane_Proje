﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnhastagiris_Click(object sender, EventArgs e)
        {
            Frmhastagiris fr=new Frmhastagiris();
            fr.Show();
            this.Hide();
        }

        private void btndoktorgırıs_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris fr =new FrmDoktorGiris();
            fr.Show();
            this.Hide();
        }

        private void btnsekretergiris_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris fr = new FrmSekreterGiris();
            fr.Show();
            this.Hide();
        }

        
    }
}
