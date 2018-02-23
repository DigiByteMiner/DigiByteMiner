using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.View.v1.AddMinerScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneMiner.View.v1
{
    public partial class AddDualMiner : Form
    {
        private IMinerContainer m_parent = null;
        //private ICoin m_selectedDualCoin = null;
        public ICoin SelectedDualCoin { get; set; }

        private int m_currentCoinIndex = 0;
        public ICoin SelectedCoin { get; set; }

        public AddDualMiner(IMinerContainer parent)
        {
            m_parent = parent;
            InitializeComponent();
        }
        private bool AlgorithmSelected()
        {
            if (lbCoinSelect.SelectedIndices[0] >= 0 && lbCoinSelect.SelectedIndices[0] <= (lbCoinSelect.Items.Count - 1))
            {
                return true;
            }
            return false;
        }

        public void SetNextButtonState()
        {
            if (AlgorithmSelected() )
                m_parent.EnableNextButton();
            else
                m_parent.DisableNextButton();

        }
        public void Init()
        {
            IHashAlgorithm algo = SelectedCoin.Algorithm;
            if (SelectedDualCoin == null)
                SelectedDualCoin = algo.DefaultDualCoin;
            MakeSelectedDualCoin();
        }
        private void AddDualMiner_Load(object sender, EventArgs e)
        {
            try
            {

                int i = 0;
                IHashAlgorithm algo = SelectedCoin.Algorithm;
                if (SelectedDualCoin == null)
                    SelectedDualCoin = algo.DefaultDualCoin;




                ImageList Imagelist = new ImageList();
                Imagelist.ImageSize = new Size(25, 25);
                foreach (ICoin item in algo.SupportedDualCoins)
                {
                    Imagelist.Images.Add(item.Logo);
                }
                lbCoinSelect.LargeImageList = Imagelist;
                lbCoinSelect.SmallImageList = Imagelist;
                i = 0;
                foreach (ICoin item in algo.SupportedDualCoins)
                {
                    lbCoinSelect.Items.Add(new ListViewItem { ImageIndex = i, Text = item.Name });


                    //lbCoinSelect.Items.Add(item.Name);
                    if (item == SelectedDualCoin)
                        m_currentCoinIndex = i;
                    i++;
                }

                lbCoinSelect.Items[m_currentCoinIndex].Selected = true;


                //lbCoinSelect.SelectedIndex = m_currentCoinIndex;

                // lbCoinSelect.SelectedIndex = m_currentCoinIndex;
                lbCoinSelect.SelectedIndexChanged += lbCoinSelect_SelectedIndexChanged;
                SetNextButtonState();
                MakeSelectedDualCoin();

            }
            catch (Exception ex)
            {
            }
        }
        public void MakeSelectedDualCoin()
        {
            if (SelectedDualCoin != null)
            {
                m_parent.MakeSelectedDualCoin(SelectedDualCoin);
                lblSelectedCoin.Text = SelectedDualCoin.Name;
            }
            else
                lblSelectedCoin.Text = "No Coin Selected";
        }

        void lbCoinSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = lbCoinSelect.SelectedIndices[0];

                if (m_currentCoinIndex == index)
                    return;
                List<ICoin> coins = SelectedCoin.Algorithm.SupportedDualCoins;

                SelectedDualCoin = coins[index];
                m_currentCoinIndex = index;

                SetNextButtonState();
                //Tell parent which coin is currently seelcted
                MakeSelectedDualCoin();

            }
            catch (Exception)
            {

            }
        }

    }
}
