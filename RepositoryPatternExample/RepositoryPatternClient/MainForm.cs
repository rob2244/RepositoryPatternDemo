using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepositoryPatternClient
{
    public partial class MainForm : Form
    {
        readonly IRepository repository;
        public MainForm()
        {
            InitializeComponent();
        }
    }
}
